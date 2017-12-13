using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    public class PauseMenuScript : OffScreenMenuScript
    {
        public Text title;
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
            title.text = "Pause";
            roundFinishedText.SetActive(false);
        }

        public void ShowRoundFinished()
        {
            title.text = "Victory!";
            roundFinishedText.SetActive(true);
            WeCollectedFoodText.text = "We collected " + (Core.Instance.Data.Game.TotalFood.Get());
            WithMonsterBonusText.text = "With " + Core.Instance.Data.Balance.MonsterBonusGold.Get().ToString();
            YouGainGoldText.text = "you gain " + Core.Instance.Data.Game.RoundFinishedBonusGold.Get().ToString();
        }
    }
}