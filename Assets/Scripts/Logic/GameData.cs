using System.Linq;
using Shibari;
using static VillageKeeper.Model.Data;

namespace VillageKeeper.Model
{
    public class GameData : BindableData
    {
        public PrimaryValue<int> CurrentHelpTip { get; private set; }
        public PrimaryValue<int> HelpTipsCount { get; private set; }

        public PrimaryValue<int> TotalFood { get; private set; }

        public PrimaryValue<int> RoundFinishedBonusGold { get; private set; }

        [ShowInEditor]
        public PrimaryValue<float> ClampedMonsterHealth { get; private set; }
        [ShowInEditor]
        public PrimaryValue<float> ClampedArrowForce { get; private set; }

        public PrimaryValue<BuildingTypes> SelectedBuildingType { get; internal set; }

        public SecondaryValue<int> NextBreadToGoldMultiplier { get; private set; }
        public SecondaryValue<int> CurrentBreadToGoldMultiplier { get; private set; }

        [ShowInEditor]
        public SecondaryValue<int> CastleUpgradeCost { get; }
        public SecondaryValue<bool> IsArrowForceOverThreshold { get; }

        public GameData()
        {
            IsArrowForceOverThreshold = new SecondaryValue<bool>(() => ClampedArrowForce >= Balance.ArrowForceThreshold, Balance.ArrowForceThreshold);
            CastleUpgradeCost = new SecondaryValue<int>(() => Balance.GetCastleUpgradeCost(Saved.VillageLevel), Saved.VillageLevel);
        }

        public void Init()
        {
            CalculateEconomy();

            Saved.VillageLevel.OnValueChanged += CalculateEconomy;

            Core.Instance.FSM.SubscribeToEnter(FSM.States.RoundFinished, CalculateEconomy);

            CurrentBreadToGoldMultiplier = new SecondaryValue<int>(() => Balance.GetBreadToGoldMultiplier(Saved.VillageLevel), Saved.VillageLevel);
            NextBreadToGoldMultiplier = new SecondaryValue<int>(() => Balance.GetBreadToGoldMultiplier(Saved.VillageLevel + 1), Saved.VillageLevel);
        }

        public void CalculateEconomy()
        {
            int villageLevel = Saved.VillageLevel;
            SerializableBuilding[] buildings = Saved.Buildings;
            int farms = buildings?.Count(c => c.Type == BuildingTypes.Farm) ?? 0;
            int windmills = buildings?.Count(c => c.Type == BuildingTypes.Windmill) ?? 0;
            
            RoundFinishedBonusGold.Set(Balance.CalculateRoundFinishedBonusGold(villageLevel, farms, windmills));

            TotalFood.Set((Balance.CalculateFarmsFood(farms) + Balance.CalculateWindmillsFood(windmills, farms)));
        }
    }
}