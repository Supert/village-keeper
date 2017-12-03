using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.FSM;

namespace VillageKeeper.UI
{
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
            var monstersDefeated = CoreScript.Instance.SavedData.MonstersDefeated.Get();
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
            if (CoreScript.Instance.SavedData.HasPremium.Get())
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

        public void ShowMenu()
        {
            SetFurniture();
            SetScores();
            offScreenMenu.Show();
        }

        public void HideMenu()
        {
            offScreenMenu.Hide();
        }

        public void ShowShop()
        {
            ShowMenu();
            offScreenMenu.Show();
            shopShadow.Show();
        }

        public void HideShop()
        {
            shopShadow.Hide();
        }

        void Init()
        {
            offScreenMenu = GetComponent<OffScreenMenuScript>() as OffScreenMenuScript;
            SetScores();
            SetFurniture();
            shopShadow.ShadowButton.onClick.AddListener(() =>
            {
                CoreScript.Instance.FSM.Event(StateMachineEvents.GoToMenu);
            });

            CoreScript.Instance.SavedData.HasPremium.OnValueChanged += SetFurniture;
        }
    }
}