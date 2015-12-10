using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiScript : MonoBehaviour {
	public PauseMenuScript PauseMenu;
	public ArrowLoadBarScript ArrowLoadBar;
	public ScreenShadowScript inGameScreenShadow;
	// Use this for initialization
	void Start () {
		StartCoroutine (InitCoroutine ());
	}
	IEnumerator InitCoroutine () {
		yield return null;
		CoreScript.Instance.GameStateChanged += (sender, e) => OnGameStateChanged (e);
	}
	void OnGameStateChanged (CoreScript.GameStateChangedEventArgs e) {
		switch (e.NewState) {
		case (CoreScript.GameStates.Paused):
			this.PauseMenu.Show ();
			this.inGameScreenShadow.Show ();
			break;
		case (CoreScript.GameStates.RoundFinished):
			this.PauseMenu.Show ();
			this.inGameScreenShadow.Show ();
			break;
		case (CoreScript.GameStates.InBattle):
			this.PauseMenu.Hide ();
			this.inGameScreenShadow.Hide ();
			break;
		case (CoreScript.GameStates.InMenu):
			this.PauseMenu.Hide ();
			this.inGameScreenShadow.Hide ();
			break;
		case (CoreScript.GameStates.InShop):
			this.PauseMenu.Hide ();
			this.inGameScreenShadow.Hide ();
			break;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
