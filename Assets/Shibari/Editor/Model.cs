using System.IO;
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
        public const string SERIALIZATION_TEMPLATES = "Assets/Shibari/Templates/";

        static string[] OnWillSaveAssets(string[] paths)
        {
            if (paths.Any(p => p == SETTINGS_PATH))
                RefreshModel();
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

            RefreshModel();
        }

        [DidReloadScripts]
        private static void OnDidReloadScripts()
        {
            RefreshModel();
        }

        public static void RefreshModel()
        {
            Shibari.Model.Initialize();
        }

        public static void RefreshTemplates()
        {
            throw new System.NotImplementedException();
            //foreach (var path in Directory.GetFiles(SERIALIZATION_TEMPLATES))
            //{
            //    FileInfo file = new FileInfo($"{path}");
            //    file.Delete();
            //}
            //foreach (var model in Shibari.Model.FullModelTree.Keys)
            //{
            //    FileInfo file = new FileInfo($"{SERIALIZATION_TEMPLATES}{model.FullName}.txt");
            //    file.Directory.Create();
            //    File.WriteAllText(file.FullName, Shibari.Model.GenerateSerializationTemplate(model));
            //}
            //AssetDatabase.Refresh();
        }
    }
}
