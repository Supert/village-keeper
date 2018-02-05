using System.Linq;
using Shibari;
using static VillageKeeper.Model.Data;

namespace VillageKeeper.Model
{
    public class GameData : BindableData
    {
        public AssignableValue<int> CurrentHelpTipIndex { get; private set; }
        
        public CalculatedValue<int> RoundFinishedBonusGold { get; } = new CalculatedValue<int>(
            () =>
            {
                if (Common.FsmState == FSM.States.RoundFinished)
                {
                    int villageLevel = Saved.VillageLevel;
                    SerializableBuilding[] buildings = Saved.Buildings;
                    int farms = buildings?.Count(c => c.Type == BuildingTypes.Farm) ?? 0;
                    int windmills = buildings?.Count(c => c.Type == BuildingTypes.Windmill) ?? 0;
                    return Balance.CalculateRoundFinishedBonusGold(villageLevel, farms, windmills);
                }
                return 0;
            },
            Common.FsmState);

        [ShowInEditor]
        public AssignableValue<float> ClampedMonsterHealth { get; private set; }
        [ShowInEditor]
        public AssignableValue<float> ClampedArrowForce { get; private set; }

        public AssignableValue<BuildingTypes> SelectedBuildingType { get; internal set; }

        [ShowInEditor]
        public CalculatedValue<int> NextBreadToGoldMultiplier { get; } = new CalculatedValue<int>(() => Balance.GetBreadToGoldMultiplier(Saved.VillageLevel + 1), Saved.VillageLevel);
        [ShowInEditor]
        public CalculatedValue<int> CurrentBreadToGoldMultiplier { get; } = new CalculatedValue<int>(() => Balance.GetBreadToGoldMultiplier(Saved.VillageLevel), Saved.VillageLevel);
        [ShowInEditor]
        public CalculatedValue<int> CastleUpgradeCost { get; }

        public CalculatedValue<bool> IsArrowForceOverThreshold { get; }

        public GameData()
        {
            IsArrowForceOverThreshold = new CalculatedValue<bool>(() => ClampedArrowForce >= Balance.ArrowForceThreshold, Balance.ArrowForceThreshold);
            CastleUpgradeCost = new CalculatedValue<int>(() => Balance.GetCastleUpgradeCost(Saved.VillageLevel), Saved.VillageLevel);
        }
    }
}