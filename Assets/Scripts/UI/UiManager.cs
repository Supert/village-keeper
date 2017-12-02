using UnityEngine;

namespace VillageKeeper.UI
{
    public class UiManager : MonoBehaviour
    {
        public WindScript Wind { get; private set; }

        private HelpMenuScript help;
        private InfoAndUpgradesWindowScript castleInfoAndUpgrades;
        private MonsterHealthBarScript monsterHealthBar;

        public void Init()
        {
            Wind = FindObjectOfType(typeof(WindScript)) as WindScript;
        }

        public void OnBuildModeEntered()
        {
            castleInfoAndUpgrades.SetValues();
            castleInfoAndUpgrades.Show();
        }

        public void OnBuildHelpEntered()
        {
            help.Show(HelpMenuScript.Modes.Build);
        }

        public void OnBattleHelpEntered()
        {
            help.Show(HelpMenuScript.Modes.Battle);
        }

        public void OnBattleEntered()
        {
            monsterHealthBar.Show();
        }
    }
}