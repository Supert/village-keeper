using UnityEngine;
using System.Collections;
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
                        CoreScript.Instance.FSM.Event(new FSM.Args(FSM.Args.Types.GoToBattle));
                        break;
                    case Modes.RoundFinished:
                        CoreScript.Instance.FSM.Event(new FSM.Args(FSM.Args.Types.GoToBuild));
                        break;
                }
                CoreScript.Instance.Audio.PlayClick();
            });

            homeButton.onClick.AddListener(() =>
            {
                CoreScript.Instance.FSM.Event(new FSM.Args(FSM.Args.Types.GoToMenu));
                CoreScript.Instance.Audio.PlayClick();
            });
        }

        public void ShowPause()
        {
            mode = Modes.Pause;
            Show();
            title.text = "Pause";
            roundFinishedText.SetActive(false);
        }

        public void ShowRoundFinished()
        {
            mode = Modes.RoundFinished;
            Show();
            title.text = "Victory!";
            roundFinishedText.SetActive(true);
            WeCollectedFoodText.text = "We collected " + (CoreScript.Instance.Data.GetFarmsFood() + CoreScript.Instance.Data.GetWindmillBonusFood()).ToString();
            WithMonsterBonusText.text = "With " + CoreScript.Instance.Data.GetMonsterBonusGold().ToString();
            YouGainGoldText.text = "you gain " + CoreScript.Instance.Data.GetRoundFinishedBonusGold().ToString();
        }
    }
}