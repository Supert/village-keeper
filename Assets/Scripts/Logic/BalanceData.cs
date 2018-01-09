using Shibari;

namespace VillageKeeper.Data
{
    public class BalanceData : BindableData
    {
        public SerializableField<float> ArrowForceThreshold { get; private set; }
        public SerializableField<int> MonsterBonusGold { get; private set; }
        public SerializableField<int> MaxVillageLevel { get; private set; }

        protected SerializableField<int> FoodPerFarm { get; set; }
        protected SerializableField<int> FoodPerWindMillMultiplier { get; set; }
        protected SerializableField<IntArray> CastleUpgradeCosts { get; set; }
        protected SerializableField<FloatArray> BuildingMaxHealths { get; set; }
        protected SerializableField<IntArray> BuildingCosts { get; set; }

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