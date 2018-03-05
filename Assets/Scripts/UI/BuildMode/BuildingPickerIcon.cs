using UnityEngine;
using UnityEngine.EventSystems;

namespace VillageKeeper.UI
{
    public class BuildingPickerIcon : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            Core.Data.UI.BuildingPicker.IsBuildingPlacerVisible.Set(true);
        }
    }
}