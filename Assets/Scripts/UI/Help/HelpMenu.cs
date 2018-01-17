using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.FSM;
using VillageKeeper.Model;

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
                Data.Game.CurrentHelpTip.Set(Data.Game.CurrentHelpTip.Get() + 1);
                Core.Instance.AudioManager.PlayClick();
            });

            previousButton.onClick.AddListener(() =>
            {
                Data.Game.CurrentHelpTip.Set(Data.Game.CurrentHelpTip.Get() - 1);
                Core.Instance.AudioManager.PlayClick();
            });

            Data.Game.CurrentHelpTip.OnValueChanged += () =>
            {
                int tip = Data.Game.CurrentHelpTip.Get();

                tipText.text = tips[Data.Game.CurrentHelpTip.Get() - 1];
                tipCounterText.text = string.Format(Data.Localization.TipFormat.Get(), tip, tips.Length);
            };

            Core.Instance.FSM.SubscribeToEnter(States.BattleHelp, Show);
            Core.Instance.FSM.SubscribeToEnter(States.BuildHelp, Show);
        }

        private void Show()
        {
            if (Core.Instance.FSM.Current == States.BattleHelp)
                tips = Data.Localization.BattleHelpTips.Get();
            else if (Core.Instance.FSM.Current == States.BuildHelp)
                tips = Data.Localization.BuildHelpTips.Get();
            Data.Game.HelpTipsCount.Set(tips.Length);
            Data.Game.CurrentHelpTip.Set(1);
        }
    }
}