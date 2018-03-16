using UnityEngine;
using UnityEngine.EventSystems;

namespace VillageKeeper
{
    public class BowLoadingArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        private RectTransform rect;
        private Vector2 bowLoadingTouchStartingPosition;
        private Vector2 bowLoadingTouchCurrentPosition;
        private bool isLoading;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isLoading = true;
            bowLoadingTouchStartingPosition = Input.mousePosition;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isLoading = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isLoading = false;
        }

        private void Update()
        {
            if (isLoading)
                bowLoadingTouchCurrentPosition = Input.mousePosition;

            if (Core.Instance.FSM.Current != FSM.States.Battle)
                return;

            if (isLoading)
                Core.Data.Game.ClampedArrowForce.Set(Mathf.Clamp01(-(bowLoadingTouchCurrentPosition - bowLoadingTouchStartingPosition).x / (rect.rect.size.x * 0.25f)));

            if (!isLoading && !Core.Data.Game.IsArrowForceOverThreshold.Get())
                Core.Data.Game.ClampedArrowForce.Set(Mathf.Clamp01(Core.Data.Game.ClampedArrowForce.Get() - Time.deltaTime * 3f));
        }
    }
}