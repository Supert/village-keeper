using UnityEngine;
using UnityEngine.EventSystems;
using VillageKeeper.Model;

namespace VillageKeeper.UI
{
    public class BuildingPickerIcon : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            Core.Data.Game.BuildingPicker.IsBuildingPlacerVisible.Set(true);
        }

        void Start()
        {
            Core.Data.Game.BuildingPicker.SelectedBuildingType.Set(BuildingTypes.Farm);
        }
    }
}