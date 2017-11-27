using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutosaveOnRun
{
    static AutosaveOnRun()
    {
        EditorApplication.playModeStateChanged += (change) =>
        {
            if (change == PlayModeStateChange.ExitingEditMode)
            {
                var scene = SceneManager.GetActiveScene();

                Debug.Log("Saving scene before entering Play mode: " + scene);

                EditorSceneManager.SaveScene(scene);
                AssetDatabase.SaveAssets();
            }
        };
    }
}