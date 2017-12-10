using UnityEngine;
using System.Reflection;
using System;

namespace Shibari
{
    public class BindableFieldInfo
    {
        public object dataField;
        public MethodInfo getMethod;
        public EventInfo eventInfo;
        public PropertyInfo property;
        public Type valueType;

        public object GetValue()
        {
            return getMethod.Invoke(dataField, new object[0]);
        }
    }
}