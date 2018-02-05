using Shibari;

namespace VillageKeeper.Model
{
    public class ResourcePathData : BindableData
    {
        [SerializeValue]
        public AssignableValue<string> CastleBackground { get; private set; }
        [SerializeValue]
        public AssignableValue<string> MountainsBackground { get; private set; }
        [SerializeValue]
        public AssignableValue<string> VillageBackground { get; private set; }
        [SerializeValue]
        public AssignableValue<string> Cliff { get; private set; }
        [SerializeValue]
        public AssignableValue<string> CastleUpgradeIcon { get; private set; }
        [SerializeValue]
        public AssignableValue<string> ArrowBar { get; private set; }
        [SerializeValue]
        public AssignableValue<string> BuildingPrefabs { get; private set; }
        [SerializeValue]
        public AssignableValue<string> BuildingIcons { get; private set; }
        [SerializeValue]
        public AssignableValue<string> FarmCrops { get; private set; }
        [SerializeValue]
        public AssignableValue<string> BuildingTile { get; private set; }
        [SerializeValue]
        public AssignableValue<string> GhostPrefab { get; private set; }
        [SerializeValue]
        public AssignableValue<string> MenuFurniture { get; internal set; }
    }
}