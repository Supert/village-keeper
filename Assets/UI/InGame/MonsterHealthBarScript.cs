using UnityEngine;
using System.Collections;

public class MonsterHealthBarScript : BarScript {
	// Use this for initialization
	private OffScreenMenuScript _offscreenMenu;
	void Awake () {
		_offscreenMenu = GetComponent<OffScreenMenuScript> () as OffScreenMenuScript;
	}
	void Start () {
		CoreScript.Instance.GameStateChanged += (sender, e) => OnGameStateChanged (e);
	}
	void OnGameStateChanged (CoreScript.GameStateChangedEventArgs e) {
		switch (e.NewState) {
		case CoreScript.GameStates.InBattle:
			this._offscreenMenu.Show ();
			this.MaxValue = CoreScript.Instance.Monster.maxHealth;
			this.minValue = 0;
			break;
		case CoreScript.GameStates.Paused:
		case CoreScript.GameStates.InHelp:
			break;
		default:
			this._offscreenMenu.Hide ();
			break;
		}
	}
	// Update is called once per frame
	void Update () {
		this.MaxValue = CoreScript.Instance.Monster.maxHealth;
		this.CurrentValue = CoreScript.Instance.Monster.Health;
	}
}
