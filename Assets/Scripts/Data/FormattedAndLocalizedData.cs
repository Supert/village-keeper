using Shibari;

namespace VillageKeeper.Model
{
    public class FormattedAndLocalizedData : BindableData
    {
        [ShowInEditor]
        public CalculatedValue<string> CurrentBuildingName { get; } = new CalculatedValue<string>(
            () => Core.Data.Localization.BuildingNames.Get()[(int)Core.Data.Game.SelectedBuildingType.Get()],
            Core.Data.Localization.BuildingNames,
            Core.Data.Game.SelectedBuildingType);

        [ShowInEditor]
        public CalculatedValue<string> CurrentBuildingPrice { get; } = new CalculatedValue<string>(
            () => Core.Data.Balance.GetBuildingGoldCost(Core.Data.Game.SelectedBuildingType).ToString(),
            Core.Data.Game.SelectedBuildingType);

        [ShowInEditor]
        public CalculatedValue<string> CurrentBuildingDescription { get; } = new CalculatedValue<string>(
            () => Core.Data.Localization.BuildingDescriptions.Get()[(int)Core.Data.Game.SelectedBuildingType.Get()],
            Core.Data.Localization.BuildingDescriptions,
            Core.Data.Game.SelectedBuildingType);

        [ShowInEditor]
        public CalculatedValue<string> CurrentPauseMenuTitle { get; } = new CalculatedValue<string>(
            () =>
            {
                if (Core.Data.Common.FsmState.Get() == FSM.States.Pause)
                    return Core.Data.Localization.Pause;
                if (Core.Data.Common.FsmState.Get() == FSM.States.RoundFinished)
                    return Core.Data.Localization.EndRoundTitle;
                return "";
            },
            Core.Data.Common.FsmState,
            Core.Data.Localization.Pause,
            Core.Data.Localization.EndRoundTitle);

        [ShowInEditor]
        public CalculatedValue<string> CurrentTip { get; } = new CalculatedValue<string>(
            () =>
            {
                return Core.Data.Localization.CurrentTips.Get()?[Core.Data.Game.CurrentHelpTipIndex] ?? "";
            },
            Core.Data.Game.CurrentHelpTipIndex,
            Core.Data.Localization.CurrentTips);

        [ShowInEditor]
        public CalculatedValue<string> CurrentTipCounter { get; } = new CalculatedValue<string>(
            () =>
            {
                int totalTips = 0;
                if (Core.Data.Common.FsmState.Get() == FSM.States.BattleHelp)
                    totalTips = Core.Data.Localization.BattleHelpTips.Get().Length;
                if (Core.Data.Common.FsmState.Get() == FSM.States.BuildHelp)
                    totalTips = Core.Data.Localization.BuildHelpTips.Get().Length;
                return string.Format(Core.Data.Localization.TipCounterFormat, Core.Data.Game.CurrentHelpTipIndex + 1, totalTips);
            },
            Core.Data.Common.FsmState,
            Core.Data.Localization.BattleHelpTips,
            Core.Data.Localization.BuildHelpTips,
            Core.Data.Localization.TipCounterFormat,
            Core.Data.Game.CurrentHelpTipIndex);

        [ShowInEditor]
        public CalculatedValue<string> CollectedGold { get; } = new CalculatedValue<string>(
            () => string.Format(Core.Data.Localization.CollectedGoldFormat, Core.Data.Game.RoundFinishedBonusGold),
            Core.Data.Game.RoundFinishedBonusGold);

        [ShowInEditor]
        public CalculatedValue<string> SlainedMonsters { get; } = new CalculatedValue<string>(
            () =>
            {
                var slainedMonstersCount = Core.Data.Saved.SlainedMonstersCount;
                if (slainedMonstersCount == 0)
                    return Core.Data.Localization.NoSlainedMonsters;
                else
                {
                    if (slainedMonstersCount == 1)
                        return Core.Data.Localization.FirstSlainedMonster;
                    else
                        return string.Format(Core.Data.Localization.MultipleSlainedMonstersFormat, slainedMonstersCount);
                }
            },
            Core.Data.Saved.SlainedMonstersCount);

        [ShowInEditor]
        public CalculatedValue<string> RoundFinished { get; } = new CalculatedValue<string>(
            () =>
            {
                return string.Format(Core.Data.Localization.RoundFinishedBodyFormat, Core.Data.Game.RoundFinishedBonusGold);
            },
            Core.Data.Game.RoundFinishedBonusGold,
            Core.Data.Localization.RoundFinishedBodyFormat);
    }
}