using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Camera mainCamera;
	// Use this for initialization
	void Start () {
		mainCamera.aspect = Screen.width / Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
