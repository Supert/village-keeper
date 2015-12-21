using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BuildingPickerScript : MonoBehaviour {
	public Text nameText;
	public Text priceText;
	public Text descriptionText;
	public Image iconImage;
	private Dictionary<BuildingScript.BuildingTypes, Sprite> _iconSprites = null;
	private Sprite GetSpriteForIconByType (BuildingScript.BuildingTypes type) {
		if (_iconSprites == null || _iconSprites.Count == 0) {
			_iconSprites = new Dictionary<BuildingScript.BuildingTypes, Sprite> ();
			foreach (BuildingScript.BuildingTypes v in Enum.GetValues (typeof (BuildingScript.BuildingTypes))) {
				_iconSprites.Add (v, Resources.Load<Sprite> ("Buildings/Icons/" + Enum.GetName (typeof(BuildingScript.BuildingTypes), v)) as Sprite);
			}
		}
		return _iconSprites [type];
	}
	public Button previousButton;
	public Button nextButton;
	private Dictionary<BuildingScript.BuildingTypes, BuildingScript> _buildingsPrepared = new Dictionary<BuildingScript.BuildingTypes, BuildingScript> ();
	public BuildingScript CurrentPreparedBuilding {
		get {
			if (!_buildingsPrepared.ContainsKey(CurrentBuildingType) || _buildingsPrepared[CurrentBuildingType] == null || _buildingsPrepared[CurrentBuildingType] != null) {
				PrepareBuildingOfType (CurrentBuildingType);
			}
			return _buildingsPrepared[CurrentBuildingType];
		}
	}
	private BuildingScript.BuildingTypes _currentBuildingType;
	public BuildingScript.BuildingTypes CurrentBuildingType {
		get {
			return _currentBuildingType;
		}
		set {
			_currentBuildingType = value;
			this.nameText.text = CurrentPreparedBuilding.HumanFriendlyName;
			this.priceText.text = CurrentPreparedBuilding.GoldCost.ToString ();
			this.descriptionText.text = CurrentPreparedBuilding.Description;
			this.iconImage.sprite = GetSpriteForIconByType (value);
		}
	}
	// Use this for initialization
	void PrepareBuildingOfType (BuildingScript.BuildingTypes type) {
		if (!_buildingsPrepared.ContainsKey (type) || _buildingsPrepared [type].Tile != null) {
			var bs = BuildingScript.GetNewBuildingOfType (type);
			bs.gameObject.SetActive (false);
			if (!_buildingsPrepared.ContainsKey (type))
				_buildingsPrepared.Add (type, bs);
			else 
				_buildingsPrepared [type] = bs;
		}
	}
	void Awake () {
		foreach (BuildingScript.BuildingTypes v in Enum.GetValues (typeof (BuildingScript.BuildingTypes))) {
			PrepareBuildingOfType (v);
		}
		previousButton.onClick.AddListener (() => {
			var n = Enum.GetNames(typeof(BuildingScript.BuildingTypes)).Length;
			CurrentBuildingType = (BuildingScript.BuildingTypes) (((byte) CurrentBuildingType - 1 + n) % n);
			CoreScript.Instance.Audio.PlayClick ();
		});
		nextButton.onClick.AddListener (() => {
			var n = Enum.GetNames(typeof(BuildingScript.BuildingTypes)).Length;
			CurrentBuildingType = (BuildingScript.BuildingTypes) (((byte) CurrentBuildingType + 1 + n) % n);
			CoreScript.Instance.Audio.PlayClick ();
		});
		this.CurrentBuildingType = BuildingScript.BuildingTypes.Farm;
	}
	void Start () {
		var offscreen = GetComponent<OffScreenMenuScript> () as OffScreenMenuScript;
		CoreScript.Instance.GameStateChanged += (sender, e) => {
			switch (e.NewState) {
			case CoreScript.GameStates.InBuildMode:
				offscreen.Show ();
				break;
			case CoreScript.GameStates.InHelp:
				break;
			default:
				offscreen.Hide ();
				break;
			}
		};
	}
	// Update is called once per frame
	void Update () {
		if (CoreScript.Instance.GameState == CoreScript.GameStates.InBuildMode) {
			if (Input.GetMouseButtonDown (0)) {
				if (RectTransformUtility.RectangleContainsScreenPoint (this.iconImage.rectTransform, Input.mousePosition, Camera.main) && CoreScript.Instance.Data.Gold >= CurrentPreparedBuilding.GoldCost) {
					CurrentPreparedBuilding.transform.localPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					CurrentPreparedBuilding.gameObject.SetActive (true);
				}
			}
		}
	}
}