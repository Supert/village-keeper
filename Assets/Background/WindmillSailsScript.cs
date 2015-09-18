using UnityEngine;
using System.Collections;

public class WindmillSailsScript : MonoBehaviour {
	public WindScript wind;
	private RectTransform _rt;
	// Use this for initialization
	void Start () {
		_rt = this.GetComponent<RectTransform> ();
	}

	void Update () {
		_rt.Rotate (0, 0, -wind.Strength * Time.deltaTime * 10);
	}
}
