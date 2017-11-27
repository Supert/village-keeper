using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Text monstersDefeatedText;
    public ScreenShadowScript shopShadow;
    OffScreenMenuScript offScreenMenu;
    public Image roomFurniture;
    public Sprite freeFurniture;
    public Sprite premiumFurniture;

    void SetScores()
    {
        var monstersDefeated = CoreScript.Instance.Data.MonstersDefeated;
        if (monstersDefeated == 0)
            monstersDefeatedText.text = "No monsters defeated yet";
        else
        {
            if (monstersDefeated == 1)
                monstersDefeatedText.text = "First monster defeated!";
            else
                monstersDefeatedText.text = monstersDefeated + " monsters defeated";
        }
    }

    private void SetFurniture()
    {
        if (CoreScript.Instance.Data.HasPremium)
        {
            if (roomFurniture.sprite != premiumFurniture)
                roomFurniture.sprite = premiumFurniture;
        }
        else
        {
            if (roomFurniture.sprite != freeFurniture)
                roomFurniture.sprite = freeFurniture;
        }
    }

    void Start()
    {
        offScreenMenu = GetComponent<OffScreenMenuScript>() as OffScreenMenuScript;
        SetScores();
        SetFurniture();
        shopShadow.ShadowButton.onClick.AddListener(() =>
        {
            CoreScript.Instance.GameState = CoreScript.GameStates.InMenu;
        });
        CoreScript.Instance.GameStateChanged += (sender, e) =>
        {
            switch (e.NewState)
            {
                case CoreScript.GameStates.InBuildMode:
                case CoreScript.GameStates.Paused:
                case CoreScript.GameStates.RoundFinished:
                case CoreScript.GameStates.InBattle:
                    offScreenMenu.Hide();
                    shopShadow.Hide();
                    break;
                case CoreScript.GameStates.InMenu:
                    SetFurniture();
                    SetScores();
                    offScreenMenu.Show();
                    shopShadow.Hide();
                    break;
                case CoreScript.GameStates.InShop:
                    offScreenMenu.Show();
                    shopShadow.Show();
                    break;
            }
        };

        CoreScript.Instance.Data.DataFieldChanged += (sender, e) =>
        {
            switch (e.FieldChanged)
            {
                case DataScript.DataFields.HasPremium:
                    SetFurniture();
                    break;
                default:
                    break;
            }
        };
    }
}
