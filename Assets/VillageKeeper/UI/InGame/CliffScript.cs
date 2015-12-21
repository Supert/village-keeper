using UnityEngine;
using System.Collections;

public class CliffScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var offscreen = GetComponent<OffScreenMenuScript>() as OffScreenMenuScript;
		CoreScript.Instance.GameStateChanged += (sender, e) => {
			switch (e.NewState) {
			case CoreScript.GameStates.InBattle:
			case CoreScript.GameStates.Paused:
				offscreen.Show ();
				break;
			case CoreScript.GameStates.InHelp:
				break;
			default:
				offscreen.Hide ();
				break;
			}
		};
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
