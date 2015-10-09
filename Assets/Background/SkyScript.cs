using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SkyScript : MonoBehaviour {
	public GameObject Clouds;
	private List<RectTransform> _cloudsList = new List<RectTransform> ();
	// Use this for initialization
	void Start () {
		_cloudsList.Add (Clouds.GetComponent<RectTransform> ());
		_cloudsList.Add (Instantiate<GameObject> (Clouds).GetComponent<RectTransform> ());
		for (int i = 0; i < _cloudsList.Count; i++) {
			_cloudsList[i].SetParent (this.transform.parent, false);
			var ap = _cloudsList[i].anchoredPosition;
			ap.x = (i - 1) * _cloudsList[i].rect.width;
			_cloudsList[i].anchoredPosition = ap;

		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach (var c in _cloudsList) {
			var ap = c.anchoredPosition;
			ap.x += CoreScript.Instance.Wind.Strength * Time.deltaTime * 10;
			if (ap.x < -c.rect.width)
				ap.x += c.rect.width * 2;
			if (ap.x > c.rect.width) {
				ap.x -= c.rect.width * 2;
			}
			c.anchoredPosition = ap;
		}
	}
}
