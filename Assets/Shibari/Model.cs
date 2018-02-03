using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using UnityEditor;

namespace Shibari
{
    [InitializeOnLoad]
    public static class Model
    {
        public const string PREFS_KEY = "SHIBARI_MODEL_RECORDS";

        public static Dictionary<Type, BindableValueReflection[]> FullModelTree { get; private set; }

        public static Dictionary<Type, BindableValueReflection[]> VisibleInEditorModelTree { get; private set; }

        public static ModelRecord[] Records { get; private set; }

        private static Dictionary<string, BindableData> registered = new Dictionary<string, BindableData>();

        static Model()
        {
            LoadRecords();
        }

        public static void BeginInitialization()
        {
            foreach (var record in Records)
            {
                object o = Activator.CreateInstance(record.type.Type);
                Register(record.key, (BindableData)o);
            }
        }

        public static void FinalizeInitialization()
        {
            foreach (var data in registered.Values)
                foreach (var value in data.Values)
                    value.Value.InvokeOnValueChanged();
        }

        public static void LoadRecords()
        {
            ShibariSettings settings = Resources.Load<ShibariSettings>("ShibariSettings");

            if (settings == null)
                Debug.LogError("Could not find container prefab for Shibari Settings.");

            if (settings.values == null)
                settings.values = new List<ModelRecord>();

            var groups = settings.values.Where(r => r != null).GroupBy(r => r.key);
            foreach (var group in groups)
            {
                var record = group.First();
                if (group.Take(2).Count() == 2)
                    Debug.LogErrorFormat("Found multiple datas with id {0}, ignoring duplicates.", record.key);
            }
            Records = groups.Select(g => g.First()).ToArray();

            FullModelTree = new Dictionary<Type, BindableValueReflection[]>();
            VisibleInEditorModelTree = new Dictionary<Type, BindableValueReflection[]>();

            var executingAssembly = Assembly.GetExecutingAssembly();
            ProcessBindableDataTypes(executingAssembly.GetTypes());

            foreach (var assembly in executingAssembly.GetReferencedAssemblies())
                ProcessBindableDataTypes(Assembly.Load(assembly).GetTypes());
        }

        public static IEnumerable<PropertyInfo> GetBindableValues(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(p => !p.GetMethod.IsPrivate)
                    .Where(p => IsBindableValue(p));
        }

        public static IEnumerable<PropertyInfo> GetPrimaryValues(Type type)
        {
            return GetBindableValues(type).Where(p => CheckTypeTreeByPredicate(type, (t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(PrimaryValue<>)));
        }

        private static void ProcessBindableDataTypes(Type[] types)
        {
            foreach (var type in types.Where(t => !t.IsAbstract).Where(t => typeof(BindableData).IsAssignableFrom(t)))
            {
                var fullModel = new List<BindableValueReflection>();
                var visibleModel = new List<BindableValueReflection>();
                if (type.GetConstructor(new Type[0]) == null)
                    Debug.LogErrorFormat("Type {0} has to implement parameterless constructor.", type.FullName);

                foreach (var p in GetBindableValues(type))
                {
                    Type t = p.PropertyType;
                    while (!(t.IsGenericType && t.GetGenericTypeDefinition() == typeof(BindableValue<>)))
                    {
                        t = t.BaseType;
                    }
                    fullModel.Add(new BindableValueReflection(p.Name, p.PropertyType, t.GetGenericArguments()[0]));
                    if (p.GetCustomAttribute(typeof(ShowInEditorAttribute)) != null)
                        visibleModel.Add(new BindableValueReflection(p.Name, p.PropertyType, t.GetGenericArguments()[0]));
                }

                if (fullModel.Any())
                    FullModelTree[type] = fullModel.ToArray();
                if (visibleModel.Any())
                    VisibleInEditorModelTree[type] = visibleModel.ToArray();
            }
        }

        public static void Add(string id, BindableData data)
        {
            if (registered.ContainsKey(id))
                throw new ArgumentException($"Data with id {id} is already registered", "id");
            registered[id] = data;
        }

        public static T Get<T>(string id) where T : BindableData
        {
            if (!registered.ContainsKey(id))
                throw new ArgumentException($"Data with id {id} is not registered.", "id");

            return (T)registered[id];
        }

        public static void Remove(string id)
        {
            registered.Remove(id);
        }

        public static void Register(string dataId, BindableData data)
        {
            data.Initialize();

            Add(dataId, data);
        }

        public static bool IsBindableValue(PropertyInfo property)
        {
            return CheckTypeTreeByPredicate(property.PropertyType, (t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(BindableValue<>));
        }

        public static bool IsPrimaryValue(PropertyInfo property)
        {
            return CheckTypeTreeByPredicate(property.PropertyType, (t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(PrimaryValue<>));
        }

        public static bool IsSerializableValue(Type type, string propertyName)
        {
            var property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return IsSerializableValue(property);
        }

        public static bool IsSerializableValue(PropertyInfo property)
        {
            return property.GetCustomAttribute<SerializeValueAttribute>() != null
                && CheckTypeTreeByPredicate(property.PropertyType, (t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(PrimaryValue<>));
        }

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

        public static string GenerateSerializationTemplate(Type t)
        {
            if (!typeof(BindableData).IsAssignableFrom(t))
                throw new ArgumentException("Type t should be child of BindableData", "t");

            return BindableDataJsonConverter.GenerateJsonTemplate(t);
        }

        public static string GenerateSerializationTemplate<T>() where T : BindableData
        {
            return GenerateSerializationTemplate(typeof(T));
        }
    }
}