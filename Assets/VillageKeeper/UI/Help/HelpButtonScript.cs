using UnityEngine;
using UnityEngine.UI;

public class HelpButtonScript : MonoBehaviour
{
    void Start()
    {
        var button = GetComponent<Button>() as Button;
        button.onClick.AddListener(() =>
        {
            CoreScript.Instance.GameState = CoreScript.GameStates.InHelp;
            CoreScript.Instance.Audio.PlayClick();
        });
    }

    void Update()
    {

    }
}