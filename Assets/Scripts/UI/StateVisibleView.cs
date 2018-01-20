using UnityEngine;

namespace VillageKeeper.UI
{
    public abstract class StateVisibleView : MonoBehaviour
    {
        [SerializeField]
        protected FSM.States[] showAtStates;

        [SerializeField]
        protected float animationDuration;
        
        protected float animationStartTime;

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
                if (value != IsShown)
                    animationStartTime = Time.time;
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

        protected abstract void ShowUpdate();

        protected abstract void HideUpdate();

        protected void AnimationUpdate()
        {
            if (IsShown)
                ShowUpdate();
            else
                HideUpdate();
        }

        protected virtual void Start()
        {
            foreach (var state in showAtStates)
            {
                Core.Instance.FSM.SubscribeToEnter(state, Show);
                Core.Instance.FSM.SubscribeToExit(state, Hide);
            }
            IsShown = isShownAtStart;
        }

        protected virtual void Update()
        {
            AnimationUpdate();
        }
    }
}