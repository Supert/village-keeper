using System;
using Shibari;

namespace VillageKeeper.Model
{
    public class BuildingPicker : BindableData
    {
        [ShowInEditor]
        public CalculatedValue<string> CurrentBuildingName { get; }

        [ShowInEditor]
        public CalculatedValue<string> CurrentBuildingPrice { get; }

        [ShowInEditor]
        public CalculatedValue<string> CurrentBuildingDescription { get; }

        public AssignableValue<bool> IsBuildingPlacerVisible { get; } = new AssignableValue<bool>();

        public AssignableValue<BuildingTypes> SelectedBuildingType { get; } = new AssignableValue<BuildingTypes>();

        [ShowInEditor]
        public CalculatedValue<int> SelectedBuildingGoldCost { get; }

        public BuildingPicker()
        {
            SelectedBuildingGoldCost = new CalculatedValue<int>(() => Core.Data.Balance.GetBuildingGoldCost(SelectedBuildingType), SelectedBuildingType);

            CurrentBuildingName = new CalculatedValue<string>(
               () => Core.Data.Localization.BuildingNames.Get()[(int)SelectedBuildingType.Get()],
               Core.Data.Localization.BuildingNames,
               SelectedBuildingType);

            CurrentBuildingPrice = new CalculatedValue<string>(
                () => Core.Data.Balance.GetBuildingGoldCost(SelectedBuildingType).ToString(),
                SelectedBuildingType);

            CurrentBuildingDescription = new CalculatedValue<string>(
                () => Core.Data.Localization.BuildingDescriptions.Get()[(int)SelectedBuildingType.Get()],
                Core.Data.Localization.BuildingDescriptions,
                SelectedBuildingType);
        }

        [ShowInEditor]
        protected void SelectNextBuildingType()
        {
            var n = Enum.GetNames(typeof(BuildingTypes)).Length;
            SelectedBuildingType.Set((BuildingTypes)(((int)SelectedBuildingType.Get() + 1 + n) % n));
        }

        [ShowInEditor]
        protected void SelectPreviousBuildingType()
        {
            var n = Enum.GetNames(typeof(BuildingTypes)).Length;
            SelectedBuildingType.Set((BuildingTypes)(((int)SelectedBuildingType.Get() - 1 + n) % n));
        }

        public override void Initialize()
        {
            base.Initialize();
            Core.Data.UI.BuildingPicker.SelectedBuildingType.Set(BuildingTypes.Farm);
        }
    }
}