using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.FSM;

namespace VillageKeeper.UI
{
    public class HelpMenuScript : MonoBehaviour
    {
        public enum Modes
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
                    CoreScript.Instance.FSM.Event(StateMachineEvents.GoToBuild);
                    break;
                case Modes.Battle:
                    CoreScript.Instance.FSM.Event(StateMachineEvents.GoToBattle);
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

        void Start()
        {
            offscreen = GetComponent<OffScreenMenuScript>() as OffScreenMenuScript;

            nextButton.onClick.AddListener(() =>
            {
                CurrentTipNumber++;
                CoreScript.Instance.AudioManager.PlayClick();
            });

            previousButton.onClick.AddListener(() =>
            {
                CurrentTipNumber--;
                CoreScript.Instance.AudioManager.PlayClick();
            });

            closeButton.onClick.AddListener(() =>
            {
                OnCloseClick();
                CoreScript.Instance.AudioManager.PlayClick();
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
            currentTips = CoreScript.Instance.Localization.GetHelpTips(mode);
            CurrentTipNumber = 0;
            offscreen.Show();
        }

        public void Hide()
        {
            offscreen.Hide();
        }
    }
}