using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Shibari
{

    public abstract class BindableData
    {
        public Dictionary<string, BindableValueInfo> Values { get; protected set; }
        public Dictionary<string, AssignableValueInfo> AssignableValues { get; protected set; }

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

        public void Deserialize(string serialized)
        {
            BindableData deserialized = GetDeserializedData(serialized, GetType());
            foreach (var property in deserialized.AssignableValues)
            {
                AssignableValues[property.Key].SetValue(property.Value.GetValue());
            }
        }

        public void Initialize()
        {
            Values = new Dictionary<string, BindableValueInfo>();
            AssignableValues = new Dictionary<string, AssignableValueInfo>();

            var properties = Model.GetBindableValues(GetType());
            
            foreach (var p in properties)
            {
                Values[p.Name] = new BindableValueInfo(p, this);
                if (Model.IsAssignableValue(p.PropertyType))
                    AssignableValues[p.Name] = new AssignableValueInfo(p, this);
            }
        }
    }
}