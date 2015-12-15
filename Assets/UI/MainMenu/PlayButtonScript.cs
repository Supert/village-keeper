using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var button = GetComponent<Button> () as Button;
		button.onClick.AddListener (() => {
			CoreScript.Instance.GameState = CoreScript.GameStates.InBuildMode;
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
