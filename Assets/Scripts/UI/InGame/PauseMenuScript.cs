using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.Model;

namespace VillageKeeper.UI
{
    public class PauseMenuScript : OffScreenMenuScript
    {
        public GameObject roundFinishedText;
        public Text WeCollectedFoodText;
        public Text WithMonsterBonusText;
        public Text YouGainGoldText;

        protected override void Start()
        {
            base.Start();
            
            Core.Instance.FSM.SubscribeToEnter(FSM.States.Pause, ShowPause);
            Core.Instance.FSM.SubscribeToExit(FSM.States.RoundFinished, ShowRoundFinished);
        }

        public void ShowPause()
        {
            roundFinishedText.SetActive(false);
        }

        public void ShowRoundFinished()
        {
            roundFinishedText.SetActive(true);
            WeCollectedFoodText.text = "We collected " + (Data.Game.TotalFood.Get());
            WithMonsterBonusText.text = "With " + Data.Balance.MonsterBonusGold.Get().ToString();
            YouGainGoldText.text = "you gain " + Data.Game.RoundFinishedBonusGold.Get().ToString();
        }
    }
}