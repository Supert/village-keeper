using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartBattleButtonScript : MonoBehaviour {
	private OffScreenMenuScript _offScreenMenu;
	void Awake () {
		_offScreenMenu = GetComponent<OffScreenMenuScript> () as OffScreenMenuScript;
	}
	void Start () {
		var button = GetComponent<Button> () as Button;
		button.onClick.AddListener (() => {
			CoreScript.Instance.GameState = CoreScript.GameStates.InBattle;
			CoreScript.Instance.Audio.PlayClick ();
		});
		CoreScript.Instance.GameStateChanged += (sender, e) => {
			switch (e.NewState) {
			case CoreScript.GameStates.Paused:
			case CoreScript.GameStates.RoundFinished:
			case CoreScript.GameStates.InBattle:
			case CoreScript.GameStates.InMenu:
			case CoreScript.GameStates.InShop:
				_offScreenMenu.Hide ();
				break;
			case CoreScript.GameStates.InBuildMode:
				_offScreenMenu.Show ();
				break;
			}
		};
	}
}
