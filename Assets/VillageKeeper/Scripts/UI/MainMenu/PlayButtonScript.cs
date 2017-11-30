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
                CoreScript.Instance.FSM.Event(FSM.StateMachineEvents.GoToBuild);
                CoreScript.Instance.AudioManager.PlayClick();
            });
        }
    }
}