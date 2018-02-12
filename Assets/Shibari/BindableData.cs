using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Shibari
{
    public abstract class BindableData
    {
        public ReadOnlyDictionary<string, BindableValueInfo> Values { get; private set; }
        public ReadOnlyDictionary<string, AssignableValueInfo> AssignableValues { get; private set; }
        public ReadOnlyDictionary<string, BindableData> Childs { get; private set; }

        private static readonly BindableDataJsonConverter converter = new BindableDataJsonConverter();

        #region public static methods
        public static IEnumerable<string> GetBindableValuesPaths(Type type, string prefix, bool isSetterRequired, bool isVisibleInEditorRequired, Type valueType = null)
        {
            List<string> result = GetBindableDatas(type)
                .Where(property => IsEditorVisibilityAcceptable(property, isVisibleInEditorRequired))
                .SelectMany(property => GetBindableValuesPaths(property.PropertyType, prefix + property.Name + "/", isSetterRequired, isVisibleInEditorRequired, valueType)).ToList();
            if (isSetterRequired)
            {
                result = result
                        .Concat(GetAssignableValues(type)
                            .Where(property => 
                                IsEditorVisibilityAcceptable(property, isVisibleInEditorRequired) 
                                && (valueType == null || valueType.IsAssignableFrom(GetBindableValueValueType(property.PropertyType))))
                            .Select(property => prefix + property.Name)).ToList();
            }
            else
            {
                result = result
                    .Concat(GetBindableValues(type)
                        .Where(property => 
                            IsEditorVisibilityAcceptable(property, isVisibleInEditorRequired)
                            && (valueType == null || valueType.IsAssignableFrom(GetBindableValueValueType(property.PropertyType))))
                        .Select(property => prefix + property.Name)).ToList();
            }
            return result.ToList();
        }

        public static IEnumerable<PropertyInfo> GetBindableDatas(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(p => !p.GetMethod.IsPrivate)
                .Where(p => IsBindableData(p.PropertyType));
        }

        public static IEnumerable<PropertyInfo> GetBindableValues(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(p => !p.GetMethod.IsPrivate)
                    .Where(p => IsBindableValue(p.PropertyType));
        }

        public static IEnumerable<PropertyInfo> GetAssignableValues(Type type)
        {
            return GetBindableValues(type).Where(p => CheckTypeTreeByPredicate(type, (t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(AssignableValue<>)));
        }

        public static IEnumerable<PropertyInfo> GetSerializableValues(Type type)
        {
            return GetBindableValues(type).Where(p => IsSerializableValue(p));
        }

        public static bool IsBindableValue(Type propertyType)
        {
            return CheckTypeTreeByPredicate(propertyType, (t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(BindableValue<>));
        }

        public static bool IsAssignableValue(Type propertyType)
        {
            return CheckTypeTreeByPredicate(propertyType, (t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(AssignableValue<>));
        }

        public static bool IsCalculatedValue(Type propertyType)
        {
            return CheckTypeTreeByPredicate(propertyType, (t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(CalculatedValue<>));
        }

        public static bool IsSerializableValue(Type modelType, string propertyName)
        {
            var property = modelType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return IsSerializableValue(property);
        }

        public static bool IsSerializableValue(PropertyInfo property)
        {
            return property.GetCustomAttribute<SerializeValueAttribute>() != null
                && CheckTypeTreeByPredicate(property.PropertyType, (t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(AssignableValue<>));
        }

        public static bool IsBindableData(Type type)
        {
            var result = typeof(BindableData).IsAssignableFrom(type);
            return result;
        }

        public static BindableData GetDeserializedData(string serialized, Type type)
        {
            if (!typeof(BindableData).IsAssignableFrom(type))
            {
                throw new ArgumentException($"Type {typeof(BindableData)} is not assignable from type {type}");
            }
            return (BindableData)JsonConvert.DeserializeObject(serialized, type, converter);
        }

        internal static Type GetBindableValueValueType(Type propertyType)
        {
            Type t = propertyType;
            while (!(t.IsGenericType && t.GetGenericTypeDefinition() == typeof(BindableValue<>)))
            {
                t = t.BaseType;

                if (t == typeof(object))
                    throw new ArgumentException("Property type is not BindableValue<>.", "propertyType");
            }

            return t.GetGenericArguments()[0];
        }

        public static T GetDeserializedData<T>(string serialized) where T : BindableData
        {
            return (T)GetDeserializedData(serialized, typeof(T));
        }
        #endregion

        #region public instance methods

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

        public virtual void Initialize()
        {
            var values = new Dictionary<string, BindableValueInfo>();
            var assignableValues = new Dictionary<string, AssignableValueInfo>();

            var valueProperties = GetBindableValues(GetType());

            foreach (var p in valueProperties)
            {
                values[p.Name] = new BindableValueInfo(p, this);
                if (IsAssignableValue(p.PropertyType))
                    assignableValues[p.Name] = new AssignableValueInfo(p, this);
            }

            Values = new ReadOnlyDictionary<string, BindableValueInfo>(values);
            AssignableValues = new ReadOnlyDictionary<string, AssignableValueInfo>(assignableValues);

            var childs = new Dictionary<string, BindableData>();

            var childProperties = GetBindableDatas(GetType());

            foreach (var p in childProperties)
            {
                childs[p.Name] = p.GetValue(this) as BindableData;

                childs[p.Name].Initialize();
            }
        }
        #endregion

        #region private static methods
        private static bool CheckTypeTreeByPredicate(Type type, Func<Type, bool> predicate)
        {
            while (type != typeof(object))
            {
                if (predicate(type))
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }

        private static bool IsEditorVisibilityAcceptable(PropertyInfo property, bool isVisibleInEditorRequired)
        {
            return !(isVisibleInEditorRequired && property.GetCustomAttribute<ShowInEditorAttribute>() == null);
        }
        #endregion

    }
}