using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Soomla.Store;
using AssemblyCSharp;
public class ShopScript : MonoBehaviour {
	OffScreenMenuScript _offScreenMenu;
	public Button thounsandButton;
	public Button tenThousandButton;
	// Use this for initialization
	void Start () {
		_offScreenMenu = GetComponent <OffScreenMenuScript> () as OffScreenMenuScript;
		StartCoroutine (InitCoroutine ());
	}
	IEnumerator InitCoroutine () {
		yield return null;
		thounsandButton.onClick.AddListener (() => {
			SoomlaStore.BuyMarketItem (EconomyAssets.THOUSAND_COINS.ItemId, "thousand");
			CoreScript.Instance.Audio.PlayClick ();
		});
		tenThousandButton.onClick.AddListener (() => {
			SoomlaStore.BuyMarketItem (EconomyAssets.TEN_THOUSAND_COINS.ItemId, "ten thousand");
			CoreScript.Instance.Audio.PlayClick ();
		});
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
