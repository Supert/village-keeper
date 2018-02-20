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
        public CalculatedValue<int> NextBreadToGoldMultiplier { get; } = new CalculatedValue<int>(() => Core.Data.Balance.GetBreadToGoldMultiplier(Core.Data.Saved.VillageLevel + 1), Core.Data.Saved.VillageLevel);
        [ShowInEditor]
        public CalculatedValue<int> CurrentBreadToGoldMultiplier { get; } = new CalculatedValue<int>(() => Core.Data.Balance.GetBreadToGoldMultiplier(Core.Data.Saved.VillageLevel), Core.Data.Saved.VillageLevel);
        [ShowInEditor]
        public CalculatedValue<int> CastleUpgradeCost { get; }

        [ShowInEditor]
        public CalculatedValue<bool> ShowPreviousTipButton { get; }

        [ShowInEditor]
        public CalculatedValue<bool> ShowNextTipButton { get; }

        public CalculatedValue<bool> IsArrowForceOverThreshold { get; }

        public GameData()
        {
            IsArrowForceOverThreshold = new CalculatedValue<bool>(() => ClampedArrowForce >= Core.Data.Balance.ArrowForceThreshold, Core.Data.Balance.ArrowForceThreshold);
            CastleUpgradeCost = new CalculatedValue<int>(() => Core.Data.Balance.GetCastleUpgradeCost(Core.Data.Saved.VillageLevel), Core.Data.Saved.VillageLevel);
            SelectedBuildingGoldCost = new CalculatedValue<int>(() => Core.Data.Balance.GetBuildingGoldCost(SelectedBuildingType), SelectedBuildingType);
            ShowPreviousTipButton = new CalculatedValue<bool>(() => CurrentHelpTipIndex > 0);
            ShowNextTipButton = new CalculatedValue<bool>(() => CurrentHelpTipIndex < (Core.Data.Localization.CurrentTips.Get()?.Length ?? 0), Core.Data.Localization.CurrentTips, CurrentHelpTipIndex);
        }

        [ShowInEditor]
        protected void NextHelpTip()
        {
            UnityEngine.Debug.Log("INVOKE");
            CurrentHelpTipIndex.Set(CurrentHelpTipIndex + 1);
        }

        [ShowInEditor]
        protected void PreviousHelpTip()
        {
            CurrentHelpTipIndex.Set(CurrentHelpTipIndex - 1);
        }
    }
}