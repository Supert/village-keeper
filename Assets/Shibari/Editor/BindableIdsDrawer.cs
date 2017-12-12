using System;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Shibari.Editor
{
    [CustomPropertyDrawer(typeof(BindableIds))]
    public class BindableIdsDrawer : PropertyDrawer
    {
        private static readonly int s_ControlHint = typeof(BindableIdsDrawer).GetHashCode();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty dataId = property.FindPropertyRelative("dataId");
            SerializedProperty fieldId = property.FindPropertyRelative("fieldId");

            string[] models = Shibari.Model.Records.Select(r => r.key).ToArray();
            int selectedModel = models.TakeWhile(m => m != dataId.stringValue).Count();
            if (selectedModel == models.Length)
                selectedModel = -1;

            string[] typeTips = Shibari.Model.Records.Select(r => r.type.ToString()).ToArray();

            Tuple<string, Type>[] fields = new Tuple<string, Type>[0];
            if (selectedModel >= 0)
                fields = Shibari.Model.ModelTree[Shibari.Model.Records.FirstOrDefault(id => id.key == models[selectedModel]).type.Type].ToArray();

            int selectedField = fields.TakeWhile(f => f.Item1 != fieldId.stringValue).Count();
            if (selectedField == fields.Length)
                selectedField = -1;

            Rect rect = new Rect(position.position, new Vector2(position.width / 2f, position.height));
            int newModel = EditorGUI.Popup(rect, selectedModel, models);

            rect.position += Vector2.right * rect.width;
            int newField = EditorGUI.Popup(rect, selectedField, fields.Select(kvp => $"{kvp.Item1} - {kvp.Item2}").ToArray());

            if (newModel != selectedModel)
            {
                if (newModel != -1)
                    dataId.stringValue = models[newModel];
                fieldId.stringValue = "";
            }
            else
            {
                if (newField != -1)
                    fieldId.stringValue = fields[newField].Item1;
            }
        }
    }
}