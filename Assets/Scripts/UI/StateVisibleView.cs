using UnityEngine;

namespace VillageKeeper.UI
{
    public abstract class StateVisibleView : MonoBehaviour
    {
        [SerializeField]
        protected FSM.States[] showAtStates;

        [SerializeField]
        protected float animationTime;

        [SerializeField]
        protected bool isShownAtStart;

        private bool isShown;
        public bool IsShown
        {
            get
            {
                return isShown;
            }
            protected set
            {
                if (value)
                    gameObject.SetActive(true);
                isShown = value;
            }
        }

        public void Show()
        {
            IsShown = true;
        }

        public void Hide()
        {
            IsShown = false;
        }

        protected abstract void AnimationUpdate();

        protected virtual void Start()
        {
            foreach (var state in showAtStates)
            {
                CoreScript.Instance.FSM.SubscribeToEnter(state, Show);
                CoreScript.Instance.FSM.SubscribeToExit(state, Hide);
            }
            IsShown = isShownAtStart;
        }

        protected virtual void Update()
        {
            AnimationUpdate();
        }
    }
}