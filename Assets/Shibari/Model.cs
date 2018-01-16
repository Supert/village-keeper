using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;

namespace Shibari
{
    [InitializeOnLoad]
    public static class Model
    {
        public const string PREFS_KEY = "SHIBARI_MODEL_RECORDS";

        public static Dictionary<Type, Tuple<string, Type>[]> ModelTree { get; private set; }

        public static Dictionary<Type, BindableMapper> Mappers { get; private set; }

        public static ModelRecord[] Records { get; private set; }

        private static Dictionary<string, BindableData> registered = new Dictionary<string, BindableData>();

        static Model()
        {
            LoadRecords();
            LoadMappers();
        }

        public static void Init()
        {
            foreach (var record in Records)
            {
                object o = Activator.CreateInstance(record.type.Type);
                Register(record.key, (BindableData)o);
            }
        }

        private static void LoadMappers()
        {
            Mappers = new Dictionary<Type, BindableMapper>();

            var executingAssembly = Assembly.GetExecutingAssembly();
            ProcessMapperTypes(executingAssembly.GetTypes());

            foreach (var assembly in executingAssembly.GetReferencedAssemblies())
                ProcessMapperTypes(Assembly.Load(assembly).GetTypes());
        }

        private static void ProcessMapperTypes(Type[] types)
        {
            foreach (var type in types.Where(t => !t.IsAbstract).Where(t => typeof(BindableMapper).IsAssignableFrom(t)))
            {
                if (type.GetConstructor(new Type[0]) == null)
                    Debug.LogErrorFormat("Type {0} has to implement parameterless constructor.", type.FullName);

                Mappers[type] = (BindableMapper)Activator.CreateInstance(type);
                Mappers[type].InitializeMappings();
            }
        }

        public static void LoadRecords()
        {
            ShibariSettings settings = Resources.Load<ShibariSettings>("ShibariSettings");

            if (settings == null)
                Debug.LogError("Could not find container prefab for Shibari Settings.");

            if (settings.values == null)
                settings.values = new ModelRecord[0];

            var groups = settings.values.Where(r => r != null).GroupBy(r => r.key);
            foreach (var group in groups)
            {
                var record = group.First();
                if (group.Take(2).Count() == 2)
                    Debug.LogErrorFormat("Found multiple datas with id {0}, ignoring duplicates.", record.key);
            }
            Records = groups.Select(g => g.First()).ToArray();

            ModelTree = new Dictionary<Type, Tuple<string, Type>[]>();

            var executingAssembly = Assembly.GetExecutingAssembly();
            ProcessBindableDataTypes(executingAssembly.GetTypes());

            foreach (var assembly in executingAssembly.GetReferencedAssemblies())
                ProcessBindableDataTypes(Assembly.Load(assembly).GetTypes());
        }

        private static void ProcessBindableDataTypes(Type[] types)
        {
            foreach (var type in types.Where(t => !t.IsAbstract).Where(t => typeof(BindableData).IsAssignableFrom(t)))
            {
                if (type.GetConstructor(new Type[0]) == null)
                    Debug.LogErrorFormat("Type {0} has to implement parameterless constructor.", type.FullName);

                ModelTree[type] = type.GetProperties()
                    .Where(p => IsBindableField(p))
                    .Select(p =>
                    {
                        Type t = p.PropertyType;
                        while (!(t.IsGenericType && t.GetGenericTypeDefinition() == typeof(BindableField<>)))
                        {
                            t = t.BaseType;
                        }
                        return new Tuple<string, Type>(p.Name, t.GetGenericArguments()[0]);
                    })
                    .ToArray();
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

            if (!typeof(T).IsAssignableFrom(registered[id].GetType()))
                throw new Exception($"Can't cast data with id {id} to type {typeof(T)}");
            return (T)registered[id];
        }

        public static void Remove(string id)
        {
            registered.Remove(id);
        }

        public static void Register(string dataId, BindableData data)
        {
            InitializeFields(data);

            data.InitializeProperties();

            Add(dataId, data);
        }

        private static void InitializeFields(BindableData data)
        {
            foreach (var p in data.GetType().GetProperties())
            {
                Type type = p.PropertyType;
                while (type != typeof(object))
                {
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(BindableField<>))
                    {
                        object value = Activator.CreateInstance(p.PropertyType);
                        p.SetValue(data, value);
                        break;
                    }
                    type = type.BaseType;
                }
            }
        }

        public static bool IsBindableField(PropertyInfo property)
        {
            return CheckTypeTreeByPredicate(property.PropertyType, (t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(BindableField<>));
        }

        public static bool IsSerializableField(PropertyInfo property)
        {
            return CheckTypeTreeByPredicate(property.PropertyType, (t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(SerializableField<>));
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

            BindableData def = (BindableData)Activator.CreateInstance(t);
            def.InitializeProperties();
            return def.Serialize();
        }

        public static string GenerateSerializationTemplate<T>() where T : BindableData
        {
            return GenerateSerializationTemplate(typeof(T));
        }
    }
}