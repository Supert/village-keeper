using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace VillageKeeper.Data
{
    public abstract class BindedData
    {
        private static Dictionary<string, BindedData> registered = new Dictionary<string, BindedData>();

        public static void Add(string id, BindedData data)
        {
            if (registered.ContainsKey(id))
                throw new ArgumentException(string.Format("Data with id {0} is already registered"));
            registered[id] = data;
        }

        public static T Get<T>(string id) where T : BindedData
        {
            if (!registered.ContainsKey(id))
                throw new ArgumentException(string.Format("Data with id {0} is not registered.", id), "id");
            T ret = registered[id] as T;
            if (ret == null)
                throw new Exception(string.Format("Data with id {0} can't be cast to {1}", id, typeof(T)));
            return ret;
        }

        public static void Remove(string id)
        {
            registered.Remove(id);
        }

        public virtual void Register(string dataId)
        {
            foreach (var p in GetType().GetProperties())
            {
                Type type = p.PropertyType;
                while (type != typeof(object))
                {
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(BindableField<>))
                    {
                        MethodInfo init = p.PropertyType.GetMethod("Init", new Type[2] { typeof(string), typeof(string) });
                        object value = p.PropertyType.GetConstructor(new Type[0]).Invoke(new object[0]);
                        init.Invoke(value, new object[2] { dataId, p.Name });
                        p.SetValue(this, value);
                        break;
                    }
                    type = type.BaseType;
                }
            }

            Add(dataId, this);
        }

        public static bool CheckIfPropertyIsBindableField(PropertyInfo property)
        {
            Type type = property.PropertyType;
            while (type != typeof(object))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(BindableField<>))
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }
    }
}