using Shibari;

namespace VillageKeeper.Model
{
    public class FormattedAndLocalizedData : BindableData
    {
        [ShowInEditor]
        public CalculatedValue<string> MonstersKilled { get; } = new CalculatedValue<string>(
            () => string.Format(Data.Localization.MonstersSlainedFormat, Data.Saved.MonstersDefeated),
            Data.Localization.MonstersSlainedFormat,
            Data.Saved.MonstersDefeated);

        [ShowInEditor]
        public CalculatedValue<string> CurrentBuildingName { get; } = new CalculatedValue<string>(
            () => Data.Localization.BuildingNames.Get()[(int)Data.Game.SelectedBuildingType.Get()],
            Data.Localization.BuildingNames,
            Data.Game.SelectedBuildingType);

        [ShowInEditor]
        public CalculatedValue<string> CurrentBuildingPrice { get; } = new CalculatedValue<string>(
            () => Data.Balance.GetBuildingGoldCost(Data.Game.SelectedBuildingType).ToString(),
            Data.Game.SelectedBuildingType);

        [ShowInEditor]
        public CalculatedValue<string> CurrentBuildingDescription { get; } = new CalculatedValue<string>(
            () => Data.Localization.BuildingDescriptions.Get()[(int)Data.Game.SelectedBuildingType.Get()],
            Data.Localization.BuildingDescriptions,
            Data.Game.SelectedBuildingType);

        [ShowInEditor]
        public CalculatedValue<string> CurrentPauseMenuTitle { get; } = new CalculatedValue<string>(
            () =>
            {
                if (Data.Common.FsmState.Get() == FSM.States.Pause)
                    return Data.Localization.Pause;
                if (Data.Common.FsmState.Get() == FSM.States.RoundFinished)
                    return Data.Localization.RoundFinished;
                return "";
            },
            Data.Common.FsmState,
            Data.Localization.Pause,
            Data.Localization.RoundFinished);

        [ShowInEditor]
        public CalculatedValue<string> CurrentTip { get; } = new CalculatedValue<string>(
            () =>
            {
                return Data.Localization.CurrentTips.Get()?[Data.Game.CurrentHelpTipIndex] ?? "";
            },
            Data.Game.CurrentHelpTipIndex,
            Data.Localization.CurrentTips);

        [ShowInEditor]
        public CalculatedValue<string> CurrentTipCounter { get; } = new CalculatedValue<string>(
            () =>
            {
                int totalTips = 0;
                if (Data.Common.FsmState.Get() == FSM.States.BattleHelp)
                    totalTips = Data.Localization.BattleHelpTips.Get().Length;
                if (Data.Common.FsmState.Get() == FSM.States.BuildHelp)
                    totalTips = Data.Localization.BuildHelpTips.Get().Length;
                return string.Format(Data.Localization.TipCounterFormat, Data.Game.CurrentHelpTipIndex + 1, totalTips);
            },
            Data.Common.FsmState,
            Data.Localization.BattleHelpTips,
            Data.Localization.BuildHelpTips,
            Data.Localization.TipCounterFormat,
            Data.Game.CurrentHelpTipIndex);

        [ShowInEditor]
        public CalculatedValue<string> CollectedGold { get; } = new CalculatedValue<string>(
            () => string.Format(Data.Localization.CollectedGoldFormat, Data.Game.RoundFinishedBonusGold),
            Data.Game.RoundFinishedBonusGold);
    }
}