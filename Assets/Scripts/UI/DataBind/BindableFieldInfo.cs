using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Shibari
{
    public class BindableFieldInfo
    {
        public object DataField { get; private set; }
        public EventInfo EventInfo { get; private set; }
        public PropertyInfo Property { get; private set; }
        public Type ValueType { get; private set; }

        private MethodInfo getMethod;
        private MethodInfo setMethod;

        public BindableFieldInfo(PropertyInfo property, BindableData owner)
        {
            Property = property;
            DataField = property.GetValue(owner);

            Type type = property.PropertyType;
            Type dataFieldType = DataField.GetType();

            EventInfo = property.PropertyType.GetEvent("OnValueChanged");
            ValueType = type.GetGenericArguments()[0];
            getMethod = dataFieldType.GetMethod("Get", new Type[0]);
            setMethod = dataFieldType.GetMethod("Set", new Type[1] { ValueType });
        }

        public object GetValue()
        {
            return getMethod.Invoke(DataField, new object[0]);
        }

        public void SetValue(object o)
        {
            if (ValueType.IsValueType && o == null)
                throw new NullReferenceException($"Field type {ValueType} is value type, but argument is null");

            if (o == null)
                setMethod.Invoke(DataField, null);

            Type objectType = o.GetType();

            if (!ValueType.IsAssignableFrom(objectType))
            {
                if (typeof(Array).IsAssignableFrom(ValueType))
                {
                    IList inputList = (IList)o;
                    Array array = (Array)Activator.CreateInstance(ValueType, new object[1] { inputList.Count });

                    inputList.CopyTo(array, 0);
                    setMethod.Invoke(DataField, new object[1] { array });
                }
                else if (ValueType.GetInterface(nameof(IDictionary)) != null)
                {
                    IList inputList = (IList)o;
                    var dictionary = (IDictionary)Activator.CreateInstance(ValueType);
                    Type kvpType = typeof(KeyValuePair<,>).MakeGenericType(ValueType.GenericTypeArguments);
                    PropertyInfo kvpKey = kvpType.GetProperty("Key");
                    PropertyInfo kvpValue = kvpType.GetProperty("Value");
                    foreach (var kvp in inputList)
                        dictionary[kvpKey.GetValue(kvp)] = kvpValue.GetValue(kvp);
                    setMethod.Invoke(DataField, new object[1] { dictionary });
                }
                else if (ValueType.GetInterface(nameof(IList)) != null
                    && o is IList)
                {
                    var inputList = (IList)o;
                    var list = (IList)Activator.CreateInstance(ValueType);
                    foreach (var element in inputList)
                        list.Add(element);
                    setMethod.Invoke(DataField, new object[1] { list });
                }
                else
                {
                    throw new ArgumentException($"Field type {ValueType} is not assignable from argument type {o.GetType()}");
                }
                return;
            }
            else
            {
                setMethod.Invoke(DataField, new object[1] { o });
            }
        }
    }
}