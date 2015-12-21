using UnityEngine;
using System.Collections;

public class OffScreenMenuScript : MonoBehaviour {
	public float animationTime;
	public RectTransform.Edge Edge;
	private Vector2 _targetPosition;
	public bool isShownAtStart;
	private bool _isShown;
	public bool IsShown {
		get {
			return _isShown;
		}
		private set {
			if (value)
				this.gameObject.SetActive (true);
			_isShown = value;
		}
	}
	public void Show () {
		this.IsShown = true;
	}
	public void Hide () {
		this.IsShown = false;
	}
	private RectTransform _rect;
	private Vector2 HiddenPositon {
		get {
			var rightTopPosition = new Vector2 (this._rect.rect.width / 2 + Camera.main.pixelWidth / 2, this._rect.rect.height / 2 + Camera.main.pixelHeight / 2);
			var result = new Vector2 ();
			switch (Edge) {
			case RectTransform.Edge.Bottom:
				result = new Vector2 (_targetPosition.x, -rightTopPosition.y);
				break;
			case RectTransform.Edge.Left:
				result = new Vector2 (-rightTopPosition.x, _targetPosition.y);
				break;
			case RectTransform.Edge.Right:
				result = new Vector2 (rightTopPosition.x, _targetPosition.y);
				break;
			case RectTransform.Edge.Top:
				result = new Vector2 (_targetPosition.x, rightTopPosition.y);
				break;
			}
			return result;
		}
	}
	// Use this for initialization
	protected virtual void Start () {
		this._rect = GetComponent<RectTransform> () as RectTransform;
		this._targetPosition = _rect.localPosition;
		StartCoroutine (InitCoroutine ());
	}
	IEnumerator InitCoroutine () {
		yield return null;
		IsShown = isShownAtStart;
		if (isShownAtStart)
			this._rect.localPosition = _targetPosition;
		else
			this._rect.localPosition = HiddenPositon;
		
	}
	// Update is called once per frame
	protected virtual void Update () {
		float distance = Vector2.Distance (_targetPosition, HiddenPositon);
		float speed = animationTime == 0 ? distance : distance * Time.deltaTime / animationTime; 
		if (IsShown)
			this._rect.localPosition = Vector2.MoveTowards (this._rect.localPosition, this._targetPosition, speed);
		else {
			if ((Vector2) this._rect.localPosition == HiddenPositon)
				this.gameObject.SetActive (false);
			else
				this._rect.localPosition = Vector2.MoveTowards (this._rect.localPosition, this.HiddenPositon, speed);
		}
	}
}
