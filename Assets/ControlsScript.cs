using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlsScript : MonoBehaviour {
	public RectTransform MonsterArea;
	public RectTransform BowLoadingArea;
	private Vector2? _touchStartingPosition;
	private Vector2? _touchCurrentPosition;
	public Vector2 TouchDeltaPosition {
		get {
			if (_touchStartingPosition != null && _touchCurrentPosition != null)
				return (Vector2) (_touchCurrentPosition - _touchStartingPosition);
			else
				return new Vector2 (0, 0);
		}
	}
	// Use this for initialization
	void Start () {
		//Input.simulateMouseWithTouches = true;

	}

	// Update is called once per frame
	void Update () {
		switch (CoreScript.Instance.GameState) {
		case (CoreScript.GameStates.InGame):
			if (RectTransformUtility.RectangleContainsScreenPoint (MonsterArea, Input.mousePosition, Camera.main)) {
				if (Input.GetMouseButtonDown (0)) {
					CoreScript.Instance.Archer.Shoot (Camera.main.ScreenToWorldPoint ( Input.mousePosition), CoreScript.Instance.UI.ArrowLoadBar.RelativeCurrentValue == 1f);
					_touchCurrentPosition = _touchStartingPosition = null;
				}
			}
			else if (RectTransformUtility.RectangleContainsScreenPoint (BowLoadingArea, Input.mousePosition, Camera.main)) {
				if (Input.GetMouseButtonDown (0))
					_touchStartingPosition = Input.mousePosition;
				else if (Input.GetMouseButton (0))
					_touchCurrentPosition = Input.mousePosition;
			}
			break;
		default:
			break;
		}
	}

}
