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
                new Rect(position.x, position.y, position.width * 0.35f, EditorGUIUtility.singleLineHeight),
                property.FindPropertyRelative("key"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(position.x + position.width * 0.35f, position.y, position.width * 0.65f, EditorGUIUtility.singleLineHeight),
                property.FindPropertyRelative("type"), GUIContent.none);
        }
    }
}
