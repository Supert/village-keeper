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

        private RectTransform rect;

        protected virtual void Awake()
        {
            rect = GetComponent<RectTransform>() as RectTransform;
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
            int index = (edge != RectTransform.Edge.Top && edge != RectTransform.Edge.Bottom) ? 0 : 1;
            bool flag = false;
            float size = 0f;

            switch (edge)
            {
                case RectTransform.Edge.Left:
                    size = rect.rect.width;
                    flag = false;
                    break;
                case RectTransform.Edge.Right:
                    size = rect.rect.width;
                    flag = true;
                    break;
                case RectTransform.Edge.Top:
                    size = rect.rect.height;
                    flag = true;
                    break;
                case RectTransform.Edge.Bottom:
                    size = rect.rect.height;
                    flag = false;
                    break;
            }
            
            if (flag)
                return -rect.anchoredPosition[index] - size * (1f - rect.pivot[index]);
            else
                return rect.anchoredPosition[index] - size * rect.pivot[index];
        }
    }
}