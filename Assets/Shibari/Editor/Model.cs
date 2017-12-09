using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Shibari.Editor
{
    [InitializeOnLoad]
    public class Model : UnityEditor.AssetModificationProcessor
    {
        public const string SETTINGS_PATH = "Assets/Shibari/Resources/ShibariSettings.prefab";

        static string[] OnWillSaveAssets(string[] paths)
        {
            if (paths.Any(p => p == SETTINGS_PATH))
                Shibari.Model.LoadRecords();
            return paths;
        }


        Model()
        {
            PrefabUtility.prefabInstanceUpdated += OnPrefabInstanceUpdate;
        }

        static void OnPrefabInstanceUpdate(GameObject instance)
        {
            GameObject prefab = PrefabUtility.GetPrefabParent(instance) as GameObject;

            ShibariSettings settings = prefab.GetComponent<ShibariSettings>();
            if (settings == null)
                return;

            string prefabPath = AssetDatabase.GetAssetPath(prefab);

            if (prefabPath != SETTINGS_PATH)
                Debug.Log($"Please, locate your shibari settings in \"{SETTINGS_PATH}\"");

            Shibari.Model.LoadRecords();
        }

        [DidReloadScripts]
        private static void OnDidReloadScripts()
        {
            Shibari.Model.LoadRecords();
        }
    }
}
