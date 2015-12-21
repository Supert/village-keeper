using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PauseMenuScript : OffScreenMenuScript {
	public Text title;
	public Button homeButton;
	public Button continueButton;
	public GameObject roundFinishedText;
	public Text WeCollectedFoodText;
	public Text WithMonsterBonusText;
	public Text YouGainGoldText;
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		StartCoroutine (InitCoroutine ());
	}
	private IEnumerator InitCoroutine () {
		yield return null;
		CoreScript.Instance.GameStateChanged += (sender, e) => OnGameStateChanged (e);
		continueButton.onClick.AddListener (() =>  {
			if (CoreScript.Instance.GameState == CoreScript.GameStates.Paused)
				CoreScript.Instance.GameState = CoreScript.GameStates.InBattle;
			else
				CoreScript.Instance.GameState = CoreScript.GameStates.InBuildMode;
			CoreScript.Instance.Audio.PlayClick ();
		});
		homeButton.onClick.AddListener (() => CoreScript.Instance.GameState = CoreScript.GameStates.InMenu);
		CoreScript.Instance.Audio.PlayClick ();
	}
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
	void OnGameStateChanged (CoreScript.GameStateChangedEventArgs e) {
		/*switch (e.NewState) {
		case (CoreScript.GameStates.Paused):
			break;
		case (CoreScript.GameStates.RoundFinished):
			this.PauseMenu.Show ();
			break;
		case (CoreScript.GameStates.InBattle):
			this.PauseMenu.Hide ();
			break;
		case (CoreScript.GameStates.InMenu):
			this.PauseMenu.Hide ();
			break;
		case (CoreScript.GameStates.InShop):
			this.PauseMenu.Hide ();
			break;
		}
	}*/
		switch (e.NewState) {
		case (CoreScript.GameStates.Paused):
			this.Show ();
			this.title.text = "Pause";
			roundFinishedText.SetActive (false);
			break;
		case (CoreScript.GameStates.RoundFinished):
			this.Show ();
			this.title.text = "Victory!";
			roundFinishedText.SetActive (true);
			WeCollectedFoodText.text = "We collected " + (CoreScript.Instance.Data.GetFarmsFood () + CoreScript.Instance.Data.GetWindmillBonusFood ()).ToString ();
			WithMonsterBonusText.text = "With " + CoreScript.Instance.Data.GetMonsterBonusGold ().ToString ();
			YouGainGoldText.text = "you gain " + CoreScript.Instance.Data.GetRoundFinishedBonusGold ().ToString ();
			break;
		default:
			this.Hide ();
			break;
		}
	}
}
