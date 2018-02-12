using UnityEditor;

namespace Shibari.Editor
{
    public class ShibariSettingsWindow : EditorWindow
    {
        [MenuItem("Shibari/Settings")]
        private static void GetWindow()
        {
            ShibariSettingsWindow window = (ShibariSettingsWindow)GetWindow(typeof(ShibariSettingsWindow));
            window.Show();
        }

        void OnGUI()
        {
            var editor = UnityEditor.Editor.CreateEditor(AssetDatabase.LoadAssetAtPath<ShibariSettings>(Model.SETTINGS_PATH));

            if (editor != null)
            {
                var so = editor.serializedObject;
                so.Update();
                editor.OnInspectorGUI();

                so.ApplyModifiedProperties();

                AssetDatabase.SaveAssets();
            }
        }
    }
}