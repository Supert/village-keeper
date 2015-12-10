using UnityEngine;
using System.Collections;

public class ShopScript : MonoBehaviour {
	OffScreenMenuScript _offScreenMenu;
	// Use this for initialization
	void Start () {
		_offScreenMenu = GetComponent <OffScreenMenuScript> () as OffScreenMenuScript;
		StartCoroutine (InitCoroutine ());
	}
	IEnumerator InitCoroutine () {
		yield return null;
		CoreScript.Instance.GameStateChanged += (object sender, CoreScript.GameStateChangedEventArgs e) => {
			switch (e.NewState) {
			case CoreScript.GameStates.InBattle:
				_offScreenMenu.Hide ();
				break;
			case CoreScript.GameStates.InMenu:
				_offScreenMenu.Hide ();
				break;
			case CoreScript.GameStates.InShop:
				_offScreenMenu.Show ();
				break;
			case CoreScript.GameStates.Paused:
				_offScreenMenu.Hide ();
				break;
			case CoreScript.GameStates.RoundFinished:
				_offScreenMenu.Hide ();
				break;
			}
		};
	}
	// Update is called once per frame
	void Update () {
	
	}
}
