using UnityEngine;
using System.Collections;

public class WindmillSailsScript : MonoBehaviour {
	private RectTransform _rt;
	// Use this for initialization
	void Start () {
		_rt = this.GetComponent<RectTransform> ();
	}

	void Update () {
		_rt.Rotate (0, 0, 1f);
	}
}
