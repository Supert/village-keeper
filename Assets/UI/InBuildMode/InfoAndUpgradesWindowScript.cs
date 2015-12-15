using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoAndUpgradesWindowScript : MonoBehaviour {
	public BreadToGoldLabelScript currentBreadToGold;
	public BreadToGoldLabelScript upgradeBreadToGold;
	public GameObject castleUpgradeWindow;
	public Sprite firstCastleUpgradeSprite;
	public Sprite secondCastleUpgradeSprite;
	public Image castleUpgradeIcon;
	public Text upgradePriceText;
	public Button upgradeButton;
	private OffScreenMenuScript _offscreen;
	void SetUpgradeButton () {
		var data = CoreScript.Instance.Data;
		if (data.Gold >= data.GetCastleUpgradeCost ())
			upgradeButton.interactable = true;
		else
			upgradeButton.interactable = false;
	}
	void SetValues () {
		var data = CoreScript.Instance.Data;
		currentBreadToGold.goldText.text = data.GetBreadToGoldMultiplier ().ToString ();
		upgradeBreadToGold.goldText.text = data.GetBreadToGoldMultiplier (data.VillageLevel + 1).ToString();
		upgradePriceText.text = data.GetCastleUpgradeCost ().ToString ();
		switch (data.VillageLevel) {
		case 0:
			castleUpgradeWindow.SetActive (true);
			castleUpgradeIcon.sprite = firstCastleUpgradeSprite;
			upgradePriceText.text = data.GetCastleUpgradeCost ().ToString ();
			break;
		case 1:
			castleUpgradeWindow.SetActive (true);
			castleUpgradeIcon.sprite = secondCastleUpgradeSprite;
			break;
		case 2:
			castleUpgradeWindow.SetActive (false);
			break;
		default:
			break;
		}
		SetUpgradeButton ();
	}
	void OnGameStateChanged (CoreScript.GameStateChangedEventArgs e) {
		switch (e.NewState) {
		case CoreScript.GameStates.InBuildMode:
			SetValues ();
			_offscreen.Show ();
			break;
		default:
			_offscreen.Hide ();
			break;
		}
	}
	void OnDataFieldChanged (DataScript.DataFieldChangedEventArgs e) {
		switch (e.FieldChanged) {
		case DataScript.DataFields.Gold:
			SetUpgradeButton ();
			break;
		case DataScript.DataFields.VillageLevel:
			SetValues ();
			break;
		}
	}

	void Start () {
		_offscreen = GetComponent<OffScreenMenuScript> () as OffScreenMenuScript;
		CoreScript.Instance.GameStateChanged += (sender, e) => OnGameStateChanged (e);
		CoreScript.Instance.Data.DataFieldChanged += (sender, e) => OnDataFieldChanged (e);
		CoreScript.Instance.Data.DataFieldChanged += (sender, e) => OnDataFieldChanged (e);
		upgradeButton.onClick.AddListener (() => {
			if (CoreScript.Instance.Data.Gold >= CoreScript.Instance.Data.GetCastleUpgradeCost ()) {
				CoreScript.Instance.Data.Gold -= CoreScript.Instance.Data.GetCastleUpgradeCost ();
				CoreScript.Instance.Data.VillageLevel++;
			}

		});
	}
}
