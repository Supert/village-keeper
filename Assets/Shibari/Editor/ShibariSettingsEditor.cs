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
                EditorGUILayout.LabelField("Record name", GUILayout.Width(w * 0.35f - 22f));
                EditorGUILayout.LabelField("Record type", GUILayout.Width(w * 0.65f + 2f));
                //EditorGUILayout.LabelField("Resource path to serialized default values", GUILayout.Width(w * 0.4f));
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