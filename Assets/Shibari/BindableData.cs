﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace Shibari
{
    public abstract class BindableData
    {
        public virtual void Init(string key) { }

        public void InitializeProperties()
        {
            ReflectedProperties = new Dictionary<string, BindableFieldInfo>();

            foreach (var p in GetType().GetProperties())
            {
                Type type = p.PropertyType;
                while (type != typeof(object))
                {
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(BindableField<>))
                    {
                        object o = Activator.CreateInstance(p.PropertyType);
                        p.SetValue(this, o);
                        ReflectedProperties[p.Name] = new BindableFieldInfo()
                        {
                            dataField = p.GetValue(this),
                            eventInfo = p.PropertyType.GetEvent("OnValueChanged"),
                            property = p,
                            valueType = type.GetGenericArguments()[0],
                        };
                        ReflectedProperties[p.Name].getMethod = ReflectedProperties[p.Name].dataField.GetType().GetMethod("Get", new Type[0]);

                        break;
                    }
                    type = type.BaseType;
                }
            }
        }

        public Dictionary<string, BindableFieldInfo> ReflectedProperties { get; protected set; }
    }
}