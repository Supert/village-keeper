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
    }
}