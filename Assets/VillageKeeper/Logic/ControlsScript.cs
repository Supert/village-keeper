using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ControlsScript : MonoBehaviour {
	public RectTransform MonsterArea;
	public RectTransform BowLoadingArea;
	private Vector2? _bowLoadingTouchStartingPosition;
	private Vector2? _bowLoadingTouchCurrentPosition;
	public Vector2 TouchDeltaPosition {
		get {
			if (_bowLoadingTouchStartingPosition != null && _bowLoadingTouchCurrentPosition != null)
				return (Vector2) (_bowLoadingTouchCurrentPosition - _bowLoadingTouchStartingPosition);
			else
				return new Vector2 (0, 0);
		}
	}

	void CheckForTouches () {
		foreach (var t in Input.touches) {
			if (RectTransformUtility.RectangleContainsScreenPoint (MonsterArea, t.position, Camera.current)) {
				if (t.phase == TouchPhase.Began) {
					if (CoreScript.Instance.Archer.IsLoaded) { 
						CoreScript.Instance.Archer.Shoot (Camera.main.ScreenToWorldPoint (t.position));
						_bowLoadingTouchCurrentPosition = _bowLoadingTouchStartingPosition = null;
					}
				}
			} else if (RectTransformUtility.RectangleContainsScreenPoint (BowLoadingArea, t.position, Camera.current)) {
				if (t.phase == TouchPhase.Began) {
					_bowLoadingTouchCurrentPosition = _bowLoadingTouchStartingPosition = t.position;
				}else if (t.phase == TouchPhase.Moved)
					_bowLoadingTouchCurrentPosition = t.position;
			}
		}
	}
	void SimulateTouchesInUnity () {
		if (RectTransformUtility.RectangleContainsScreenPoint (MonsterArea, Input.mousePosition, Camera.main)) {
			if (Input.GetMouseButtonDown (0)) {
				CoreScript.Instance.Archer.Shoot (Camera.main.ScreenToWorldPoint ( Input.mousePosition));
				_bowLoadingTouchCurrentPosition = _bowLoadingTouchStartingPosition = null;
			}
		}
		else if (RectTransformUtility.RectangleContainsScreenPoint (BowLoadingArea, Input.mousePosition, Camera.main)) {
			if (Input.GetMouseButtonDown (0))
				_bowLoadingTouchStartingPosition = Input.mousePosition;
			else if (Input.GetMouseButton (0))
				_bowLoadingTouchCurrentPosition = Input.mousePosition;
		}
	}
	// Update is called once per frame
	void Update () {
		switch (CoreScript.Instance.GameState) {
		case (CoreScript.GameStates.InBattle):
			#if UNITY_EDITOR
			SimulateTouchesInUnity ();
			#elif UNITY_ANDROID
			CheckForTouches ();
			#endif
			if (Input.GetKeyDown (KeyCode.Escape))
				CoreScript.Instance.GameState = CoreScript.GameStates.Paused;
			break;
		case (CoreScript.GameStates.Paused):
			if (Input.GetKeyDown (KeyCode.Escape))
				CoreScript.Instance.GameState = CoreScript.GameStates.InMenu;
			break;
		case (CoreScript.GameStates.RoundFinished):
			if (Input.GetKeyDown (KeyCode.Escape))
				CoreScript.Instance.GameState = CoreScript.GameStates.InMenu;
			break;
		case (CoreScript.GameStates.InShop):
			if (Input.GetKeyDown (KeyCode.Escape))
				CoreScript.Instance.GameState = CoreScript.GameStates.InMenu;
			break;
		case (CoreScript.GameStates.InMenu):
			if (Input.GetKeyDown (KeyCode.Escape))
				Application.Quit ();
			break;
		case (CoreScript.GameStates.InBuildMode):
			if (Input.GetKeyDown (KeyCode.Escape))
				CoreScript.Instance.GameState = CoreScript.GameStates.InMenu;
			break;
		default:
			break;
		}
	}

}
