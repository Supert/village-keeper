using Shibari;

namespace VillageKeeper.Model
{
    public class ResourceData : BindableData
    {
        //[ShowInEditor]
        public SecondaryValue<string> CastleBackground { get; }

        [SerializeValue]
        public PrimaryValue<string> CastleBackgroundFormat { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> MountainsBackgroundFormat { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> VillageBackgroundFormat { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> CliffFormat { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> CastleUpgradeIconFormat { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> ArrowBarFormat { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> BuildingPrefabsFormat { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> FarmCropsFormat { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> BuildingTileFormat { get; private set; }

        [SerializeValue]
        public PrimaryValue<string> GhostPrefab { get; private set; }

        [SerializeValue]
        public PrimaryValue<string> AdUnitId { get; private set; }

        public ResourceData()
        {
            CastleBackground = new SecondaryValue<string>(
            () => string.Format(CastleBackgroundFormat, Data.Common.Special, Data.Saved.VillageLevel),
            Data.Common.Special,
            Data.Saved.VillageLevel);
        }
    }
}