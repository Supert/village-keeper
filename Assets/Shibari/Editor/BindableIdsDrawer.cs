using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Shibari.Editor
{
    [CustomPropertyDrawer(typeof(BindableIds))]
    public class BindableIdsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty dataId = property.FindPropertyRelative("dataId");
            SerializedProperty fieldId = property.FindPropertyRelative("fieldId");

            var bindableIds = PropertyDrawerUtility.GetActualObjectForSerializedProperty<BindableIds>(fieldInfo, property);
            
            string[] models = Shibari.Model.Records.Where(r => Shibari.Model.VisibleInEditorModelTree.ContainsKey(r.type.Type)).Select(r => r.key).ToArray();
            int selectedModel = models.TakeWhile(m => m != dataId.stringValue).Count();
            if (selectedModel == models.Length)
                selectedModel = -1;

            BindableValueReflection[] fields = new BindableValueReflection[0];
            if (selectedModel >= 0)
            {
                fields = Shibari.Model.VisibleInEditorModelTree[Shibari.Model.Records.FirstOrDefault(id => id.key == models[selectedModel]).type.Type]
                    .Where(t => bindableIds.allowedValueType.IsAssignableFrom(t.ValueType) && (!bindableIds.isSetterRequired || Shibari.Model.IsCalculatedValue(t.Type)))
                    .ToArray();
            }
            int selectedField = fields.TakeWhile(f => f.Name != fieldId.stringValue).Count();
            if (selectedField == fields.Length)
                selectedField = -1;

            Rect rect = new Rect(position.position, new Vector2(position.width / 2f, position.height));
            int newModel = EditorGUI.Popup(rect, selectedModel, models);

            rect.position += Vector2.right * rect.width;
            int newField = EditorGUI.Popup(rect, selectedField, fields.Select(kvp => $"{kvp.Name} - {kvp.ValueType}").ToArray());

            if (newModel != selectedModel)
            {
                if (newModel != -1)
                    dataId.stringValue = models[newModel];
                fieldId.stringValue = "";
            }
            else
            {
                if (newField != -1)
                    fieldId.stringValue = fields[newField].Name;
            }
        }
    }
}