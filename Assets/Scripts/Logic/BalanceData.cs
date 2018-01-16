using Shibari;
using System.Linq;

namespace VillageKeeper.Data
{
    public class BalanceData : BindableData
    {
        [SerializeValue]
        public BindableField<float> ArrowForceThreshold { get; private set; }
        [SerializeValue]
        public BindableField<int> MonsterBonusGold { get; private set; }
        [SerializeValue]
        public BindableField<int> MaxVillageLevel { get; private set; }

        [SerializeValue]
        protected BindableField<int> MonsterMaxHealthPossible { get; private set; }
        [SerializeValue]
        protected BindableField<int> MonsterMinHealthPossible { get; private set; }
        [SerializeValue]
        protected BindableField<float> MonsterBuildingCostModifier { get; private set; }
        [SerializeValue]
        protected BindableField<float> MonsterPowerPointsBuildingsHealthModifier { get; private set; }
        [SerializeValue]
        protected BindableField<float> MonsterPowerPointsVillageLevelModifier { get; private set; }

        [SerializeValue]
        protected BindableField<int> FoodPerFarm { get; set; }
        [SerializeValue]
        protected BindableField<int> FoodPerWindMillMultiplier { get; set; }
        [SerializeValue]
        protected BindableField<int[]> CastleUpgradeCosts { get; set; }
        [SerializeValue]
        protected BindableField<float[]> BuildingMaxHealths { get; set; }
        [SerializeValue]
        protected BindableField<int[]> BuildingCosts { get; set; }

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