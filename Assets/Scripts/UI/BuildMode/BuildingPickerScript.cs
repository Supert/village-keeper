using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using VillageKeeper.Game;
using VillageKeeper.Data;

namespace VillageKeeper.UI
{
    public class BuildingPickerScript : MonoBehaviour
    {
        public Text nameText;
        public Text priceText;
        public Text descriptionText;
        public Image iconImage;

        public Button previousButton;
        public Button nextButton;

        private Dictionary<BuildingTypes, Sprite> iconSprites = null;

        private Dictionary<BuildingTypes, BuildingScript> buildingsPrepared = new Dictionary<BuildingTypes, BuildingScript>();

        private BuildingTypes currentBuildingType;

        private Sprite GetSpriteForIconByType(BuildingTypes type)
        {
            if (iconSprites == null || iconSprites.Count == 0)
            {
                iconSprites = new Dictionary<BuildingTypes, Sprite>();
                foreach (BuildingTypes v in Enum.GetValues(typeof(BuildingTypes)))
                {
                    iconSprites.Add(v, Resources.Load<Sprite>("Buildings/Icons/" + Enum.GetName(typeof(BuildingTypes), v)) as Sprite);
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

        public BuildingTypes CurrentBuildingType
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

        void PrepareBuildingOfType(BuildingTypes type)
        {
            var bs = BuildingScript.GetNewBuildingOfType(type);
            bs.gameObject.SetActive(false);
            if (!buildingsPrepared.ContainsKey(type))
                buildingsPrepared.Add(type, bs);
            else
                buildingsPrepared[type] = bs;
        }

        void Start()
        {
            foreach (BuildingTypes v in Enum.GetValues(typeof(BuildingTypes)))
            {
                PrepareBuildingOfType(v);
            }

            previousButton.onClick.AddListener(() =>
            {
                var n = Enum.GetNames(typeof(BuildingTypes)).Length;
                CurrentBuildingType = (BuildingTypes)(((byte)CurrentBuildingType - 1 + n) % n);
                Core.Instance.AudioManager.PlayClick();
            });
            nextButton.onClick.AddListener(() =>
            {
                var n = Enum.GetNames(typeof(BuildingTypes)).Length;
                CurrentBuildingType = (BuildingTypes)(((byte)CurrentBuildingType + 1 + n) % n);
                Core.Instance.AudioManager.PlayClick();
            });
            CurrentBuildingType = BuildingTypes.Farm;
        }

        void Update()
        {
            if (Core.Instance.FSM.Current == FSM.States.Build
                && Input.GetMouseButtonDown(0)
                && RectTransformUtility.RectangleContainsScreenPoint(iconImage.rectTransform, Input.mousePosition, Camera.main)
                && Core.Instance.SavedData.Gold.Get() >= CurrentPreparedBuilding.GoldCost)
            {
                CurrentPreparedBuilding.transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                CurrentPreparedBuilding.gameObject.SetActive(true);
                PrepareBuildingOfType(CurrentBuildingType);
            }
        }
    }
}