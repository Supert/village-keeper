using UnityEngine;
using VillageKeeper.Game;

namespace VillageKeeper.UI
{
    public class BuildingPlacer : MonoBehaviour
    {
        private Building building;

        private void Start()
        {
            Core.Data.UI.BuildingPicker.SelectedBuildingType.OnValueChanged += OnSelectedBuildingTypeChanged;
            Core.Data.UI.BuildingPicker.IsBuildingPlacerVisible.OnValueChanged += OnIsBuildingPlacerVisibleChanged;

            OnSelectedBuildingTypeChanged();
            OnIsBuildingPlacerVisibleChanged();
            gameObject.SetActive(false);
        }

        private void OnIsBuildingPlacerVisibleChanged()
        {
            gameObject.SetActive(Core.Data.UI.BuildingPicker.IsBuildingPlacerVisible);
            if (Core.Data.UI.BuildingPicker.IsBuildingPlacerVisible)
                transform.position = Input.mousePosition;
        }

        private void OnSelectedBuildingTypeChanged()
        {
            if (building != null)
                Destroy(building.gameObject);
            GetNewBuilding();
        }

        private void GetNewBuilding()
        {
            building = ResourceMock.GetBuilding(Core.Data.UI.BuildingPicker.SelectedBuildingType);
            building.transform.SetParent(transform, false);
        }

        private void Update()
        {
            transform.position = Input.mousePosition;

            if (Input.GetMouseButtonUp(0))
            {
                Core.Data.UI.BuildingPicker.IsBuildingPlacerVisible.Set(false);
                BuildingTile tile = Core.Instance.BuildingsArea.GetClosestTile(transform.position);
                if (tile != null)
                {
                    Core.Instance.BuildingsArea.BuyBuilding(building, tile);
                    GetNewBuilding();
                }
            }
        }

        private void OnDestroy()
        {
            Core.Data.UI.BuildingPicker.SelectedBuildingType.OnValueChanged -= OnSelectedBuildingTypeChanged;
            Core.Data.UI.BuildingPicker.IsBuildingPlacerVisible.OnValueChanged -= OnIsBuildingPlacerVisibleChanged;
        }
    }
}