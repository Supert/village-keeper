using UnityEngine;

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
                    if (t.phase == TouchPhase.Began && Core.Instance.GameData.IsArrowForceOverThreshold.Get())
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

        void CheckArcherControls()
        {
            if (Application.isMobilePlatform)
                CheckForTouches();
            else
                CheckForMouse();

            if (Core.Instance.FSM.Current != FSM.States.Battle)
                return;

            if (isLoading)
                Core.Instance.GameData.ClampedArrowForce.Set(Mathf.Clamp01(-(bowLoadingTouchCurrentPosition - bowLoadingTouchStartingPosition).x / 150f));

            if (!isLoading && !Core.Instance.GameData.IsArrowForceOverThreshold.Get())
                Core.Instance.GameData.ClampedArrowForce.Set(Mathf.Clamp01(Core.Instance.GameData.ClampedArrowForce.Get() - Time.deltaTime * 3f));
        }

        void Update()
        {
            if (Core.Instance.FSM.Current == FSM.States.Battle)
                CheckArcherControls();
        }
    }
}