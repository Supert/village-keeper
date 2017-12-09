using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;

namespace Shibari
{
    public static class Model
    {
        public const string PREFS_KEY = "SHIBARI_MODEL_RECORDS";

        public static LocalizationData Localization { get; private set; }

        private static Dictionary<Type, Tuple<string, Type>[]> modelTree;

        private static ModelRecord[] records;

        public static void Init()
        {
            LoadRecords();

            foreach (var record in records)
            {
                Register(record.key, (IBindableData)record.type.Type.GetConstructor(new Type[0]).Invoke(new object[0]));
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
            records = groups.Select(g => g.First()).ToArray();

            modelTree = new Dictionary<Type, Tuple<string, Type>[]>();

            var executingAssembly = Assembly.GetExecutingAssembly();
            ProcessTypes(executingAssembly.GetTypes());

            foreach (var assembly in executingAssembly.GetReferencedAssemblies())
                ProcessTypes(Assembly.Load(assembly).GetTypes());
        }

        private static void ProcessTypes(Type[] types)
        {
            foreach (var type in types.Where(t => t.GetInterfaces().Contains(typeof(IBindableData))))
            {
                if (type.GetConstructor(new Type[0]) == null)
                    Debug.LogErrorFormat("Type {0} has to implement parameterless constructor.", type.FullName);

                modelTree[type] = type.GetProperties()
                    .Where(p => IsBindableField(p))
                    .Select(p => new Tuple<string, Type>(p.Name, p.PropertyType))
                    .ToArray();
            }
        }

        private static Dictionary<string, IBindableData> registered = new Dictionary<string, IBindableData>();

        public static void Add(string id, IBindableData data)
        {
            if (registered.ContainsKey(id))
                throw new ArgumentException(string.Format("Data with id {0} is already registered"));
            registered[id] = data;
        }

        public static T Get<T>(string id) where T : IBindableData
        {
            if (!registered.ContainsKey(id))
                throw new ArgumentException(string.Format("Data with id {0} is not registered.", id), "id");
            if (typeof(T).IsAssignableFrom(registered[id].GetType()))
                throw new Exception(string.Format("Can't cast data with id {0} to type {1}", id, typeof(T)));
            return (T)registered[id];
        }

        public static void Remove(string id)
        {
            registered.Remove(id);
        }

        public static void Register(string dataId, IBindableData data)
        {
            foreach (var p in data.GetType().GetProperties())
            {
                Type type = p.PropertyType;
                while (type != typeof(object))
                {
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(BindableField<>))
                    {
                        MethodInfo init = p.PropertyType.GetMethod("Init", new Type[2] { typeof(string), typeof(string) });
                        object value = p.PropertyType.GetConstructor(new Type[0]).Invoke(new object[0]);
                        init.Invoke(value, new object[2] { dataId, p.Name });
                        p.SetValue(data, value);
                        break;
                    }
                    type = type.BaseType;
                }
            }

            Add(dataId, data);
        }

        public static bool IsBindableField(PropertyInfo property)
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