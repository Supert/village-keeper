using System.Linq;
using Shibari;
using static VillageKeeper.Model.Data;

namespace VillageKeeper.Model
{
    public class GameData : BindableData
    {
        public PrimaryValue<int> CurrentHelpTipIndex { get; private set; }
        
        public SecondaryValue<int> RoundFinishedBonusGold { get; } = new SecondaryValue<int>(
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
        public PrimaryValue<float> ClampedMonsterHealth { get; private set; }
        [ShowInEditor]
        public PrimaryValue<float> ClampedArrowForce { get; private set; }

        public PrimaryValue<BuildingTypes> SelectedBuildingType { get; internal set; }

        [ShowInEditor]
        public SecondaryValue<int> NextBreadToGoldMultiplier { get; } = new SecondaryValue<int>(() => Balance.GetBreadToGoldMultiplier(Saved.VillageLevel + 1), Saved.VillageLevel);
        [ShowInEditor]
        public SecondaryValue<int> CurrentBreadToGoldMultiplier { get; } = new SecondaryValue<int>(() => Balance.GetBreadToGoldMultiplier(Saved.VillageLevel), Saved.VillageLevel);
        [ShowInEditor]
        public SecondaryValue<int> CastleUpgradeCost { get; }

        public SecondaryValue<bool> IsArrowForceOverThreshold { get; }

        public GameData()
        {
            IsArrowForceOverThreshold = new SecondaryValue<bool>(() => ClampedArrowForce >= Balance.ArrowForceThreshold, Balance.ArrowForceThreshold);
            CastleUpgradeCost = new SecondaryValue<int>(() => Balance.GetCastleUpgradeCost(Saved.VillageLevel), Saved.VillageLevel);
        }
    }
}