using UnityEngine;
using System.Collections;

public class OffScreenMenuScript : MonoBehaviour
{
    public float animationTime;
    public RectTransform.Edge Edge;
    private Vector2 _targetPosition;
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
    private RectTransform _rect;
    private Vector2 HiddenPositon
    {
        get
        {
            var rightTopPosition = new Vector2(_rect.rect.width / 2 + Camera.main.pixelWidth / 2, _rect.rect.height / 2 + Camera.main.pixelHeight / 2);
            var result = new Vector2();
            switch (Edge)
            {
                case RectTransform.Edge.Bottom:
                    result = new Vector2(_targetPosition.x, -rightTopPosition.y);
                    break;
                case RectTransform.Edge.Left:
                    result = new Vector2(-rightTopPosition.x, _targetPosition.y);
                    break;
                case RectTransform.Edge.Right:
                    result = new Vector2(rightTopPosition.x, _targetPosition.y);
                    break;
                case RectTransform.Edge.Top:
                    result = new Vector2(_targetPosition.x, rightTopPosition.y);
                    break;
            }
            return result;
        }
    }
    // Use this for initialization
    protected virtual void Start()
    {
        _rect = GetComponent<RectTransform>() as RectTransform;
        _targetPosition = _rect.localPosition;
        StartCoroutine(InitCoroutine());
    }
    IEnumerator InitCoroutine()
    {
        yield return null;
        IsShown = isShownAtStart;
        if (isShownAtStart)
            _rect.localPosition = _targetPosition;
        else
            _rect.localPosition = HiddenPositon;

    }
    // Update is called once per frame
    protected virtual void Update()
    {
        float distance = Vector2.Distance(_targetPosition, HiddenPositon);
        float speed = animationTime == 0 ? distance : distance * Time.deltaTime / animationTime;
        if (IsShown)
            _rect.localPosition = Vector2.MoveTowards(_rect.localPosition, _targetPosition, speed);
        else
        {
            if ((Vector2)_rect.localPosition == HiddenPositon)
                gameObject.SetActive(false);
            else
                _rect.localPosition = Vector2.MoveTowards(_rect.localPosition, HiddenPositon, speed);
        }
    }
}
