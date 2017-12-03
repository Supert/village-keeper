using System;
using System.Collections;
using UnityEngine;
using System.Linq;

namespace VillageKeeper.UI
{
    public class OffScreenMenuScript : StateVisibleView
    {
        [SerializeField]
        protected RectTransform.Edge edge;

        private Vector2 targetPosition;
        
        private RectTransform rect;
        private Vector2 HiddenPositon
        {
            get
            {
                var rightTopPosition = new Vector2(rect.rect.width / 2 + Camera.main.pixelWidth / 2, rect.rect.height / 2 + Camera.main.pixelHeight / 2);
                var result = new Vector2();
                switch (edge)
                {
                    case RectTransform.Edge.Bottom:
                        result = new Vector2(targetPosition.x, -rightTopPosition.y);
                        break;
                    case RectTransform.Edge.Left:
                        result = new Vector2(-rightTopPosition.x, targetPosition.y);
                        break;
                    case RectTransform.Edge.Right:
                        result = new Vector2(rightTopPosition.x, targetPosition.y);
                        break;
                    case RectTransform.Edge.Top:
                        result = new Vector2(targetPosition.x, rightTopPosition.y);
                        break;
                }
                return result;
            }
        }

        protected virtual void Awake()
        {
            rect = GetComponent<RectTransform>() as RectTransform;
            targetPosition = rect.localPosition;
        }

        protected override void Start()
        {
            base.Start();
            if (isShownAtStart)
                rect.localPosition = targetPosition;
            else
                rect.localPosition = HiddenPositon;
        }

        protected override void AnimationUpdate()
        {
            float distance = Vector2.Distance(targetPosition, HiddenPositon);

            float speed = animationTime == 0 ? distance : distance * Time.deltaTime / animationTime;
            if (IsShown)
                rect.localPosition = Vector2.MoveTowards(rect.localPosition, targetPosition, speed);
            else
            {
                if ((Vector2)rect.localPosition == HiddenPositon)
                    gameObject.SetActive(false);
                else
                    rect.localPosition = Vector2.MoveTowards(rect.localPosition, HiddenPositon, speed);
            }
        }
    }
}