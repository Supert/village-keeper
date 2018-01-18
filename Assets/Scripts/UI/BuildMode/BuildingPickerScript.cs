using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using VillageKeeper.Game;
using VillageKeeper.Model;

namespace VillageKeeper.UI
{
    public class BuildingPickerScript : MonoBehaviour
    {
        public Image iconImage;

        public Button previousButton;
        public Button nextButton;

        private Dictionary<BuildingTypes, Sprite> iconSprites = null;

        private Dictionary<BuildingTypes, Building> preparedBuildings = new Dictionary<BuildingTypes, Building>();
        
        void PrepareBuilding()
        {
            BuildingTypes type = Data.Game.SelectedBuildingType;
            var bs = ResourceMock.GetBuilding(type);
            bs.gameObject.SetActive(false);
            if (!preparedBuildings.ContainsKey(type))
                preparedBuildings.Add(type, bs);
            else
                preparedBuildings[type] = bs;
        }

        void Start()
        {
            previousButton.onClick.AddListener(() =>
            {
                var n = Enum.GetNames(typeof(BuildingTypes)).Length;
                Data.Game.SelectedBuildingType.Set((BuildingTypes)(((int) Data.Game.SelectedBuildingType.Get() - 1 + n) % n));
                Core.Instance.AudioManager.PlayClick();
            });
            nextButton.onClick.AddListener(() =>
            {
                var n = Enum.GetNames(typeof(BuildingTypes)).Length;
                Data.Game.SelectedBuildingType.Set((BuildingTypes)(((int)Data.Game.SelectedBuildingType.Get() + 1 + n) % n));
                Core.Instance.AudioManager.PlayClick();
            });

            Data.Game.SelectedBuildingType.OnValueChanged += PrepareBuilding;

            Data.Game.SelectedBuildingType.Set(BuildingTypes.Farm);
        }

        void Update()
        {
            if (Core.Instance.FSM.Current == FSM.States.Build
                && Input.GetMouseButtonDown(0)
                && RectTransformUtility.RectangleContainsScreenPoint(iconImage.rectTransform, Input.mousePosition, Camera.main)
                && Data.Saved.Gold >= Data.Balance.GetBuildingGoldCost(Data.Game.SelectedBuildingType))
            {
                BuildingTypes type = Data.Game.SelectedBuildingType;
                var preparedBuilding = preparedBuildings[type];
                preparedBuilding.transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                preparedBuilding.gameObject.SetActive(true);
                PrepareBuilding();
            }
        }
    }
}