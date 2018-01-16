using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Shibari
{
    internal class BindableDataJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(BindableData).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            BindableData instance = (BindableData)existingValue;
            if (instance == null)
            {
                instance = (BindableData)Activator.CreateInstance(objectType);
                instance.InitializeProperties();
            }

            JObject jsonObject = JObject.Load(reader);
            foreach (var serialized in jsonObject.Properties())
            {
                if (instance.ReflectedProperties.ContainsKey(serialized.Name))
                {
                    BindableFieldInfo reflected = instance.ReflectedProperties[serialized.Name];
                    if (Model.IsSerializableField(reflected.Property))
                    {
                        if (serialized.Value.Type == JTokenType.Array)
                        {
                            Type elementType = null;
                            if (reflected.ValueType.IsArray)
                            {
                                elementType = reflected.ValueType.GetElementType();
                            }
                            else if (reflected.ValueType.GetInterface(nameof(IDictionary)) != null)
                            {
                                elementType = typeof(KeyValuePair<,>).MakeGenericType(reflected.ValueType.GenericTypeArguments);
                            }
                            else if (reflected.ValueType.GetInterface(nameof(IEnumerable)) != null)
                            {
                                elementType = reflected.ValueType.GenericTypeArguments[0];
                            }

                            var elements = serialized.Values().Select(v => v.ToObject(elementType)).ToArray();
                            reflected.SetValue(elements);
                            continue;
                        }
                        reflected.SetValue(serialized.ToObject(reflected.ValueType));
                        continue;
                    }
                    else
                    {
                        Debug.LogError($"Property {serialized.Name} is not marked as serializable.");
                        continue;
                    }
                }
                else
                {
                    Debug.LogError($"Property {serialized.Name} is not found.");
                    continue;
                }
            }

            return instance;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject jsonObject = new JObject();
            BindableData data = (BindableData)value;

            foreach (var property in data.ReflectedProperties.Where(p => Model.IsSerializableField(p.Value.Property)))
            {
                object v = property.Value.GetValue();
                if (v == null)
                {
                    if (property.Value.ValueType == typeof(string) || property.Value.ValueType.GetInterface(nameof(IEnumerable)) == null)
                    {
                        jsonObject.AddFirst(new JProperty(property.Key, new JValue("")));
                    }
                    else
                    {
                        jsonObject.AddFirst(new JProperty(property.Key, new JArray()));
                    }
                }
                else
                {
                    jsonObject.AddFirst(new JProperty(property.Key, JToken.FromObject(v)));
                }
            }

            jsonObject.WriteTo(writer);
        }
    }

    public abstract class BindableData
    {
        private static readonly BindableDataJsonConverter converter = new BindableDataJsonConverter();

        public Dictionary<string, BindableFieldInfo> ReflectedProperties { get; protected set; }

        public void InitializeProperties()
        {
            ReflectedProperties = new Dictionary<string, BindableFieldInfo>();

            var t = GetType();

            var properties = t.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var p in properties)
            {
                Type type = p.PropertyType;
                while (type != typeof(object))
                {
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(BindableField<>))
                    {
                        object o = Activator.CreateInstance(p.PropertyType);
                        p.SetValue(this, o);
                        ReflectedProperties[p.Name] = new BindableFieldInfo(p, this);

                        break;
                    }
                    type = type.BaseType;
                }
            }
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, converter);
        }

        public void Deserialize(string serialized)
        {
            var jsonSerializer = new JsonSerializer();

            BindableData deserialized = GetDeserializedData(serialized, GetType());
            foreach (var property in deserialized.ReflectedProperties)
            {
                ReflectedProperties[property.Key].SetValue(property.Value.GetValue());
            }
        }

        public static BindableData GetDeserializedData (string serialized, Type type)
        {
            if (!typeof(BindableData).IsAssignableFrom(type))
            {
                throw new ArgumentException($"Type {typeof(BindableData)} is not assignable from type {type}");
            }
            return (BindableData) JsonConvert.DeserializeObject(serialized, type, converter);
        }

        public static T GetDeserializedData<T>(string serialized) where T : BindableData
        {
            return (T) GetDeserializedData(serialized, typeof(T));
        }
    }
}