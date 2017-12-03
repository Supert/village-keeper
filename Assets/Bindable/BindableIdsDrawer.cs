using UnityEngine;
using UnityEditor;
using VillageKeeper.UI;

namespace VillageKeeper.Edit.UI
{
    [CustomPropertyDrawer(typeof(BindableIds))]
    public class BindableIdsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float oldLabelWidth = EditorGUIUtility.labelWidth;

            EditorGUIUtility.labelWidth = 65f;
            Rect rect = new Rect(position.x, position.y, Screen.width / 2f - 9f, position.height);
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(rect, property.FindPropertyRelative("dataId"), new GUIContent("Data Id"));

            EditorGUIUtility.labelWidth = 65f;
            rect = new Rect(position.x + Screen.width / 2f - 9f, position.y, Screen.width / 2f - 9f, position.height);
            EditorGUI.PropertyField(rect, property.FindPropertyRelative("fieldId"), new GUIContent("Field Id"));
            EditorGUI.EndProperty();
            EditorGUIUtility.labelWidth = oldLabelWidth;
        }
    }
}