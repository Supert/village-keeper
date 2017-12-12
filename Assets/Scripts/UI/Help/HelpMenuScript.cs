using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.FSM;

namespace VillageKeeper.UI
{
    public class HelpMenuScript : MonoBehaviour
    {
        public Text tipText;
        public Text tipCounterText;
        public Button closeButton;
        public Button previousButton;
        public Button nextButton;

        private string[] currentTips;

        private void Start()
        {
            nextButton.onClick.AddListener(() =>
            {
                Core.Instance.GameData.CurrentHelpTip.Set(Core.Instance.GameData.CurrentHelpTip.Get() + 1);
                Core.Instance.AudioManager.PlayClick();
            });

            previousButton.onClick.AddListener(() =>
            {
                Core.Instance.GameData.CurrentHelpTip.Set(Core.Instance.GameData.CurrentHelpTip.Get() - 1);
                Core.Instance.AudioManager.PlayClick();
            });

            Core.Instance.GameData.CurrentHelpTip.OnValueChanged += () =>
            {
                int tip = Core.Instance.GameData.CurrentHelpTip.Get();
                
                tipText.text = currentTips[Core.Instance.GameData.CurrentHelpTip.Get() - 1];
                tipCounterText.text = "Tip " + tip.ToString() + Core.Instance.GameData.CurrentHelpTip.Get() + "/" + currentTips.Length;
            };

            Core.Instance.FSM.SubscribeToEnter(States.BattleHelp, Show);
            Core.Instance.FSM.SubscribeToEnter(States.BuildHelp, Show);
        }

        private void Show()
        {
            Core.Instance.GameData.HelpTipsCount.Set(currentTips.Length);
            Core.Instance.GameData.CurrentHelpTip.Set(1);
        }
    }
}