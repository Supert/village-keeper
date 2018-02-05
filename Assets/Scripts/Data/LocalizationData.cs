using Shibari;
using VillageKeeper.FSM;

namespace VillageKeeper.Model
{
    public class LocalizationData : BindableData
    {
        public CalculatedValue<string[]> CurrentTips { get; }

        [SerializeValue, ShowInEditor]
        public AssignableValue<string> Help { get; private set; }
        [SerializeValue, ShowInEditor]
        public AssignableValue<string> UpgradeCastleButtonText { get; private set; }
        [SerializeValue, ShowInEditor]
        public AssignableValue<string> BuildingPickerMemo { get; private set; }
        [SerializeValue, ShowInEditor]
        public AssignableValue<string> Shop { get; private set; }
        [SerializeValue, ShowInEditor]
        public AssignableValue<string> GameName { get; private set; }

        [SerializeValue]
        public AssignableValue<string> CollectedGoldFormat { get; private set; }
        [SerializeValue]
        public AssignableValue<string> TipCounterFormat { get; private set; }
        [SerializeValue]
        public AssignableValue<string> Pause { get; private set; }
        [SerializeValue]
        public AssignableValue<string> RoundFinished { get; private set; }
        [SerializeValue]
        public AssignableValue<string> MonstersSlainedFormat { get; private set; }
        [SerializeValue]
        public AssignableValue<string> TipFormat { get; private set; }
        [SerializeValue]
        public AssignableValue<string[]> BattleHelpTips { get; private set; }
        [SerializeValue]
        public AssignableValue<string[]> BuildHelpTips { get; private set; }
        [SerializeValue]
        public AssignableValue<string[]> BuildingDescriptions { get; private set; }
        [SerializeValue]
        public AssignableValue<string[]> BuildingNames { get; private set; }

        public LocalizationData()
        {
            CurrentTips = new CalculatedValue<string[]>(() =>
            {
                if (Data.Common.FsmState == States.BattleHelp)
                {
                    Data.Game.CurrentHelpTipIndex.Set(0);
                    return BattleHelpTips;
                }
                if (Data.Common.FsmState == States.BuildHelp)
                {
                    Data.Game.CurrentHelpTipIndex.Set(0);
                    return BuildHelpTips;
                }

                return CurrentTips;
            },
            Data.Common.FsmState);
        }
    }

}