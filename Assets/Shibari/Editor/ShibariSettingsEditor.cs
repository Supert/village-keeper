using UnityEngine;
using UnityEditor;

namespace Shibari.Editor
{
    [CustomEditor(typeof(ShibariSettings))]
    public class ShibariSettingsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var prefab = PrefabUtility.GetPrefabObject(target);
            if (PrefabUtility.GetPrefabParent(target) == null && prefab != null && AssetDatabase.GetAssetPath(prefab) == Model.SETTINGS_PATH)
            {
                base.OnInspectorGUI();
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal();
                float w = Screen.width;
                EditorGUILayout.LabelField("Record Name", GUILayout.Width(w * 0.2f - 22f));
                EditorGUILayout.LabelField("Record Type", GUILayout.Width(w * 0.4f + 2f));
                EditorGUILayout.LabelField("Resource Path To Serialized Default Values", GUILayout.Width(w * 0.4f));
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
                if (GUILayout.Button("Apply"))
                {
                    Shibari.Model.LoadRecords();
                }
            }
            else
            {
                EditorGUILayout.LabelField($"Please, make sure that you edit {Model.SETTINGS_PATH} directly");
            }
        }
    }
}