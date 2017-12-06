using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Shibari.Editor
{
    public class ShibariSettingsWindow : EditorWindow
    {
        [MenuItem("Shibari/Settings")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            ShibariSettingsWindow window = (ShibariSettingsWindow)GetWindow(typeof(ShibariSettingsWindow));
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("Shibari Settings", "BoldLabel");

            ModelRecordList deserializedKludge = JsonUtility.FromJson<ModelRecordList>(PlayerPrefs.GetString(Model.PREFS_KEY));
            if (deserializedKludge == null)
                deserializedKludge = new ModelRecordList() { value = new ModelRecord[0] };

            if (deserializedKludge.value == null)
                deserializedKludge.value = new ModelRecord[0];

            List<ModelRecord> deserialized = deserializedKludge.value.ToList();

            var list = new UnityEditorInternal.ReorderableList(deserialized, typeof(ModelRecord));
            list.DoLayoutList();
            deserializedKludge.value = new ModelRecord[list.list.Count];
            list.list.CopyTo(deserializedKludge.value, 0);
            string serialized = JsonUtility.ToJson(deserializedKludge);
            if (deserializedKludge.value.Length > 0)
            {

            }
            PlayerPrefs.SetString(Model.PREFS_KEY, serialized);

            Model.LoadRecords();
        }
    }
}

namespace Shibari
{
    public class LocalizationData : IBindableData
    {
        public DataField<string> Language { get; private set; }
        public DataField<string> DefaultLanguage { get; private set; }

        public void Init(string key)
        {

        }
    }



    public static class Model
    {
        public const string PREFS_KEY = "SHIBARI_MODEL_RECORDS";

        public static LocalizationData Localization { get; private set; }

        private static Dictionary<Type, Tuple<string, Type>[]> modelTree;

        private static ModelRecordList records;

        public static void Init()
        {
            LoadRecords();

            foreach (var record in records.value)
            {
                Register(record.key, (IBindableData)record.type.Type.GetConstructor(new Type[0]).Invoke(new object[0]));
            }
        }

        [DidReloadScripts]
        private static void OnDidReloadScripts()
        {
            LoadRecords();
        }

        public static void LoadRecords()
        {
            string serialized = PlayerPrefs.GetString(PREFS_KEY);

            records = JsonUtility.FromJson<ModelRecordList>(serialized);

            if (records == null)
                records = new ModelRecordList() { value = new ModelRecord[0] };

            if (records.value == null)
                records.value = new ModelRecord[0];

            var groups = records.value.GroupBy(r => r.key);
            foreach (var group in groups)
            {
                var record = group.First();
                if (group.Take(2).Count() == 2)
                    Debug.LogErrorFormat("Found multiple datas with id {0}, using first.", record.key);
            }
            records.value = groups.Select(g => g.First()).ToArray();

            modelTree = new Dictionary<Type, Tuple<string, Type>[]>();

            var executingAssembly = Assembly.GetExecutingAssembly();
            ProcessTypes(executingAssembly.GetTypes());

            foreach (var assembly in executingAssembly.GetReferencedAssemblies())
                ProcessTypes(Assembly.Load(assembly).GetTypes());
            Debug.Log("Types reloaded");
        }

        private static void SaveRecords()
        {
            PlayerPrefs.SetString(PREFS_KEY, JsonUtility.ToJson(records));
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