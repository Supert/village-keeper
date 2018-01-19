using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Shibari
{

    public abstract class BindableData
    {
        public Dictionary<string, BindableValueInfo> Values { get; protected set; }
        public Dictionary<string, PrimaryValueInfo> PrimaryValues { get; protected set; }

        private static readonly BindableDataJsonConverter converter = new BindableDataJsonConverter();

        public static BindableData GetDeserializedData(string serialized, Type type)
        {
            if (!typeof(BindableData).IsAssignableFrom(type))
            {
                throw new ArgumentException($"Type {typeof(BindableData)} is not assignable from type {type}");
            }
            return (BindableData)JsonConvert.DeserializeObject(serialized, type, converter);
        }

        public static T GetDeserializedData<T>(string serialized) where T : BindableData
        {
            return (T)GetDeserializedData(serialized, typeof(T));
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, converter);
        }

        public void Deserialize(string serialized, bool raiseOnValueChanged = false)
        {
            BindableData deserialized = GetDeserializedData(serialized, GetType());
            foreach (var property in deserialized.PrimaryValues)
            {
                PrimaryValues[property.Key].SetValue(property.Value.GetValue(), !raiseOnValueChanged);
            }
        }

        public void Initialize()
        {
            Values = new Dictionary<string, BindableValueInfo>();
            PrimaryValues = new Dictionary<string, PrimaryValueInfo>();

            var properties = Model.GetBindableValues(GetType());
            
            foreach (var p in properties)
            {
                Type type = p.PropertyType;
                while (type != typeof(object))
                {
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(PrimaryValue<>))
                    {
                        object value = Activator.CreateInstance(p.PropertyType);
                        p.SetValue(this, value);
                        break;
                    }
                    type = type.BaseType;
                }
            }
            
            foreach (var p in properties)
            {
                Values[p.Name] = new BindableValueInfo(p, this);
                if (Model.IsPrimaryValue(p))
                    PrimaryValues[p.Name] = new PrimaryValueInfo(p, this);
            }
        }
    }
}