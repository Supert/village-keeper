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

        private int currentTipNumber;
        public int CurrentTipNumber
        {
            get
            {
                return currentTipNumber;
            }
            private set
            {
                currentTipNumber = value;

                previousButton.gameObject.SetActive(currentTipNumber > 0);
                nextButton.gameObject.SetActive(currentTipNumber < currentTips.Length - 1);

                tipText.text = currentTips[currentTipNumber];
                tipCounterText.text = "Tip " + (currentTipNumber + 1).ToString() + "/" + currentTips.Length;
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

        public void Show(Modes mode)
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