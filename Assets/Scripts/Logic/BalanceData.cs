using Shibari;
using System.Linq;

namespace VillageKeeper.Data
{
    public class BalanceData : BindableData
    {
        public SerializableField<float> ArrowForceThreshold { get; private set; }
        public SerializableField<int> MonsterBonusGold { get; private set; }
        public SerializableField<int> MaxVillageLevel { get; private set; }
        
        protected SerializableField<int> MonsterMaxHealthPossible { get; private set; }
        protected SerializableField<int> MonsterMinHealthPossible { get; private set; }
        protected SerializableField<float> MonsterBuildingCostModifier { get; private set; }
        protected SerializableField<float> MonsterPowerPointsBuildingsHealthModifier { get; private set; }
        protected SerializableField<float> MonsterPowerPointsVillageLevelModifier { get; private set; }

        protected SerializableField<int> FoodPerFarm { get; set; }
        protected SerializableField<int> FoodPerWindMillMultiplier { get; set; }
        protected SerializableField<int[]> CastleUpgradeCosts { get; set; }
        protected SerializableField<float[]> BuildingMaxHealths { get; set; }
        protected SerializableField<int[]> BuildingCosts { get; set; }

        public float GetMonsterPowerPoints()
        {
            float buildingsHealth = Core.Instance.BuildingsArea.buildings.Sum(b => b.MaxHealth);
            return buildingsHealth * MonsterPowerPointsBuildingsHealthModifier.Get() * (1 + Core.Instance.Data.Saved.VillageLevel.Get() * MonsterPowerPointsVillageLevelModifier.Get());
        }

        public float GetMonsterMinHealth()
        {
            float buildingsCost = Core.Instance.BuildingsArea.buildings.Sum(b => b.GoldCost);
            return buildingsCost * MonsterBuildingCostModifier.Get() + MonsterMinHealthPossible.Get();
        }

        public float GetMonsterMaxHealth()
        {
            return MonsterMaxHealthPossible.Get();
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
            return farms * FoodPerFarm.Get();
        }

        public int CalculateWindmillsFood(int windmills, int farms)
        {
            return windmills * farms * FoodPerWindMillMultiplier.Get();
        }

        public int CalculateRoundFinishedBonusGold(int villageLevel, int farms, int windmills)
        {
            int result = MonsterBonusGold.Get();
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