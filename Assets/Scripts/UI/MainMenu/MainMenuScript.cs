using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.FSM;

namespace VillageKeeper.UI
{
    public class MainMenuScript : MonoBehaviour
    {
        public Text monstersDefeatedText;
        public ScreenShadowScript shopShadow;
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
        }

        public void ShowShop()
        {
            shopShadow.Show();
        }

        public void HideShop()
        {
            shopShadow.Hide();
        }

        void Init()
        {
            SetScores();
            SetFurniture();
            shopShadow.ShadowButton.onClick.AddListener(() =>
            {
                CoreScript.Instance.FSM.Event(StateMachineEvents.GoToMenu);
            });

            CoreScript.Instance.SavedData.MonstersDefeated.OnValueChanged += SetScores;
            CoreScript.Instance.SavedData.HasPremium.OnValueChanged += SetFurniture;
        }
    }
}