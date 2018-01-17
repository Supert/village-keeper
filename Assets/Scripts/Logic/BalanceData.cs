using Shibari;
using System.Linq;

namespace VillageKeeper.Model
{
    public class BalanceData : BindableData
    {
        [SerializeValue]
        public PrimaryValue<float> ArrowForceThreshold { get; private set; }
        [SerializeValue]
        public PrimaryValue<int> MonsterBonusGold { get; private set; }
        [SerializeValue]
        public PrimaryValue<int> MaxVillageLevel { get; private set; }

        [SerializeValue]
        protected PrimaryValue<int> MonsterMaxHealthPossible { get; private set; }
        [SerializeValue]
        protected PrimaryValue<int> MonsterMinHealthPossible { get; private set; }
        [SerializeValue]
        protected PrimaryValue<float> MonsterBuildingCostModifier { get; private set; }
        [SerializeValue]
        protected PrimaryValue<float> MonsterPowerPointsBuildingsHealthModifier { get; private set; }
        [SerializeValue]
        protected PrimaryValue<float> MonsterPowerPointsVillageLevelModifier { get; private set; }

        [SerializeValue]
        protected PrimaryValue<int> FoodPerFarm { get; set; }
        [SerializeValue]
        protected PrimaryValue<int> FoodPerWindMillMultiplier { get; set; }
        [SerializeValue]
        protected PrimaryValue<int[]> CastleUpgradeCosts { get; set; }
        [SerializeValue]
        protected PrimaryValue<float[]> BuildingMaxHealths { get; set; }
        [SerializeValue]
        protected PrimaryValue<int[]> BuildingCosts { get; set; }

        public float GetMonsterPowerPoints()
        {
            float buildingsHealth = Core.Instance.BuildingsArea.buildings.Sum(b => b.MaxHealth);
            return buildingsHealth * MonsterPowerPointsBuildingsHealthModifier * (1 + Data.Saved.VillageLevel * MonsterPowerPointsVillageLevelModifier);
        }

        public float GetMonsterMinHealth()
        {
            float buildingsCost = Core.Instance.BuildingsArea.buildings.Sum(b => b.GoldCost);
            return buildingsCost * MonsterBuildingCostModifier + MonsterMinHealthPossible;
        }

        public float GetMonsterMaxHealth()
        {
            return MonsterMaxHealthPossible;
        }

        public float GetBuildingMaxHealth(BuildingTypes type)
        {
            return BuildingMaxHealths.Get()[(int)type];
        }

        public int GetBuildingGoldCost(BuildingTypes type)
        {
            return BuildingCosts.Get()[(int)type];
        }

        public int GetBreadToGoldMultiplier(int villageLevel)
        {
            return villageLevel + 1;
        }

        public int CalculateFarmsFood(int farms)
        {
            return farms * FoodPerFarm;
        }

        public int CalculateWindmillsFood(int windmills, int farms)
        {
            return windmills * farms * FoodPerWindMillMultiplier;
        }

        public int CalculateRoundFinishedBonusGold(int villageLevel, int farms, int windmills)
        {
            int result = MonsterBonusGold;
            int food = CalculateFarmsFood(farms) + CalculateWindmillsFood(windmills, farms);
            result += food * GetBreadToGoldMultiplier(villageLevel);
            return result;
        }

        public int GetCastleUpgradeCost(int villageLevel)
        {
            return CastleUpgradeCosts.Get()[villageLevel];
        }
    }
}