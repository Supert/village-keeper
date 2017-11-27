using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class BuildingPickerScript : MonoBehaviour
{
    public Text nameText;
    public Text priceText;
    public Text descriptionText;
    public Image iconImage;

    public Button previousButton;
    public Button nextButton;

    private Dictionary<BuildingScript.BuildingTypes, Sprite> iconSprites = null;

    private Dictionary<BuildingScript.BuildingTypes, BuildingScript> buildingsPrepared = new Dictionary<BuildingScript.BuildingTypes, BuildingScript>();

    private BuildingScript.BuildingTypes currentBuildingType;

    private Sprite GetSpriteForIconByType(BuildingScript.BuildingTypes type)
    {
        if (iconSprites == null || iconSprites.Count == 0)
        {
            iconSprites = new Dictionary<BuildingScript.BuildingTypes, Sprite>();
            foreach (BuildingScript.BuildingTypes v in Enum.GetValues(typeof(BuildingScript.BuildingTypes)))
            {
                iconSprites.Add(v, Resources.Load<Sprite>("Buildings/Icons/" + Enum.GetName(typeof(BuildingScript.BuildingTypes), v)) as Sprite);
            }
        }
        return iconSprites[type];
    }

    public BuildingScript CurrentPreparedBuilding
    {
        get
        {
            if (!buildingsPrepared.ContainsKey(CurrentBuildingType)
                || buildingsPrepared[CurrentBuildingType] == null
                || buildingsPrepared[CurrentBuildingType].Tile != null
                || buildingsPrepared[CurrentBuildingType].gameObject == null)
            {
                PrepareBuildingOfType(CurrentBuildingType);
            }
            return buildingsPrepared[CurrentBuildingType];
        }
    }

    public BuildingScript.BuildingTypes CurrentBuildingType
    {
        get
        {
            return currentBuildingType;
        }
        set
        {
            currentBuildingType = value;
            nameText.text = CurrentPreparedBuilding.HumanFriendlyName;
            priceText.text = CurrentPreparedBuilding.GoldCost.ToString();
            descriptionText.text = CurrentPreparedBuilding.Description;
            iconImage.sprite = GetSpriteForIconByType(value);
        }
    }

    void PrepareBuildingOfType(BuildingScript.BuildingTypes type)
    {
        var bs = BuildingScript.GetNewBuildingOfType(type);
        bs.gameObject.SetActive(false);
        if (!buildingsPrepared.ContainsKey(type))
            buildingsPrepared.Add(type, bs);
        else
            buildingsPrepared[type] = bs;
    }

    void Awake()
    {
        foreach (BuildingScript.BuildingTypes v in Enum.GetValues(typeof(BuildingScript.BuildingTypes)))
        {
            PrepareBuildingOfType(v);
        }
        previousButton.onClick.AddListener(() =>
        {
            var n = Enum.GetNames(typeof(BuildingScript.BuildingTypes)).Length;
            CurrentBuildingType = (BuildingScript.BuildingTypes)(((byte)CurrentBuildingType - 1 + n) % n);
            CoreScript.Instance.Audio.PlayClick();
        });
        nextButton.onClick.AddListener(() =>
        {
            var n = Enum.GetNames(typeof(BuildingScript.BuildingTypes)).Length;
            CurrentBuildingType = (BuildingScript.BuildingTypes)(((byte)CurrentBuildingType + 1 + n) % n);
            CoreScript.Instance.Audio.PlayClick();
        });
        CurrentBuildingType = BuildingScript.BuildingTypes.Farm;
    }

    void Start()
    {
        var offscreen = GetComponent<OffScreenMenuScript>() as OffScreenMenuScript;
        CoreScript.Instance.GameStateChanged += (sender, e) =>
        {
            switch (e.NewState)
            {
                case CoreScript.GameStates.InBuildMode:
                    offscreen.Show();
                    break;
                case CoreScript.GameStates.InHelp:
                    break;
                default:
                    offscreen.Hide();
                    break;
            }
        };
    }

    void Update()
    {
        if (CoreScript.Instance.GameState == CoreScript.GameStates.InBuildMode
            && Input.GetMouseButtonDown(0)
            && RectTransformUtility.RectangleContainsScreenPoint(iconImage.rectTransform, Input.mousePosition, Camera.main) && CoreScript.Instance.Data.Gold >= CurrentPreparedBuilding.GoldCost)
        {
            CurrentPreparedBuilding.transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CurrentPreparedBuilding.gameObject.SetActive(true);
            PrepareBuildingOfType(CurrentBuildingType);
        }
    }
}