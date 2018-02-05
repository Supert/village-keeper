using Shibari;
using System.Linq;

namespace VillageKeeper.Model
{
    public class BalanceData : BindableData
    {
        [SerializeValue]
        public AssignableValue<float> ArrowForceThreshold { get; private set; }
        [SerializeValue]
        public AssignableValue<int> MonsterBonusGold { get; private set; }
        [SerializeValue]
        public AssignableValue<int> MaxVillageLevel { get; private set; }

        [SerializeValue]
        protected AssignableValue<int> MonsterMaxHealthPossible { get; private set; }
        [SerializeValue]
        protected AssignableValue<int> MonsterMinHealthPossible { get; private set; }
        [SerializeValue]
        protected AssignableValue<float> MonsterBuildingCostModifier { get; private set; }
        [SerializeValue]
        protected AssignableValue<float> MonsterPowerPointsBuildingsHealthModifier { get; private set; }
        [SerializeValue]
        protected AssignableValue<float> MonsterPowerPointsVillageLevelModifier { get; private set; }

        [SerializeValue]
        protected AssignableValue<int> FoodPerFarm { get; set; }
        [SerializeValue]
        protected AssignableValue<int> FoodPerWindMillMultiplier { get; set; }
        [SerializeValue]
        protected AssignableValue<int[]> CastleUpgradeCosts { get; set; }
        [SerializeValue]
        protected AssignableValue<float[]> BuildingMaxHealths { get; set; }
        [SerializeValue]
        protected AssignableValue<int[]> BuildingCosts { get; set; }

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