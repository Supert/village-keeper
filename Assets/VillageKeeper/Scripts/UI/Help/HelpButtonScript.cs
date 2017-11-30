using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.Game;

namespace VillageKeeper.UI
{
    public class HelpButtonScript : MonoBehaviour
    {
        void Start()
        {
            var button = GetComponent<Button>() as Button;
            button.onClick.AddListener(() =>
            {
                if (CoreScript.Instance.FSM.Current == FSM.States.Battle)
                    CoreScript.Instance.FSM.Event(FSM.StateMachineEvents.ShowBattleHelp);
                else
                    if (CoreScript.Instance.FSM.Current == FSM.States.Build)
                    CoreScript.Instance.FSM.Event(FSM.StateMachineEvents.ShowBuildHelp);
                CoreScript.Instance.AudioManager.PlayClick();
            });
        }
    }
}