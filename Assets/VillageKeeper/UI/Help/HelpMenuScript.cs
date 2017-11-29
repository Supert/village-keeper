using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.FSM;

namespace VillageKeeper.UI
{
    public class HelpMenuScript : MonoBehaviour
    {
        private enum Modes
        {
            Build,
            Battle,
        }

        private Modes mode;

        protected void OnCloseClick()
        {
            switch (mode)
            {
                case Modes.Build:
                    CoreScript.Instance.FSM.Event(new Args(Args.Types.GoToBuild));
                    break;
                case Modes.Battle:
                    CoreScript.Instance.FSM.Event(new Args(Args.Types.GoToBattle));
                    break;
            }

        }

        private OffScreenMenuScript offscreen;

        public Text tipText;
        public Text tipCounterText;
        public Button closeButton;
        public Button previousButton;
        public Button nextButton;

        private int _currentTipNumber;
        public int CurrentTipNumber
        {
            get
            {
                return _currentTipNumber;
            }
            private set
            {
                _currentTipNumber = value;
                if (_currentTipNumber == 0)
                    previousButton.gameObject.SetActive(false);
                else
                    previousButton.gameObject.SetActive(true);
                if (_currentTipNumber == currentTips.Length - 1)
                    nextButton.gameObject.SetActive(false);
                else
                    nextButton.gameObject.SetActive(true);
                tipText.text = currentTips[_currentTipNumber];
                tipCounterText.text = "Tip " + (_currentTipNumber + 1).ToString() + "/" + currentTips.Length;

            }
        }

        private string[] currentTips;
        private string[] inBuildModeTips = new string[] {
        "Welcome to Village Keeper, Keeper! We just settled down here, in beautiful Unknown.",
        "Drag and drop farm to build it. Build defenses, too.",
        "One or two wooden watchtowers behind stockade would be enough at first.",
        "Note that enemies always come from right side.",
        "Click red button at top to read these tips again. Good luck, Keeper!"
    };

        private string[] inBattleTips = new string[] {
        "Whoa! This monster is huge! Well, the bigger it is, the harder it fall.",
        "Do you see me standing at cliff? Swipe there to the left to draw a bow, then click at monster to shoot.",
        "Don't hesitate to shoot as fast as you can. We are not short of arrows.",
        "Click red button at top to read these tips again. To arms, Keeper!"
    };

        void Start()
        {
            offscreen = GetComponent<OffScreenMenuScript>() as OffScreenMenuScript;

            nextButton.onClick.AddListener(() =>
            {
                CurrentTipNumber++;
                CoreScript.Instance.Audio.PlayClick();
            });

            previousButton.onClick.AddListener(() =>
            {
                CurrentTipNumber--;
                CoreScript.Instance.Audio.PlayClick();
            });

            closeButton.onClick.AddListener(() =>
            {
                OnCloseClick();
                CoreScript.Instance.Audio.PlayClick();
            });
        }

        public void ShowBattle()
        {
            Show(Modes.Battle);
        }

        public void ShowBuild()
        {
            Show(Modes.Build);
        }

        private void Show(Modes mode)
        {
            this.mode = mode;
            switch (mode)
            {
                case Modes.Build:
                    currentTips = inBuildModeTips;
                    break;
                case Modes.Battle:
                    currentTips = inBattleTips;
                    break;
            }
            CurrentTipNumber = 0;
            offscreen.Show();
        }

        public void Hide()
        {
            offscreen.Hide();
        }
    }
}