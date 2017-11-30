using System.Linq;
using VillageKeeper.Game;
using VillageKeeper.Data;

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

        public int GetBreadToGoldMultiplier()
        {
            return GetBreadToGoldMultiplier(CoreScript.Instance.Data.VillageLevel.Get());
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
            if (CoreScript.Instance.Data.VillageLevel.Get() == 0)
                return 600;
            if (CoreScript.Instance.Data.VillageLevel.Get() == 1)
                return 6000;
            return 0;
        }
    }
}