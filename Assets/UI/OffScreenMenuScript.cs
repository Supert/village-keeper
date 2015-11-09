using UnityEngine;
using System.Collections;

public class OffScreenMenuScript : MonoBehaviour {
	public RectTransform.Edge Edge;
	public Vector2 TargetPosition;
	public bool IsShown {
		get;
		private set;
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
				result = new Vector2 (TargetPosition.x, -rightTopPosition.y);
				break;
			case RectTransform.Edge.Left:
				result = new Vector2 (-rightTopPosition.x, TargetPosition.y);
				break;
			case RectTransform.Edge.Right:
				result = new Vector2 (rightTopPosition.x, TargetPosition.y);
				break;
			case RectTransform.Edge.Top:
				result = new Vector2 (TargetPosition.x, rightTopPosition.y);
				break;
			}
			return result;
		}
	}
	// Use this for initialization
	protected virtual void Start () {
		this._rect = GetComponent<RectTransform> () as RectTransform;
		StartCoroutine (InitCoroutine ());
	}
	IEnumerator InitCoroutine () {
		yield return null;
		this._rect.localPosition = HiddenPositon;
	}
	// Update is called once per frame
	protected virtual void Update () {
		if (IsShown)
			this._rect.localPosition = Vector2.MoveTowards (this._rect.localPosition, this.TargetPosition, Screen.width * Time.deltaTime / 2);
		else 
			this._rect.localPosition = Vector2.MoveTowards (this._rect.localPosition, this.HiddenPositon, Screen.width  * Time.deltaTime / 2);
	}
}
