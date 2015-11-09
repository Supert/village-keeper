using UnityEngine;
using System.Collections;

public class UiScript : MonoBehaviour {
	public PauseMenuScript PauseMenu;
	public ArrowLoadBarScript ArrowLoadBar;
	// Use this for initialization
	void Start () {
		CoreScript.Instance.GameStateChanged += (sender, e) => OnGameStateChanged (e);
	}

	void OnGameStateChanged (CoreScript.GameStateChangedEventArgs e) {
		switch (e.NewState) {
		case (CoreScript.GameStates.Paused):
			this.PauseMenu.Show ();
			break;
		case (CoreScript.GameStates.RoundFinished):
			this.PauseMenu.Show ();
			break;
		case (CoreScript.GameStates.InGame):
			this.PauseMenu.Hide ();
			break;
		case (CoreScript.GameStates.InMenu):
			this.PauseMenu.Hide ();
			break;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
