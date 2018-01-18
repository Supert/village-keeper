﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shibari
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class SerializeValueAttribute : Attribute
    {

    }

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
                instance.InitializePrimaryValues();
                instance.InitializeSecondaryValues();
            }

            JObject jsonObject = JObject.Load(reader);
            foreach (var serialized in jsonObject.Properties())
            {
                if (instance.ReflectedProperties.ContainsKey(serialized.Name))
                {
                    PrimaryValueInfo reflected = instance.ReflectedProperties[serialized.Name];
                    if (Model.IsSerializableValue(reflected.Property))
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

            foreach (var property in data.ReflectedProperties.Where(p => Model.IsSerializableValue(p.Value.Property)))
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

        public static string GenerateJsonTemplate(Type type)
        {
            if (!typeof(BindableData).IsAssignableFrom(type))
                throw new ArgumentException("Type should be child of BindableData", "type");

            JObject jsonObject = new JObject();
            
            foreach (var property in Model.FullModelTree[type].Where(tuple => Model.IsSerializableValue(type.GetProperty(tuple.Item1))))
            {
                Type valueType = property.Item2;

                if (valueType.IsValueType)
                {
                    jsonObject.AddFirst(new JProperty(property.Item1, Activator.CreateInstance(valueType)));
                }
                else if (valueType == typeof(string) || valueType.GetInterface(nameof(IEnumerable)) == null)
                {
                    jsonObject.AddFirst(new JProperty(property.Item1, new JValue("")));
                }
                else
                {
                    jsonObject.AddFirst(new JProperty(property.Item1, new JArray()));
                }
            }

            return jsonObject.ToString(Formatting.Indented);
        }
    }
}