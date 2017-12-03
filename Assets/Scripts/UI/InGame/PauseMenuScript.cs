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
                        CoreScript.Instance.FSM.Event(FSM.StateMachineEvents.GoToBattle);
                        break;
                    case Modes.RoundFinished:
                        CoreScript.Instance.FSM.Event(FSM.StateMachineEvents.GoToBuild);
                        break;
                }
                CoreScript.Instance.AudioManager.PlayClick();
            });

            homeButton.onClick.AddListener(() =>
            {
                CoreScript.Instance.FSM.Event(FSM.StateMachineEvents.GoToMenu);
                CoreScript.Instance.AudioManager.PlayClick();
            });

            CoreScript.Instance.FSM.SubscribeToEnter(FSM.States.Pause, ShowPause);
            CoreScript.Instance.FSM.SubscribeToExit(FSM.States.RoundFinished, ShowRoundFinished);
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
            WeCollectedFoodText.text = "We collected " + (CoreScript.Instance.GameData.TotalFood.Get());
            WithMonsterBonusText.text = "With " + CoreScript.Instance.GameData.MonsterBonusGold.Get().ToString();
            YouGainGoldText.text = "you gain " + CoreScript.Instance.GameData.RoundFinishedBonusGold.Get().ToString();
        }
    }
}