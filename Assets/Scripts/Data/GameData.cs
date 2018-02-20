using System.Linq;
using Shibari;
using static VillageKeeper.Model.Data;

namespace VillageKeeper.Model
{
    public class GameData : BindableData
    {
        public AssignableValue<int> CurrentHelpTipIndex { get; } = new AssignableValue<int>();

        public CalculatedValue<int> RoundFinishedBonusGold { get; } = new CalculatedValue<int>(
            () =>
            {
                if (Core.Data.Common.FsmState == FSM.States.RoundFinished)
                {
                    int villageLevel = Core.Data.Saved.VillageLevel;
                    SerializableBuilding[] buildings = Core.Data.Saved.Buildings;
                    int farms = buildings?.Count(c => c.Type == BuildingTypes.Farm) ?? 0;
                    int windmills = buildings?.Count(c => c.Type == BuildingTypes.Windmill) ?? 0;
                    return Core.Data.Balance.CalculateRoundFinishedBonusGold(villageLevel, farms, windmills);
                }
                return 0;
            },
            Core.Data.Common.FsmState);

        [ShowInEditor]
        public AssignableValue<float> ClampedMonsterHealth { get; } = new AssignableValue<float>();
        [ShowInEditor]
        public AssignableValue<float> ClampedArrowForce { get; } = new AssignableValue<float>();

        public AssignableValue<BuildingTypes> SelectedBuildingType { get; } = new AssignableValue<BuildingTypes>();

        [ShowInEditor]
        public CalculatedValue<int> SelectedBuildingGoldCost { get; }

        [ShowInEditor]
        public CalculatedValue<int> NextBreadToGoldMultiplier { get; } = new CalculatedValue<int>(
            () => Core.Data.Balance.GetBreadToGoldMultiplier(Core.Data.Saved.VillageLevel + 1), 
            Core.Data.Saved.VillageLevel);

        [ShowInEditor]
        public CalculatedValue<int> CurrentBreadToGoldMultiplier { get; } = new CalculatedValue<int>(
            () => Core.Data.Balance.GetBreadToGoldMultiplier(Core.Data.Saved.VillageLevel), 
            Core.Data.Saved.VillageLevel);

        [ShowInEditor]
        public CalculatedValue<bool> IsUpgradeCastleButtonInteractable { get; } = new CalculatedValue<bool>(
            () => Core.Data.Saved.Gold.Get() >= Core.Data.Balance.GetCastleUpgradeCost(Core.Data.Saved.VillageLevel.Get()),
            Core.Data.Saved.Gold,
            Core.Data.Saved.VillageLevel);

        [ShowInEditor]
        public CalculatedValue<bool> IsCastleUpgradeWindowVisible { get; } = new CalculatedValue<bool>(
            () => Core.Data.Saved.VillageLevel < Core.Data.Balance.MaxVillageLevel,
            Core.Data.Saved.VillageLevel,
            Core.Data.Balance.MaxVillageLevel);
        
        [ShowInEditor]
        public CalculatedValue<int> CastleUpgradeCost { get; }
        
        [ShowInEditor]
        public CalculatedValue<bool> IsPreviousTipButtonInteractable { get; }

        [ShowInEditor]
        public CalculatedValue<bool> IsNextTipButtonInteractable { get; }


        public CalculatedValue<bool> IsArrowForceOverThreshold { get; }

        public GameData()
        {
            IsArrowForceOverThreshold = new CalculatedValue<bool>(() => ClampedArrowForce >= Core.Data.Balance.ArrowForceThreshold, Core.Data.Balance.ArrowForceThreshold);
            CastleUpgradeCost = new CalculatedValue<int>(() => Core.Data.Balance.GetCastleUpgradeCost(Core.Data.Saved.VillageLevel), Core.Data.Saved.VillageLevel);
            SelectedBuildingGoldCost = new CalculatedValue<int>(() => Core.Data.Balance.GetBuildingGoldCost(SelectedBuildingType), SelectedBuildingType);
            IsPreviousTipButtonInteractable = new CalculatedValue<bool>(() => CurrentHelpTipIndex > 0, CurrentHelpTipIndex);
            IsNextTipButtonInteractable = new CalculatedValue<bool>(() => CurrentHelpTipIndex + 1 < (Core.Data.Localization.CurrentTips.Get()?.Length ?? 0), Core.Data.Localization.CurrentTips, CurrentHelpTipIndex);
        }

        [ShowInEditor]
        protected void NextHelpTip()
        {
            CurrentHelpTipIndex.Set(CurrentHelpTipIndex + 1);
        }

        [ShowInEditor]
        protected void PreviousHelpTip()
        {
            CurrentHelpTipIndex.Set(CurrentHelpTipIndex - 1);
        }

        [ShowInEditor]
        protected void UpgradeCastle()
        {
            if (Core.Data.Saved.Gold.Get() >= Core.Data.Balance.GetCastleUpgradeCost(Core.Data.Saved.VillageLevel.Get()))
            {
                Core.Data.Saved.Gold.Set(Core.Data.Saved.Gold.Get() - Core.Data.Balance.GetCastleUpgradeCost(Core.Data.Saved.VillageLevel.Get()));
                Core.Data.Saved.VillageLevel.Set(Core.Data.Saved.VillageLevel.Get() + 1);
            }
        }
    }
}