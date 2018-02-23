using UnityEngine;
using VillageKeeper.Game;

namespace VillageKeeper.UI
{
    public class BuildingPlacer : MonoBehaviour
    {
        private Building building;

        private void Start()
        {
            Core.Data.Game.BuildingPicker.SelectedBuildingType.OnValueChanged += OnSelectedBuildingTypeChanged;
            Core.Data.Game.BuildingPicker.IsBuildingPlacerVisible.OnValueChanged += OnIsBuildingPlacerVisibleChanged;
            gameObject.SetActive(false);
        }

        private void OnIsBuildingPlacerVisibleChanged()
        {
            gameObject.SetActive(Core.Data.Game.BuildingPicker.IsBuildingPlacerVisible);
            if (Core.Data.Game.BuildingPicker.IsBuildingPlacerVisible)
                transform.position = Input.mousePosition;
        }

        private void OnSelectedBuildingTypeChanged()
        {
            if (building != null)
                Destroy(building.gameObject);
            building = ResourceMock.GetBuilding(Core.Data.Game.BuildingPicker.SelectedBuildingType);
            building.transform.SetParent(transform, false);
        }

        private void Update()
        {
            transform.position = Input.mousePosition;

            if (Input.GetMouseButtonUp(0))
            {
                Core.Data.Game.BuildingPicker.IsBuildingPlacerVisible.Set(false);

                if (Input.GetMouseButtonUp(0))
                {
                    var buildingsArea = Core.Instance.BuildingsArea;
                    var closestCell = buildingsArea.GetClosestGridPosition(transform.localPosition);
                    var closestCellPosition = buildingsArea.GetWorldPositionByGridPosition(closestCell);
                    var distance = (Vector2)transform.localPosition - closestCellPosition;
                    if (Mathf.Abs(distance.x) <= buildingsArea.CellWorldSize.x / 2 && Mathf.Abs(distance.y) <= buildingsArea.CellWorldSize.y / 2 && Core.Data.Saved.Gold >= building.GoldCost)
                    {
                        buildingsArea.BuyBuilding(building, closestCell);
                    }
                }
            }
        }

        private void OnDestroy()
        {
            Core.Data.Game.BuildingPicker.SelectedBuildingType.OnValueChanged -= OnSelectedBuildingTypeChanged;
            Core.Data.Game.BuildingPicker.IsBuildingPlacerVisible.OnValueChanged -= OnIsBuildingPlacerVisibleChanged;
        }
    }
}