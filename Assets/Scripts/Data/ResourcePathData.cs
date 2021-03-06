﻿using Shibari;

namespace VillageKeeper.Model
{
    public class ResourcePathData : BindableData
    {
        [SerializeValue]
        public AssignableValue<string> ArcherHat { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> WatchtowerArrowPrefab { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> ArcherArrowPrefab { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> BuildingTileDefaultSprite { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> BuildingTileHighlightedSprite { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string[]> EnabledMusicButtonSprites { get; } = new AssignableValue<string[]>();
        [SerializeValue]
        public AssignableValue<string[]> EnabledSoundEffectsButtonSprites { get; } = new AssignableValue<string[]>();
        [SerializeValue]
        public AssignableValue<string[]> DisabledMusicButtonSprites { get; } = new AssignableValue<string[]>();
        [SerializeValue]
        public AssignableValue<string[]> DisabledSoundEffectsButtonSprites { get; } = new AssignableValue<string[]>();
        [SerializeValue]
        public AssignableValue<string> CastleBackground { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> MountainsBackground { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> VillageBackground { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> Cliff { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> CastleUpgradeIcon { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> ArrowBar { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> BuildingPrefabs { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> BuildingIcons { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> FarmCrops { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> BuildingTile { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> GhostPrefab { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> MenuFurniture { get; } = new AssignableValue<string>();
    }
}