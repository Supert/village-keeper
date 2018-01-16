using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Shibari
{
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