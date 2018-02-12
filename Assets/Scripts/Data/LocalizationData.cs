using System;
using Shibari;
using VillageKeeper.FSM;

namespace VillageKeeper.Model
{
    public class LocalizationData : BindableData
    {
        public CalculatedValue<string[]> CurrentTips { get; }

        [SerializeValue, ShowInEditor]
        public AssignableValue<string> Help { get; } = new AssignableValue<string>();
        [SerializeValue, ShowInEditor]
        public AssignableValue<string> UpgradeCastleButtonText { get; } = new AssignableValue<string>();
        [SerializeValue, ShowInEditor]
        public AssignableValue<string> BuildingPickerMemo { get; } = new AssignableValue<string>();
        [SerializeValue, ShowInEditor]
        public AssignableValue<string> Shop { get; } = new AssignableValue<string>();
        [SerializeValue, ShowInEditor]
        public AssignableValue<string> GameName { get; } = new AssignableValue<string>();
        [SerializeValue, ShowInEditor]
        public AssignableValue<string> EndRoundTitle { get; } = new AssignableValue<string>();

        [SerializeValue]
        public AssignableValue<string> RoundFinishedBodyFormat { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> CollectedGoldFormat { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> TipCounterFormat { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> Pause { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> TipFormat { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string[]> BattleHelpTips { get; } = new AssignableValue<string[]>();
        [SerializeValue]
        public AssignableValue<string[]> BuildHelpTips { get; } = new AssignableValue<string[]>();
        [SerializeValue]
        public AssignableValue<string[]> BuildingDescriptions { get; } = new AssignableValue<string[]>();
        [SerializeValue]
        public AssignableValue<string[]> BuildingNames { get; } = new AssignableValue<string[]>();
        [SerializeValue]
        public AssignableValue<string> NoSlainedMonsters { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> FirstSlainedMonster { get; } = new AssignableValue<string>();
        [SerializeValue]
        public AssignableValue<string> MultipleSlainedMonstersFormat { get; } = new AssignableValue<string>();

        public LocalizationData()
        {
            CurrentTips = new CalculatedValue<string[]>(() =>
            {
                if (Core.Data.Common.FsmState == States.BattleHelp)
                {
                    Core.Data.Game.CurrentHelpTipIndex.Set(0);
                    return BattleHelpTips;
                }
                if (Core.Data.Common.FsmState == States.BuildHelp)
                {
                    Core.Data.Game.CurrentHelpTipIndex.Set(0);
                    return BuildHelpTips;
                }

                return CurrentTips;
            },
            Core.Data.Common.FsmState);
        }
    }

}