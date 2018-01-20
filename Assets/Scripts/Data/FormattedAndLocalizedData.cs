using Shibari;

namespace VillageKeeper.Model
{
    public class FormattedAndLocalizedData : BindableData
    {
        [ShowInEditor]
        public SecondaryValue<string> MonstersKilled { get; } = new SecondaryValue<string>(
            () => string.Format(Data.Localization.MonstersSlainedFormat, Data.Saved.MonstersDefeated),
            Data.Localization.MonstersSlainedFormat,
            Data.Saved.MonstersDefeated);

        [ShowInEditor]
        public SecondaryValue<string> CurrentBuildingName { get; } = new SecondaryValue<string>(
            () => Data.Localization.BuildingNames.Get()[(int)Data.Game.SelectedBuildingType.Get()],
            Data.Localization.BuildingNames,
            Data.Game.SelectedBuildingType);

        [ShowInEditor]
        public SecondaryValue<string> CurrentBuildingPrice { get; } = new SecondaryValue<string>(
            () => Data.Balance.GetBuildingGoldCost(Data.Game.SelectedBuildingType).ToString(),
            Data.Game.SelectedBuildingType);

        [ShowInEditor]
        public SecondaryValue<string> CurrentBuildingDescription { get; } = new SecondaryValue<string>(
            () => Data.Localization.BuildingDescriptions.Get()[(int)Data.Game.SelectedBuildingType.Get()],
            Data.Localization.BuildingDescriptions,
            Data.Game.SelectedBuildingType);

        [ShowInEditor]
        public SecondaryValue<string> CurrentPauseMenuTitle { get; } = new SecondaryValue<string>(
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
        public SecondaryValue<string> CurrentTip { get; } = new SecondaryValue<string>(
            () =>
            {
                return Data.Localization.CurrentTips.Get()?[Data.Game.CurrentHelpTipIndex] ?? "";
            },
            Data.Game.CurrentHelpTipIndex,
            Data.Localization.CurrentTips);

        [ShowInEditor]
        public SecondaryValue<string> CurrentTipCounter { get; } = new SecondaryValue<string>(
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
        public SecondaryValue<string> CollectedGold { get; } = new SecondaryValue<string>(
            () => string.Format(Data.Localization.CollectedGoldFormat, Data.Game.RoundFinishedBonusGold),
            Data.Game.RoundFinishedBonusGold);
    }
}