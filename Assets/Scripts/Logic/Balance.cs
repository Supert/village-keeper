using VillageKeeper.Game;
using VillageKeeper.Data;
using System;

namespace VillageKeeper.Balance
{
    public static class Balance
    {
        public static int MonsterBonusGold { get; } = 20;
        public static int MaxVillageLevel { get; } = 2;
        public static int FoodPerFarm { get; } = 1;
        public static int FoodPerWindMillMultiplier { get; } = 1;

        public static int GetCastleUpgradeCost(int currentLevel)
        {
            if (currentLevel == 0)
                return 600;
            if (currentLevel == 1)
                return 6000;
            return 0;
        }

        public static int GetBreadToGoldMultiplier(int villageLevel)
        {
            return villageLevel + 1;
        }

        public static int CalculateFarmsFood(int farms)
        {
            return farms * FoodPerFarm;
        }

        public static int CalculateWindmillsFood(int windmills, int farms)
        {
            return windmills * farms * FoodPerWindMillMultiplier;
        }

        public static int CalculateRoundFinishedBonusGold(int villageLevel, int farms, int windmills)
        {
            int result = MonsterBonusGold;
            int food = CalculateFarmsFood(farms) + CalculateWindmillsFood(windmills, farms);
            result += food * GetBreadToGoldMultiplier(villageLevel);
            return result;
        }

        public static float GetBuildingMaxHealth(BuildingTypes type)
        {
            switch (type)
            {
                case BuildingTypes.Farm:
                    return 2f;
                case BuildingTypes.WallStone:
                    return 6f;
                case BuildingTypes.WallWooden:
                    return 3f;
                case BuildingTypes.WatchtowerStone:
                    return 4f;
                case BuildingTypes.WatchtowerWooden:
                    return 3f;
                case BuildingTypes.Windmill:
                    return 6f;
                default:
                    return 0;
            }
        }

        public static int GetBuildingGoldCost(BuildingTypes type)
        {
            switch (type)
            {
                case BuildingTypes.Farm:
                    return 2;
                case BuildingTypes.WallStone:
                    return 25;
                case BuildingTypes.WallWooden:
                    return 2;
                case BuildingTypes.WatchtowerStone:
                    return 50;
                case BuildingTypes.WatchtowerWooden:
                    return 10;
                case BuildingTypes.Windmill:
                    return 100;
                default:
                    return 0;
            }
        }
    }
}