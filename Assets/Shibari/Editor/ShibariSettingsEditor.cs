using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Shibari.Editor
{
    [CustomEditor(typeof(ShibariSettings))]
    public class ShibariSettingsEditor : UnityEditor.Editor
    {
        private ReorderableList valuesList;

        private void OnEnable()
        {
            valuesList = new ReorderableList(serializedObject,
                    serializedObject.FindProperty("values"),
                    true, true, true, true);
            valuesList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                float w = Screen.width;
                var element = valuesList.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                EditorGUI.PropertyField(
                    new Rect(rect.x, rect.y, w * 0.35f - 22f, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("key"), GUIContent.none);
                EditorGUI.PropertyField(
                    new Rect(w * 0.35f + 20f, rect.y, w * 0.65f - 32f, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("type"), GUIContent.none);
            };
        }

        public override void OnInspectorGUI()
        {
            var prefab = PrefabUtility.GetPrefabObject(target);
            if (PrefabUtility.GetPrefabParent(target) == null && prefab != null && AssetDatabase.GetAssetPath(prefab) == Model.SETTINGS_PATH)
            {
                EditorGUILayout.BeginHorizontal();
                float w = Screen.width;
                EditorGUILayout.LabelField("Record name", GUILayout.Width(w * 0.35f - 22f));
                EditorGUILayout.LabelField("Record type", GUILayout.Width(w * 0.65f + 2f));
                EditorGUILayout.EndHorizontal();

                serializedObject.Update();
                valuesList.DoLayoutList();
                serializedObject.ApplyModifiedProperties();

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Refresh Templates"))
                {
                    Model.RefreshModel();
                    Model.RefreshTemplates();
                }

                if (GUILayout.Button("Apply"))
                {
                    Model.RefreshModel();
                }
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.LabelField($"Please, make sure that you edit {Model.SETTINGS_PATH} directly");
            }
        }
    }
}