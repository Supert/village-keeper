using Shibari;

namespace VillageKeeper.Model
{
    public class FormattedAndLocalizedData : BindableData
    {
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
            Core.Data.Saved.SlainedMonstersCount,
            Core.Data.Common.FsmState);

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