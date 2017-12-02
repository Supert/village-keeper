using UnityEngine;
using System.Collections;

namespace VillageKeeper.UI
{
    public class OffScreenMenuScript : MonoBehaviour
    {
        public float animationTime;
        public RectTransform.Edge Edge;
        private Vector2 targetPosition;
        public bool isShownAtStart;

        private bool isShown;
        public bool IsShown
        {
            get
            {
                return isShown;
            }
            private set
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
        private RectTransform rect;
        private Vector2 HiddenPositon
        {
            get
            {
                var rightTopPosition = new Vector2(rect.rect.width / 2 + Camera.main.pixelWidth / 2, rect.rect.height / 2 + Camera.main.pixelHeight / 2);
                var result = new Vector2();
                switch (Edge)
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
        // Use this for initialization
        protected virtual void Start()
        {
            rect = GetComponent<RectTransform>() as RectTransform;
            targetPosition = rect.localPosition;
            StartCoroutine(InitCoroutine());
        }
        IEnumerator InitCoroutine()
        {
            yield return null;
            IsShown = isShownAtStart;
            if (isShownAtStart)
                rect.localPosition = targetPosition;
            else
                rect.localPosition = HiddenPositon;

        }
        // Update is called once per frame
        protected virtual void Update()
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