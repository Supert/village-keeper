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
                if (CoreScript.Instance.FSM.Current == typeof(FSM.BattleState))
                    CoreScript.Instance.FSM.Event(new FSM.Args(FSM.Args.Types.ShowBattleHelp));
                else
                    if (CoreScript.Instance.FSM.Current == typeof(FSM.BuildState))
                    CoreScript.Instance.FSM.Event(new FSM.Args(FSM.Args.Types.ShowBuildHelp));
                CoreScript.Instance.Audio.PlayClick();
            });
        }
    }
}