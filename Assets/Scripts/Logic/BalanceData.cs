using System.Linq;
using VillageKeeper.Data;

namespace VillageKeeper.Balance
{
    public class BalanceData : BindedData
    {
        public BalanceField<string> MonsterBonusGold { get; private set; }
        public BalanceField<string> MaxVillageLevel { get; private set; }

        //public BalanceField<string> FarmsFood { get; private set; }
        //public BalanceField<string> WindmillBonusFood { get; private set; }

        public BalanceField<string> TotalFood { get; private set; }

        public BalanceField<string> CurrentBreadToGoldMultiplier { get; private set; }
        public BalanceField<string> NextBreadToGoldMultiplier { get; private set; }
        public BalanceField<string> RoundFinishedBonusGold { get; private set; }
        public BalanceField<string> CastleUpgradeCost { get; private set; }

        public override void InitDataFields(string prefix)
        {
            base.InitDataFields(prefix);

            CalculateEconomy();

            MonsterBonusGold.Set(Balance.MonsterBonusGold.ToString());
            MaxVillageLevel.Set(Balance.MonsterBonusGold.ToString());

            CoreScript.Instance.CommonData.VillageLevel.OnValueChanged += CalculateEconomy;
            CoreScript.Instance.FSM.SubscribeToEnter(FSM.States.RoundFinished, CalculateEconomy);
        }

        public void CalculateEconomy()
        {
            int villageLevel = CoreScript.Instance.CommonData.VillageLevel.Get();
            var buildings = CoreScript.Instance.CommonData.Buildings.Get();
            int farms = buildings.list.Count(c => c.Type == BuildingTypes.Farm);
            int windmills = buildings.list.Count(c => c.Type == BuildingTypes.Windmill);

            CurrentBreadToGoldMultiplier.Set(Balance.GetBreadToGoldMultiplier(villageLevel).ToString());
            if (villageLevel < Balance.MaxVillageLevel)
                NextBreadToGoldMultiplier.Set(Balance.GetBreadToGoldMultiplier(villageLevel + 1).ToString());
            RoundFinishedBonusGold.Set(Balance.CalculateRoundFinishedBonusGold(villageLevel, farms, windmills).ToString());
            CastleUpgradeCost.Set(Balance.GetCastleUpgradeCost(villageLevel).ToString());

            TotalFood.Set((Balance.CalculateFarmsFood(farms) + Balance.CalculateWindmillsFood(windmills, farms)).ToString());
        }

        public BalanceData(string prefix)
        {
            InitDataFields(prefix);
        }
    }
}