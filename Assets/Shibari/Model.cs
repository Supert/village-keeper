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
        public static BindableData RootNode { get; private set; }
        public static Type RootNodeType { get; private set; }

        static Model()
        {
            Initialize();
        }

        public static void Initialize()
        {
            ShibariSettings settings = Resources.Load<ShibariSettings>("ShibariSettings");
            RootNodeType = settings.RootNodeType.Type;
            if (RootNodeType == null)
            {
                Debug.LogError("Please, set root node type in Shibari/Settings menu.");
                return;
            }
            else if (RootNodeType.GetConstructor(new Type[0]) == null)
            {
                Debug.LogError($"Root node type {RootNodeType} should implement default public constructor");
            }
            
            //RootNode = (BindableData) Activator.CreateInstance(RootNodeType);
        }

        //public static void LoadRecords()
        //{

        //    if (settings == null)
        //        Debug.LogError("Could not find container prefab for Shibari Settings.");

        //    if (settings.values == null)
        //        settings.values = new List<ModelRecord>();

        //    var groups = settings.values.Where(r => r != null).GroupBy(r => r.key);
        //    foreach (var group in groups)
        //    {
        //        var record = group.First();
        //        if (group.Take(2).Count() == 2)
        //            Debug.LogErrorFormat("Found multiple datas with id {0}, ignoring duplicates.", record.key);
        //    }

        //    FullModelTree = new Dictionary<Type, BindableValueReflection[]>();
        //    VisibleInEditorModelTree = new Dictionary<Type, BindableValueReflection[]>();

        //    var executingAssembly = Assembly.GetExecutingAssembly();
        //    ProcessBindableDataTypes(executingAssembly.GetTypes());

        //    foreach (var assembly in executingAssembly.GetReferencedAssemblies())
        //        ProcessBindableDataTypes(Assembly.Load(assembly).GetTypes());
        //}

        //private static void ProcessBindableDataTypes(Type[] types)
        //{
        //    foreach (var type in types.Where(t => !t.IsAbstract).Where(t => typeof(BindableData).IsAssignableFrom(t)))
        //    {
        //        var fullModel = new List<BindableValueReflection>();
        //        var visibleModel = new List<BindableValueReflection>();
        //        if (type.GetConstructor(new Type[0]) == null)
        //            Debug.LogErrorFormat("Type {0} has to implement parameterless constructor.", type.FullName);

        //        foreach (var p in BindableData.GetBindableValues(type))
        //        {
        //            Type t = p.PropertyType;
        //            while (!(t.IsGenericType && t.GetGenericTypeDefinition() == typeof(BindableValue<>)))
        //            {
        //                t = t.BaseType;
        //            }
        //            fullModel.Add(new BindableValueReflection(p.Name, p.PropertyType, t.GetGenericArguments()[0]));
        //            if (p.GetCustomAttribute(typeof(ShowInEditorAttribute)) != null)
        //                visibleModel.Add(new BindableValueReflection(p.Name, p.PropertyType, t.GetGenericArguments()[0]));
        //        }

        //        if (fullModel.Any())
        //            FullModelTree[type] = fullModel.ToArray();
        //        if (visibleModel.Any())
        //            VisibleInEditorModelTree[type] = visibleModel.ToArray();
        //    }
        //}

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