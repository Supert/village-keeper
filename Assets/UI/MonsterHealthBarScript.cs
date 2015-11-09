using UnityEngine;
using System.Collections;

public class MonsterHealthBarScript : BarScript {
	// Use this for initialization
	void Start () {
		StartCoroutine (InitCoroutine ());
	}
	IEnumerator InitCoroutine () {
		yield return null;
		this.maxValue = CoreScript.Instance.Monster.maxHealth;
		CoreScript.Instance.GameStateChanged += (sender, e) => OnGameStateChanged (e);
	}
	void OnGameStateChanged (CoreScript.GameStateChangedEventArgs e) {
		if (e.NewState == CoreScript.GameStates.InGame) {
			this.maxValue = CoreScript.Instance.Monster.maxHealth;
			this.minValue = 0;
		}
	}
	// Update is called once per frame
	void Update () {
		this.CurrentValue = CoreScript.Instance.Monster.Health;
	}
}
