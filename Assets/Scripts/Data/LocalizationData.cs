using Shibari;

namespace VillageKeeper.Model
{
    public class LocalizationData : BindableData
    {
        [SerializeValue]
        public PrimaryValue<string> TipFormat { get; private set; }
        [SerializeValue]
        public PrimaryValue<string[]> BattleHelpTips { get; private set; }
        [SerializeValue]
        public PrimaryValue<string[]> BuildHelpTips { get; private set; }
        [SerializeValue]
        public PrimaryValue<string[]> BuildingDescriptions { get; private set; }
        [SerializeValue]
        public PrimaryValue<string[]> BuildingNames { get; private set; }
    }
}