using Shibari;

namespace VillageKeeper.Data
{
    public class ResourceData : BindableData
    {
        [SerializeValue]
        public BindableField<string> CastleBackground { get; private set; }
        [SerializeValue]
        public BindableField<string> MountainsBackground { get; private set; }
        [SerializeValue]
        public BindableField<string> VillageBackground { get; private set; }
        [SerializeValue]
        public BindableField<string> Cliff { get; private set; }
        [SerializeValue]
        public BindableField<string> CastleUpgradeIcon { get; private set; }
        [SerializeValue]
        public BindableField<string> ArrowBar { get; private set; }
        [SerializeValue]
        public BindableField<string> BuildingPrefabs { get; private set; }
        [SerializeValue]
        public BindableField<string> FarmCrops { get; private set; }
        [SerializeValue]
        public BindableField<string> BuildingTile { get; private set; }

        [SerializeValue]
        public BindableField<string> GhostPrefab { get; private set; }

        [SerializeValue]
        public BindableField<string> AdUnitId { get; private set; }
    }
}