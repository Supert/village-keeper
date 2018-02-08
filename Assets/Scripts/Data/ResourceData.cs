using Shibari;
using UnityEngine;
using VillageKeeper.Game;
using static VillageKeeper.Model.Data;

namespace VillageKeeper.Model
{
    public class ResourceData : BindableData
    {
        [ShowInEditor]
        public ResourceValue<Sprite> CastleBackground { get; } = new ResourceValue<Sprite>(ResourcePaths.CastleBackground, Common.Special, Saved.VillageLevel);
        [ShowInEditor]
        public ResourceValue<Sprite> MountainsBackground { get; } = new ResourceValue<Sprite>(ResourcePaths.MountainsBackground, Common.Special);
        [ShowInEditor]
        public ResourceValue<Sprite> VillageBackground { get; } = new ResourceValue<Sprite>(ResourcePaths.VillageBackground, Common.Special);
        [ShowInEditor]
        public ResourceValue<Sprite> Cliff { get; } = new ResourceValue<Sprite>(ResourcePaths.Cliff, Common.Special);
        [ShowInEditor]
        public ResourceValue<Sprite> CastleUpgradeIcon { get; } = new ResourceValue<Sprite>(ResourcePaths.CastleUpgradeIcon, Saved.VillageLevel);
        [ShowInEditor]
        public ResourceValue<Sprite> ArrowBar { get; } = new ResourceValue<Sprite>(ResourcePaths.ArrowBar, Data.Game.IsArrowForceOverThreshold);
        [ShowInEditor]
        public ResourceValue<Sprite> CurrentBuildingIcon { get; } = new ResourceValue<Sprite>(ResourcePaths.BuildingIcons, Data.Game.SelectedBuildingType);
        [ShowInEditor]
        public CalculatedValue<Building> CurrentBuildingPrefab { get; } = new ResourceValue<Building>(ResourcePaths.BuildingPrefabs, Data.Game.SelectedBuildingType);
        [ShowInEditor]
        public ResourceValue<Sprite> FarmCrops { get; } = new ResourceValue<Sprite>(ResourcePaths.FarmCrops, Common.Special);
        [ShowInEditor]
        public ResourceValue<Sprite> MenuFurniture { get; } = new ResourceValue<Sprite>(ResourcePaths.MenuFurniture, Saved.HasPremium);
        [ShowInEditor]
        public ResourceValue<GameObject> BuildingTile { get; } = new ResourceValue<GameObject>(ResourcePaths.BuildingTile);
        [ShowInEditor]
        public ResourceValue<Ghost> GhostPrefab { get; } = new ResourceValue<Ghost>(ResourcePaths.GhostPrefab);

        [SerializeValue]
        public AssignableValue<string> AdUnitId { get; } = new AssignableValue<string>();
    }
}