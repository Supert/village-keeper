using Shibari;
using UnityEngine;
using VillageKeeper.Game;
using static VillageKeeper.Model.Data;

namespace VillageKeeper.Model
{
    public class ResourceData : BindableData
    {
        [ShowInEditor]
        public ResourceValue<Sprite> CastleBackground { get; } = new ResourceValue<Sprite>(Core.Data.ResourcePaths.CastleBackground, Core.Data.Common.Special, Core.Data.Saved.VillageLevel);
        [ShowInEditor]
        public ResourceValue<Sprite> MountainsBackground { get; } = new ResourceValue<Sprite>(Core.Data.ResourcePaths.MountainsBackground, Core.Data.Common.Special);
        [ShowInEditor]
        public ResourceValue<Sprite> VillageBackground { get; } = new ResourceValue<Sprite>(Core.Data.ResourcePaths.VillageBackground, Core.Data.Common.Special);
        [ShowInEditor]
        public ResourceValue<Sprite> Cliff { get; } = new ResourceValue<Sprite>(Core.Data.ResourcePaths.Cliff, Core.Data.Common.Special);
        [ShowInEditor]
        public ResourceValue<Sprite> CastleUpgradeIcon { get; } = new ResourceValue<Sprite>(Core.Data.ResourcePaths.CastleUpgradeIcon, Core.Data.Saved.VillageLevel);
        [ShowInEditor]
        public ResourceValue<Sprite> ArrowBar { get; } = new ResourceValue<Sprite>(Core.Data.ResourcePaths.ArrowBar, Core.Data.Game.IsArrowForceOverThreshold);
        [ShowInEditor]
        public ResourceValue<Sprite> CurrentBuildingIcon { get; } = new ResourceValue<Sprite>(Core.Data.ResourcePaths.BuildingIcons, Core.Data.Game.SelectedBuildingType);
        [ShowInEditor]
        public CalculatedValue<Building> CurrentBuildingPrefab { get; } = new ResourceValue<Building>(Core.Data.ResourcePaths.BuildingPrefabs, Core.Data.Game.SelectedBuildingType);
        [ShowInEditor]
        public ResourceValue<Sprite> FarmCrops { get; } = new ResourceValue<Sprite>(Core.Data.ResourcePaths.FarmCrops, Core.Data.Common.Special);
        [ShowInEditor]
        public ResourceValue<Sprite> MenuFurniture { get; } = new ResourceValue<Sprite>(Core.Data.ResourcePaths.MenuFurniture, Core.Data.Saved.HasPremium);
        [ShowInEditor]
        public ResourceValue<GameObject> BuildingTile { get; } = new ResourceValue<GameObject>(Core.Data.ResourcePaths.BuildingTile);
        [ShowInEditor]
        public ResourceValue<Ghost> GhostPrefab { get; } = new ResourceValue<Ghost>(Core.Data.ResourcePaths.GhostPrefab);

        [SerializeValue]
        public AssignableValue<string> AdUnitId { get; } = new AssignableValue<string>();
    }
}