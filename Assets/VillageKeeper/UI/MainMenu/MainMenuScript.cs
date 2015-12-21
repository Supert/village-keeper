using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour {
	public Text monstersDefeatedText;
	public ScreenShadowScript shopShadow;
	OffScreenMenuScript _offScreenMenu;
	public Image roomFurniture;
	public Sprite freeFurniture;
	public Sprite premiumFurniture;	
	private OffScreenMenuScript _shop;
	public OffScreenMenuScript Shop {
		get {
			return null;
		}
	}
	void SetScores () {
		var monstersDefeated = CoreScript.Instance.Data.MonstersDefeated;
		if (monstersDefeated == 0)
			this.monstersDefeatedText.text = "No monsters defeated yet";
		else {
			if (monstersDefeated == 1)
				this.monstersDefeatedText.text = "First monster defeated!";
			else
				this.monstersDefeatedText.text = monstersDefeated + " monsters defeated";
		}
	}
	private void SetFurniture () {
		if (CoreScript.Instance.Data.HasPremium) {
			if (this.roomFurniture.sprite != premiumFurniture)
				this.roomFurniture.sprite = premiumFurniture;
		} else {
			if (this.roomFurniture.sprite != freeFurniture)
				this.roomFurniture.sprite = freeFurniture;
		}
	}
	// Use this for initialization
	void Start () {
		_offScreenMenu = GetComponent<OffScreenMenuScript>() as OffScreenMenuScript;
		SetScores ();
		SetFurniture ();
		shopShadow.ShadowButton.onClick.AddListener (() => {
			CoreScript.Instance.GameState = CoreScript.GameStates.InMenu;
		});
		CoreScript.Instance.GameStateChanged += (sender, e) => {
			switch (e.NewState) {
			case CoreScript.GameStates.InBuildMode:
			case CoreScript.GameStates.Paused:
			case CoreScript.GameStates.RoundFinished:
			case CoreScript.GameStates.InBattle: 
				_offScreenMenu.Hide ();
				shopShadow.Hide ();
				break;
			case CoreScript.GameStates.InMenu:
				SetFurniture ();
				SetScores ();
				_offScreenMenu.Show ();
				shopShadow.Hide ();
				break;
			case CoreScript.GameStates.InShop:
				_offScreenMenu.Show ();
				shopShadow.Show ();
				break;
			}
		};
		CoreScript.Instance.Data.DataFieldChanged += (sender, e) => {
			switch (e.FieldChanged) {
			case DataScript.DataFields.HasPremium:
				SetFurniture ();
				break;
			default:
				break;
			}
		};
	}
}
