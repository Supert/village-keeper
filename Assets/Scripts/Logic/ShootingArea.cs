using UnityEngine;
using UnityEngine.EventSystems;

namespace VillageKeeper
{
    public class ShootingArea : MonoBehaviour, IPointerDownHandler
    {
        private RectTransform rect;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Core.Data.Game.IsArrowForceOverThreshold.Get())
            {
                Vector3 target = new Vector3(float.NaN, float.NaN, float.NaN);
                RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, eventData.position, null, out target);
                Core.Instance.Archer.Shoot(target);
            }
        }
    }
}