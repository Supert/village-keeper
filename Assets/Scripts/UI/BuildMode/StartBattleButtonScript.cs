using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    public class StartBattleButtonScript : MonoBehaviour
    {
        void Start()
        {
            var button = GetComponent<Button>() as Button;
            button.onClick.AddListener(() =>
            {
                Core.Instance.FSM.Event(FSM.StateMachineEvents.GoToBattle);
                Core.Instance.AudioManager.PlayClick();
            });
        }
    }
}