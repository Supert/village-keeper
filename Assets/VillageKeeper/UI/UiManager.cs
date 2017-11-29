using UnityEngine;

namespace VillageKeeper.UI
{
    public class UiManager : MonoBehaviour
    {
        private CastleScript castle;
        private HelpMenuScript help;
        private GoldLabelScript gold;
        private InfoAndUpgradesWindowScript castleInfoAndUpgrades;
        private MonsterHealthBarScript monsterHealthBar;

        public void OnBuildModeEntered()
        {
            castle.SetSprite();
            gold.SetText();

            castleInfoAndUpgrades.SetValues();
            castleInfoAndUpgrades.Show();
        }

        public void OnBuildHelpEntered()
        {
            help.ShowBuild();
        }

        public void OnBattleHelpEntered()
        {
            help.ShowBattle();
        }

        public void OnBattleEntered()
        {
            monsterHealthBar.Show();
        }
    }
}