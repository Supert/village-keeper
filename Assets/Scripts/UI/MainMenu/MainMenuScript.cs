using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.Model;

namespace VillageKeeper.UI
{
    public class MainMenuScript : MonoBehaviour
    {
        public Text monstersDefeatedText;
        public Image roomFurniture;
        public Sprite freeFurniture;
        public Sprite premiumFurniture;

        void SetScores()
        {
            var monstersDefeated = Data.Saved.MonstersDefeated;
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
            if (Data.Saved.HasPremium)
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

        void Init()
        {
            SetScores();
            SetFurniture();

            Data.Saved.MonstersDefeated.OnValueChanged += SetScores;
            Data.Saved.HasPremium.OnValueChanged += SetFurniture;
        }
    }
}