using Shibari;
using UnityEngine;
using VillageKeeper.Game;

namespace VillageKeeper.Model
{
    public class ResourceData : BindableData
    {
        [ShowInEditor]
        public CalculatedValue<Shibari.UI.SelectableSprites> MusicButtonSprites { get; } = new CalculatedValue<Shibari.UI.SelectableSprites>(
            () =>
            {
                if (Core.Data.Saved.IsMusicEnabled)
                    return ResourceMock.GetButtonSprites(Core.Data.ResourcePaths.EnabledMusicButtonSprites);
                else
                    return ResourceMock.GetButtonSprites(Core.Data.ResourcePaths.DisabledMusicButtonSprites);
            },
            Core.Data.Saved.IsMusicEnabled,
            Core.Data.ResourcePaths.EnabledMusicButtonSprites,
            Core.Data.ResourcePaths.DisabledMusicButtonSprites);

        [ShowInEditor]
        public CalculatedValue<Shibari.UI.SelectableSprites> SoundEffectsButtonSprites { get; } = new CalculatedValue<Shibari.UI.SelectableSprites>(
            () =>
            {
                if (Core.Data.Saved.IsSoundEffectsEnabled)
                    return ResourceMock.GetButtonSprites(Core.Data.ResourcePaths.EnabledSoundEffectsButtonSprites);
                else
                    return ResourceMock.GetButtonSprites(Core.Data.ResourcePaths.DisabledSoundEffectsButtonSprites);
            },
            Core.Data.Saved.IsSoundEffectsEnabled,
            Core.Data.ResourcePaths.EnabledSoundEffectsButtonSprites,
            Core.Data.ResourcePaths.DisabledSoundEffectsButtonSprites);

        [ShowInEditor]
        public ResourceValue<Sprite> ArcherHat { get; } = new ResourceValue<Sprite>(Core.Data.ResourcePaths.ArcherHat, Core.Data.Common.Special);

        [ShowInEditor]
        public ResourceValue<WatchtowerArrow> WatchtowerArrowPrefab { get; } = new ResourceValue<WatchtowerArrow>(Core.Data.ResourcePaths.WatchtowerArrowPrefab);

        [ShowInEditor]
        public ResourceValue<ArcherArrow> ArcherArrowPrefab { get; } = new ResourceValue<ArcherArrow>(Core.Data.ResourcePaths.ArcherArrowPrefab);

        [ShowInEditor]
        public ResourceValue<Sprite> BuildingTileDefaultSprite { get; } = new ResourceValue<Sprite>(Core.Data.ResourcePaths.BuildingTileDefaultSprite);

        [ShowInEditor]
        public ResourceValue<Sprite> BuildingTileHighlightedSprite { get; } = new ResourceValue<Sprite>(Core.Data.ResourcePaths.BuildingTileHighlightedSprite);

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
        public ResourceValue<Sprite> CurrentBuildingIcon { get; } = new ResourceValue<Sprite>(Core.Data.ResourcePaths.BuildingIcons, Core.Data.UI.BuildingPicker.SelectedBuildingType);
        [ShowInEditor]
        public CalculatedValue<Building> CurrentBuildingPrefab { get; } = new ResourceValue<Building>(Core.Data.ResourcePaths.BuildingPrefabs, Core.Data.UI.BuildingPicker.SelectedBuildingType);
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