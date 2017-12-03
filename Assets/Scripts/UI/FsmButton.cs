using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    [RequireComponent(typeof(Button))]
    public class FsmButton : MonoBehaviour
    {
        [SerializeField]
        protected bool playClick;
        [SerializeField]
        protected FSM.StateMachineEvents onClickEvent;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                Core.Instance.FSM.Event(onClickEvent);
                if (playClick)
                    Core.Instance.AudioManager.PlayClick();
            });
        }
    }
}