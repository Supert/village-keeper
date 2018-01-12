using UnityEngine;
using UnityEngine.UI;

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
            var monstersDefeated = Core.Instance.Data.Saved.MonstersDefeated.Get();
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
            if (Core.Instance.Data.Saved.HasPremium.Get())
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

            Core.Instance.Data.Saved.MonstersDefeated.OnValueChanged += SetScores;
            Core.Instance.Data.Saved.HasPremium.OnValueChanged += SetFurniture;
        }
    }
}