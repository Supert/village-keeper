using UnityEngine;
using UnityEditor;

namespace Shibari.Editor
{
    [CustomPropertyDrawer(typeof(ModelRecord))]
    public class ModelRecordPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(
                new Rect(position.x, position.y, position.width * 0.2f, EditorGUIUtility.singleLineHeight),
                property.FindPropertyRelative("key"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(position.x + position.width * 0.2f, position.y, position.width * 0.4f, EditorGUIUtility.singleLineHeight),
                property.FindPropertyRelative("type"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(position.x + position.width * 0.6f, position.y, position.width * 0.40f, EditorGUIUtility.singleLineHeight),
                property.FindPropertyRelative("defaultValuesPath"), GUIContent.none);
        }
    }
}
