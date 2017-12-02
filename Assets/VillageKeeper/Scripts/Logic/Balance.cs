using System.Linq;
using VillageKeeper.Game;
using VillageKeeper.Data;
using System;

namespace VillageKeeper
{
    public class Balance
    {
        public int GetMonsterBonusGold()
        {
            return 20;
        }

        public int GetFarmsFood()
        {
            return CoreScript.Instance.BuildingsArea.buildings.Where(x => x.Type == BuildingTypes.Farm).Count();
        }

        public int GetWindmillBonusFood()
        {
            int result = 0;
            foreach (WindmillScript w in CoreScript.Instance.BuildingsArea.buildings.Where(x => x.Type == BuildingTypes.Windmill))
                result += w.WindmillBonus;
            return result;
        }

        internal float GetBuildingMaxHealth(BuildingTypes type)
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

        internal int GetBuildingBoldCost(BuildingTypes type)
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

        public int GetBreadToGoldMultiplier()
        {
            return GetBreadToGoldMultiplier(CoreScript.Instance.CommonData.VillageLevel.Get());
        }

        public int GetBreadToGoldMultiplier(int villageLevel)
        {
            return villageLevel + 1;
        }

        public int GetRoundFinishedBonusGold()
        {
            int result = GetMonsterBonusGold();
            int food = GetFarmsFood() + GetWindmillBonusFood();
            result += food * GetBreadToGoldMultiplier();
            return result;
        }

        public int GetCastleUpgradeCost()
        {
            if (CoreScript.Instance.CommonData.VillageLevel.Get() == 0)
                return 600;
            if (CoreScript.Instance.CommonData.VillageLevel.Get() == 1)
                return 6000;
            return 0;
        }
    }
}