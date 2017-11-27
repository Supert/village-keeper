using UnityEngine;
using UnityEngine.UI;

public class ShopButtonScript : MonoBehaviour
{
    void Start()
    {
        var button = GetComponent<Button>() as Button;
        button.onClick.AddListener(() =>
        {
            CoreScript.Instance.GameState = CoreScript.GameStates.InShop;
            CoreScript.Instance.Audio.PlayClick();
        });
    }
}
