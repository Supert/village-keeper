using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Canvas))]
public class ScalerScript : MonoBehaviour {
	
	public RectTransform cliffAreaRT;
	public RectTransform archerRT;
	// Use this for initialization
	void Start () {
		archerRT.anchoredPosition = new Vector2 (cliffAreaRT.rect.width * 0.7f, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
