using Shibari;
using UnityEngine;
using VillageKeeper.Game;
using static VillageKeeper.Model.Data;

namespace VillageKeeper.Model
{
    public class ResourceData : BindableData
    {
        [ShowInEditor]
        public ResourceValue<Sprite> CastleBackground { get; } = new ResourceValue<Sprite>(ResourcePaths.CastleBackground.Get, Common.Special, Saved.VillageLevel);
        [ShowInEditor]
        public ResourceValue<Sprite> MountainsBackground { get; } = new ResourceValue<Sprite>(ResourcePaths.MountainsBackground.Get, Common.Special);
        [ShowInEditor]
        public ResourceValue<Sprite> VillageBackground { get; } = new ResourceValue<Sprite>(ResourcePaths.VillageBackground.Get, Common.Special);
        [ShowInEditor]
        public ResourceValue<Sprite> Cliff { get; } = new ResourceValue<Sprite>(ResourcePaths.Cliff.Get, Common.Special);
        [ShowInEditor]
        public ResourceValue<Sprite> CastleUpgradeIcon { get; } = new ResourceValue<Sprite>(ResourcePaths.CastleUpgradeIcon.Get, Saved.VillageLevel);
        [ShowInEditor]
        public ResourceValue<Sprite> ArrowBar { get; } = new ResourceValue<Sprite>(ResourcePaths.ArrowBar.Get, Data.Game.IsArrowForceOverThreshold);
        [ShowInEditor]
        public SecondaryValue<Building> CurrentBuildingPrefab { get; } = new ResourceValue<Building>(ResourcePaths.CurrentBuildingPrefab.Get, Data.Game.SelectedBuildingType);
        [ShowInEditor]
        public ResourceValue<Sprite> FarmCrops { get; } = new ResourceValue<Sprite>(ResourcePaths.FarmCrops.Get, Common.Special);
        [ShowInEditor]
        public ResourceValue<Sprite> MenuFurniture { get; } = new ResourceValue<Sprite>(ResourcePaths.MenuFurniture.Get, Common.Special);
        [ShowInEditor]
        public ResourceValue<BuildingTileScript> BuildingTile { get; } = new ResourceValue<BuildingTileScript>(ResourcePaths.BuildingTile.Get);
        [ShowInEditor]
        public ResourceValue<Ghost> GhostPrefab { get; } = new ResourceValue<Ghost>(ResourcePaths.GhostPrefab.Get);
        [SerializeValue]
        public PrimaryValue<string> AdUnitId { get; private set; }
    }
}