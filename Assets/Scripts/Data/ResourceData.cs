using Shibari;

namespace VillageKeeper.Data
{
    public class ResourceData : BindableData
    {
        public SerializableField<string> CastleBackground { get; private set; }
        public SerializableField<string> MountainsBackground { get; private set; }
        public SerializableField<string> VillageBackground { get; private set; }
        public SerializableField<string> Cliff { get; private set; }
        public SerializableField<string> CastleUpgradeIcon { get; private set; }
        public SerializableField<string> ArrowBar { get; private set; }
        public SerializableField<string> BuildingPrefabs { get; private set; }
    }
}