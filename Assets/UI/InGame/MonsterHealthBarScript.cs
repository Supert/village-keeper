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
		if (e.NewState == CoreScript.GameStates.InBattle) {
			this._offscreenMenu.Show ();
			this.MaxValue = CoreScript.Instance.Monster.maxHealth;
			this.minValue = 0;
		} else {
			this._offscreenMenu.Hide ();
		}
	}
	// Update is called once per frame
	void Update () {
		this.MaxValue = CoreScript.Instance.Monster.maxHealth;
		this.CurrentValue = CoreScript.Instance.Monster.Health;
	}
}
