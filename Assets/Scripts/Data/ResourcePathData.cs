using Shibari;

namespace VillageKeeper.Model
{
    public class ResourcePathData : BindableData
    {
        [SerializeValue]
        public PrimaryValue<string> CastleBackground { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> MountainsBackground { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> VillageBackground { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> Cliff { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> CastleUpgradeIcon { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> ArrowBar { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> CurrentBuildingPrefab { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> FarmCrops { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> BuildingTile { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> GhostPrefab { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> MenuFurniture { get; internal set; }
    }
}