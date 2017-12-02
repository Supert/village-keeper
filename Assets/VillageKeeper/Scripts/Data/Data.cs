using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace VillageKeeper.Data
{
    public abstract class Data
    {
        public virtual void InitDataFields(string prefix)
        {
            foreach (var p in GetType().GetProperties())
            {
                Type type = p.PropertyType;
                while (type != typeof(object))
                {
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(DataField<>))
                    {
                        MethodInfo init = p.PropertyType.GetMethod("Init", new Type[1] { typeof(string) });
                        object value = p.PropertyType.GetConstructor(new Type[0]).Invoke(new object[0]);
                        init.Invoke(value, new object[1] { p.Name });
                        p.SetValue(this, value);
                        break;
                    }
                    type = type.BaseType;
                }
            }
        }

        public static bool CheckIfPropertyIsDataField(PropertyInfo property)
        {
            Type type = property.PropertyType;
            while (type != typeof(object))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(DataField<>))
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }
    }
}