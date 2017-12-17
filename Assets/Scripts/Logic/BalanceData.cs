using System;
using Shibari;
using VillageKeeper.Data;

namespace VillageKeeper.Data
{
    public class BalanceData : BindableData
    {
        public SerializableField<float> ArrowForceThreshold { get; private set; }
        public SerializableField<int> MonsterBonusGold { get; private set; }
        public SerializableField<int> MaxVillageLevel { get; private set; }
        public SerializableField<int> FoodPerFarm { get; private set; }
        public SerializableField<int> FoodPerWindMillMultiplier { get; private set; }

        public SerializableField<IntArray> CastleUpgradeCosts { get; private set; }

        public SerializableField<FloatArray> BuildingMaxHealths { get; private set; }
        public SerializableField<IntArray> BuildingCosts { get; private set; }

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