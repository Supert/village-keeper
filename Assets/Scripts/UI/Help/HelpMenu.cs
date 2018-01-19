using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.FSM;
using VillageKeeper.Model;

namespace VillageKeeper.UI
{
    public class HelpMenu : MonoBehaviour
    {
        public Button previousButton;
        public Button nextButton;
        
        private void Start()
        {
            nextButton.onClick.AddListener(() =>
            {
                Data.Game.CurrentHelpTipIndex.Set(Data.Game.CurrentHelpTipIndex.Get() + 1);
                Core.Instance.AudioManager.PlayClick();
            });

            previousButton.onClick.AddListener(() =>
            {
                Data.Game.CurrentHelpTipIndex.Set(Data.Game.CurrentHelpTipIndex.Get() - 1);
                Core.Instance.AudioManager.PlayClick();
            });

            Core.Instance.FSM.SubscribeToEnter(States.BattleHelp, Show);
            Core.Instance.FSM.SubscribeToEnter(States.BuildHelp, Show);
        }

        private void Show()
        {
            Data.Game.CurrentHelpTipIndex.Set(0);
        }
    }
}