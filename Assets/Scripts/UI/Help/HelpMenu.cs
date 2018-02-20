using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.FSM;
using VillageKeeper.Model;

namespace VillageKeeper.UI
{
    public class HelpMenu : MonoBehaviour
    {        
        private void Start()
        {
            Core.Instance.FSM.SubscribeToEnter(States.BattleHelp, Show);
            Core.Instance.FSM.SubscribeToEnter(States.BuildHelp, Show);
        }

        private void Show()
        {
            Core.Data.Game.CurrentHelpTipIndex.Set(0);
        }
    }
}