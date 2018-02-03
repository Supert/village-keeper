﻿using Shibari.UI;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Shibari.Editor
{
    [CustomEditor(typeof(BindableView), true)]
    public class BindableViewEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BindableView actualObject = serializedObject.targetObject as System.Object as BindableView;
            actualObject.Initialize();
            var controller = target as BindableView;
            SerializedProperty tps = serializedObject.FindProperty("bindableValuesIds");
            EditorGUI.BeginChangeCheck();
            for (int i = 0; i < tps.arraySize; i++)
                EditorGUILayout.PropertyField(tps.GetArrayElementAtIndex(i), true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
        }
    }
}