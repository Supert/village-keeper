using Shibari;

namespace VillageKeeper.Model
{
    public class FormattedAndLocalizedData : BindableData
    {
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
            Core.Data.Common.FsmState,
            Core.Data.Saved.SlainedMonstersCount,
            Core.Data.Common.FsmState);
    }
}