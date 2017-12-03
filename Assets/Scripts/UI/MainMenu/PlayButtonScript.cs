using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    public class PlayButtonScript : MonoBehaviour
    {
        void Start()
        {
            var button = GetComponent<Button>() as Button;
            button.onClick.AddListener(() =>
            {
                Core.Instance.FSM.Event(FSM.StateMachineEvents.GoToBuild);
                Core.Instance.AudioManager.PlayClick();
            });
        }
    }
}