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
                Core.Instance.Data.Game.CurrentHelpTip.Set(Core.Instance.Data.Game.CurrentHelpTip.Get() + 1);
                Core.Instance.AudioManager.PlayClick();
            });

            previousButton.onClick.AddListener(() =>
            {
                Core.Instance.Data.Game.CurrentHelpTip.Set(Core.Instance.Data.Game.CurrentHelpTip.Get() - 1);
                Core.Instance.AudioManager.PlayClick();
            });

            Core.Instance.Data.Game.CurrentHelpTip.OnValueChanged += () =>
            {
                int tip = Core.Instance.Data.Game.CurrentHelpTip.Get();
                
                tipText.text = currentTips[Core.Instance.Data.Game.CurrentHelpTip.Get() - 1];
                tipCounterText.text = "Tip " + tip.ToString() + Core.Instance.Data.Game.CurrentHelpTip.Get() + "/" + currentTips.Length;
            };

            Core.Instance.FSM.SubscribeToEnter(States.BattleHelp, Show);
            Core.Instance.FSM.SubscribeToEnter(States.BuildHelp, Show);
        }

        private void Show()
        {
            Core.Instance.Data.Game.HelpTipsCount.Set(currentTips.Length);
            Core.Instance.Data.Game.CurrentHelpTip.Set(1);
        }
    }
}