using UnityEngine;
using VillageKeeper.Model;

namespace VillageKeeper
{
    public class ControlsScript : MonoBehaviour
    {
        public RectTransform MonsterArea;
        public RectTransform BowLoadingArea;

        private Vector2 bowLoadingTouchStartingPosition;
        private Vector2 bowLoadingTouchCurrentPosition;
        private bool isLoading;

        void CheckForTouches()
        {
            bowLoadingTouchStartingPosition = Vector2.zero;
            bowLoadingTouchCurrentPosition = Vector2.zero;
            isLoading = false;
            foreach (var t in Input.touches)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(MonsterArea, t.position, Camera.current))
                {
                    if (t.phase == TouchPhase.Began && Core.Data.Game.IsArrowForceOverThreshold.Get())
                    {
                        Core.Instance.Archer.Shoot(Camera.main.ScreenToWorldPoint(t.position));
                    }
                }
                else if (RectTransformUtility.RectangleContainsScreenPoint(BowLoadingArea, t.position, Camera.current))
                {
                    if (t.phase == TouchPhase.Began)
                    {
                        isLoading = true;
                        bowLoadingTouchCurrentPosition = bowLoadingTouchStartingPosition = t.position;
                    }
                    else if (t.phase == TouchPhase.Moved)
                    {
                        isLoading = true;
                        bowLoadingTouchCurrentPosition = t.position;
                    }
                }
            }
        }

        void CheckForMouse()
        {
            isLoading = false;
            if (RectTransformUtility.RectangleContainsScreenPoint(MonsterArea, Input.mousePosition, Camera.main))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Core.Instance.Archer.Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(BowLoadingArea, Input.mousePosition, Camera.main))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isLoading = true;
                    bowLoadingTouchStartingPosition = Input.mousePosition;
                }
                else if (Input.GetMouseButton(0))
                {
                    isLoading = true;
                    bowLoadingTouchCurrentPosition = Input.mousePosition;
                }
            }
        }

        private void CheckArcherControls()
        {
            if (Application.isMobilePlatform)
                CheckForTouches();
            else
                CheckForMouse();

            if (Core.Instance.FSM.Current != FSM.States.Battle)
                return;

            if (isLoading)
                Core.Data.Game.ClampedArrowForce.Set(Mathf.Clamp01(-(bowLoadingTouchCurrentPosition - bowLoadingTouchStartingPosition).x / 150f));

            if (!isLoading && !Core.Data.Game.IsArrowForceOverThreshold.Get())
                Core.Data.Game.ClampedArrowForce.Set(Mathf.Clamp01(Core.Data.Game.ClampedArrowForce.Get() - Time.deltaTime * 3f));
        }

        private void Update()
        {
            CheckArcherControls();
        }
    }
}