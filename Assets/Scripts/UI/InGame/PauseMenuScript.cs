using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    public class PauseMenuScript : OffScreenMenuScript
    {
        private enum Modes
        {
            Pause,
            RoundFinished,
        }

        private Modes mode;

        public Text title;
        public Button homeButton;
        public Button continueButton;
        public GameObject roundFinishedText;
        public Text WeCollectedFoodText;
        public Text WithMonsterBonusText;
        public Text YouGainGoldText;

        protected override void Start()
        {
            base.Start();

            continueButton.onClick.AddListener(() =>
            {
                switch (mode)
                {
                    case Modes.Pause:
                        Core.Instance.FSM.Event(FSM.StateMachineEvents.GoToBattle);
                        break;
                    case Modes.RoundFinished:
                        Core.Instance.FSM.Event(FSM.StateMachineEvents.GoToBuild);
                        break;
                }
                Core.Instance.AudioManager.PlayClick();
            });

            homeButton.onClick.AddListener(() =>
            {
                Core.Instance.FSM.Event(FSM.StateMachineEvents.GoToMenu);
                Core.Instance.AudioManager.PlayClick();
            });

            Core.Instance.FSM.SubscribeToEnter(FSM.States.Pause, ShowPause);
            Core.Instance.FSM.SubscribeToExit(FSM.States.RoundFinished, ShowRoundFinished);
        }

        public void ShowPause()
        {
            mode = Modes.Pause;
            title.text = "Pause";
            roundFinishedText.SetActive(false);
        }

        public void ShowRoundFinished()
        {
            mode = Modes.RoundFinished;
            title.text = "Victory!";
            roundFinishedText.SetActive(true);
            WeCollectedFoodText.text = "We collected " + (Core.Instance.GameData.TotalFood.Get());
            WithMonsterBonusText.text = "With " + Core.Instance.CommonData.MonsterBonusGold.Get().ToString();
            YouGainGoldText.text = "you gain " + Core.Instance.GameData.RoundFinishedBonusGold.Get().ToString();
        }
    }
}