using Shibari;

namespace VillageKeeper.Model
{
    public class LocalizationData : BindableData
    {
        [SerializeValue, ShowInEditor]
        public PrimaryValue<string> Help { get; private set; }
        [SerializeValue, ShowInEditor]
        public PrimaryValue<string> UpgradeCastleButtonText { get; private set; }
        [SerializeValue, ShowInEditor]
        public PrimaryValue<string> BuildingPickerMemo { get; private set; }
        [SerializeValue, ShowInEditor]
        public PrimaryValue<string> Shop { get; private set; }
        [SerializeValue, ShowInEditor]
        public PrimaryValue<string> GameName { get; private set; }


        [SerializeValue]
        public PrimaryValue<string> TipCounterFormat { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> Pause { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> RoundFinished { get; private set; }
        [SerializeValue]
        public PrimaryValue<string> MonstersSlainedFormat { get; private set; }
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