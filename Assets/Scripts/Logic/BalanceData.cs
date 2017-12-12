using System;
using Shibari;
using VillageKeeper.Data;

namespace VillageKeeper.Data
{
    public class BalanceData : BindableData
    {
        public SerializableField<float> ArrowForceThreshold { get; private set; }// = 0.8f;
        public SerializableField<int> MonsterBonusGold { get; private set; }// = 20;
        public SerializableField<int> MaxVillageLevel { get; private set; }// = 2;
        public SerializableField<int> FoodPerFarm { get; private set; }// = 1;
        public SerializableField<int> FoodPerWindMillMultiplier { get; private set; }// = 1;

        public SerializableField<IntArray> CastleUpgradeCosts { get; private set; } // [600, 6000]

        public SerializableField<FloatArray> BuildingMaxHealths { get; private set; } //[2, 3, 6, 3, 4, 6]
        public SerializableField<IntArray> BuildingCosts { get; private set; } //       [2, 2, 25, 10, 50, 100]

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