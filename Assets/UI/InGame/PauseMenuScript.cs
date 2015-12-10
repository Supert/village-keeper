using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PauseMenuScript : OffScreenMenuScript {
	public Text title;
	public Button homeButton;
	public Button continueButton;
	public Image autoContinueIndicator;
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		StartCoroutine (InitCoroutine ());
	}
	private IEnumerator InitCoroutine () {
		yield return null;
		CoreScript.Instance.GameStateChanged += (sender, e) => OnGameStateChanged (e);
		continueButton.onClick.AddListener (() => CoreScript.Instance.GameState = CoreScript.GameStates.InBattle);
		homeButton.onClick.AddListener (() => CoreScript.Instance.GameState = CoreScript.GameStates.InMenu);
	}
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		switch (CoreScript.Instance.GameState) {
		case (CoreScript.GameStates.RoundFinished):
			if (this.autoContinueIndicator.fillAmount < 1f)
				this.autoContinueIndicator.fillAmount += Time.deltaTime / 5f; 
			else
				CoreScript.Instance.GameState = CoreScript.GameStates.InBattle;
			break;
		default:
			break;
		}
	}
	void OnGameStateChanged (CoreScript.GameStateChangedEventArgs e) {
		switch (e.NewState) {
		case (CoreScript.GameStates.Paused):
			this.title.text = "Pause";
			this.autoContinueIndicator.gameObject.SetActive (false);
			break;
		case (CoreScript.GameStates.RoundFinished):
			this.title.text = "Victory!";
			this.autoContinueIndicator.gameObject.SetActive (true);
			this.autoContinueIndicator.fillAmount = 0;
			break;
		default:
			break;
		}
	}
}
