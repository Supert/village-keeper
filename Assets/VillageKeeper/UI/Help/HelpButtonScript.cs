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
                if (CoreScript.Instance.FSM.Current == typeof(Game.FSM.BattleState))
                    CoreScript.Instance.FSM.Event(new Game.FSM.Args(Game.FSM.Args.Types.ShowBattleHelp));
                else
                    if (CoreScript.Instance.FSM.Current == typeof(Game.FSM.BuildState))
                    CoreScript.Instance.FSM.Event(new Game.FSM.Args(Game.FSM.Args.Types.ShowBuildHelp));
                CoreScript.Instance.Audio.PlayClick();
            });
        }
    }
}