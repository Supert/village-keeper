using System;
using System.Collections;
using UnityEngine;
using System.Linq;

namespace VillageKeeper.UI
{
    public class OffScreenMenu : StateVisibleView
    {
        [SerializeField]
        protected RectTransform.Edge edge;

        private Vector2 targetPosition;

        private RectTransform parent;
        private RectTransform rect;

        protected virtual void Awake()
        {
            rect = GetComponent<RectTransform>();
            parent = rect.parent as RectTransform;
            targetPosition = rect.anchoredPosition;
        }

        protected override void Start()
        {
            base.Start();
            animationStartTime = Time.time - animationDuration;
            if (isShownAtStart)
                ShowUpdate();
            else
                HideUpdate();
        }

        protected override void HideUpdate()
        {
            float progress = 0f;
            if (animationDuration == 0f)
                progress = 1f;
            else
                progress = (Time.time - animationStartTime) / animationDuration;

            float targetInset = 0f;
            float currentInset = GetCurrentInset(edge, rect);
            float size = 0f;
            switch (edge)
            {
                case RectTransform.Edge.Left:
                    targetInset = -rect.rect.width;
                    size = rect.rect.width;
                    break;
                case RectTransform.Edge.Right:
                    targetInset = -rect.rect.width;
                    size = rect.rect.width;
                    break;
                case RectTransform.Edge.Top:
                    targetInset = -rect.rect.height;
                    size = rect.rect.height;
                    break;
                case RectTransform.Edge.Bottom:
                    targetInset = -rect.rect.height;
                    size = rect.rect.height;
                    break;
            }

            Vector2 anchorMin = rect.anchorMin;
            Vector2 anchorMax = rect.anchorMax;
            rect.SetInsetAndSizeFromParentEdge(edge, progress >= 1f ? targetInset : Mathf.Lerp(currentInset, targetInset, progress), size);
            Vector3 position = rect.position;
            Vector2 rectSize = rect.rect.size;
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.position = position;
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectSize.x);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectSize.y);
        }

        protected override void ShowUpdate()
        {
            float progress = 0f;
            if (animationDuration == 0f)
                progress = 1f;
            else
                progress = (Time.time - animationStartTime) / animationDuration;
            if (progress >= 1f)
                rect.anchoredPosition = targetPosition;
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, targetPosition, progress);
        }
        
        private float GetCurrentInset(RectTransform.Edge edge, RectTransform rect)
        {
            int cornerIndex = (edge == RectTransform.Edge.Top || edge == RectTransform.Edge.Right) ? 2 : 0;

            Vector3[] corners = new Vector3[4];
            rect.GetWorldCorners(corners);

            Vector3[] parentCorners = new Vector3[4];
            parent.GetWorldCorners(parentCorners);

            Vector2 localChildCorner = parent.InverseTransformPoint(corners[cornerIndex]);
            Vector2 localParentCorner = parent.InverseTransformPoint(parentCorners[cornerIndex]);

            switch (edge)
            {
                case RectTransform.Edge.Left:
                    return localChildCorner.x - localParentCorner.x;
                case RectTransform.Edge.Right:
                    return localParentCorner.x - localChildCorner.x;
                case RectTransform.Edge.Top:
                    return localParentCorner.y - localChildCorner.y;
                case RectTransform.Edge.Bottom:
                    return localChildCorner.y - localParentCorner.y;
                default:
                    throw new Exception("Unity team added fifth edge. Why.");
            }
        }
    }
}