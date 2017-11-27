using UnityEngine;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour
{
    void Start()
    {
        var button = GetComponent<Button>() as Button;
        button.onClick.AddListener(() =>
        {
            CoreScript.Instance.GameState = CoreScript.GameStates.InBuildMode;
            CoreScript.Instance.Audio.PlayClick();
        });
    }
}