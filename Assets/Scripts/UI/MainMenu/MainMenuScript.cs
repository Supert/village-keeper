using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.Model;

namespace VillageKeeper.UI
{
    public class MainMenuScript : MonoBehaviour
    {
        public Text monstersDefeatedText;

        void SetScores()
        {
            var monstersDefeated = Data.Saved.SlainedMonstersCount;
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

        public void ShowMenu()
        {
            SetScores();
        }

        void Init()
        {
            SetScores();

            Data.Saved.SlainedMonstersCount.OnValueChanged += SetScores;
        }
    }
}