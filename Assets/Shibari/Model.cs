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

        public static Dictionary<Type, Tuple<string, Type>[]> ModelTree { get; private set; }

        public static ModelRecord[] Records { get; private set; }

        public static void Init()
        {
            LoadRecords();

            foreach (var record in Records)
            {
                object o = Activator.CreateInstance(record.type.Type);
                Register(record.key, (BindableData) o);
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
            ProcessTypes(executingAssembly.GetTypes());

            foreach (var assembly in executingAssembly.GetReferencedAssemblies())
                ProcessTypes(Assembly.Load(assembly).GetTypes());
        }

        private static void ProcessTypes(Type[] types)
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

        private static Dictionary<string, BindableData> registered = new Dictionary<string, BindableData>();

        public static void Add(string id, BindableData data)
        {
            if (registered.ContainsKey(id))
                throw new ArgumentException(string.Format("Data with id {0} is already registered"));
            registered[id] = data;
        }

        public static T Get<T>(string id) where T : BindableData
        {
            if (!registered.ContainsKey(id))
                throw new ArgumentException(string.Format("Data with id {0} is not registered.", id), "id");
            
            if (!typeof(T).IsAssignableFrom(registered[id].GetType()))
                throw new Exception(string.Format("Can't cast data with id {0} to type {1}", id, typeof(T)));
            return (T)registered[id];
        }

        public static void Remove(string id)
        {
            registered.Remove(id);
        }

        public static void Register(string dataId, BindableData data)
        {
            foreach (var p in data.GetType().GetProperties())
            {
                Type type = p.PropertyType;
                while (type != typeof(object))
                {
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(BindableField<>))
                    {
                        MethodInfo init = p.PropertyType.GetMethod("Init", new Type[2] { typeof(string), typeof(string) });
                        object value = Activator.CreateInstance(p.PropertyType);
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