using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlsScript : MonoBehaviour {
	public RectTransform MonsterArea;
	public RectTransform BowLoadingArea;
	// Use this for initialization
	void Start () {
		//Input.simulateMouseWithTouches = true;

	}

	// Update is called once per frame
	void OnGUI () {
		var correctMousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		if (RectTransformUtility.RectangleContainsScreenPoint (MonsterArea, correctMousePosition, Camera.main)) {
			if (Input.GetMouseButtonDown (0))
				CoreScript.Instance.Archer.Shoot (Camera.main.ScreenToWorldPoint ((Vector3) correctMousePosition));
		}
		else if (RectTransformUtility.RectangleContainsScreenPoint (BowLoadingArea, Input.mousePosition)) {
		}

	}

}
