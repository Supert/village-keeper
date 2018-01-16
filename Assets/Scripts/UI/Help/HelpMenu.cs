using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.FSM;

namespace VillageKeeper.UI
{
    public class HelpMenu : MonoBehaviour
    {
        public Text tipText;
        public Text tipCounterText;
        public Button previousButton;
        public Button nextButton;

        private string[] tips;
        
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

                tipText.text = tips[Core.Instance.Data.Game.CurrentHelpTip.Get() - 1];
                tipCounterText.text = string.Format(Core.Instance.Data.Localization.TipFormat.Get(), tip, tips.Length);
            };

            Core.Instance.FSM.SubscribeToEnter(States.BattleHelp, Show);
            Core.Instance.FSM.SubscribeToEnter(States.BuildHelp, Show);
        }

        private void Show()
        {
            if (Core.Instance.FSM.Current == States.BattleHelp)
                tips = Core.Instance.Data.Localization.BattleHelpTips.Get();
            else if (Core.Instance.FSM.Current == States.BuildHelp)
                tips = Core.Instance.Data.Localization.BuildHelpTips.Get();
            Core.Instance.Data.Game.HelpTipsCount.Set(tips.Length);
            Core.Instance.Data.Game.CurrentHelpTip.Set(1);
        }
    }
}