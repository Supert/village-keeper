using System.Linq;
using VillageKeeper.Data;

namespace VillageKeeper.Balance
{
    public class UiBalanceData : BindedData
    {
        public BindableField<string> MonsterBonusGold { get; private set; }
        public BindableField<string> MaxVillageLevel { get; private set; }

        public BindableField<string> TotalFood { get; private set; }

        public BindableField<string> CurrentBreadToGoldMultiplier { get; private set; }
        public BindableField<string> NextBreadToGoldMultiplier { get; private set; }
        public BindableField<string> RoundFinishedBonusGold { get; private set; }
        public BindableField<string> CastleUpgradeCost { get; private set; }

        public override void Register(string prefix)
        {
            base.Register(prefix);

            CalculateEconomy();

            MonsterBonusGold.Set(Balance.MonsterBonusGold.ToString());
            MaxVillageLevel.Set(Balance.MonsterBonusGold.ToString());

            CoreScript.Instance.SavedData.VillageLevel.OnValueChanged += CalculateEconomy;
            CoreScript.Instance.FSM.SubscribeToEnter(FSM.States.RoundFinished, CalculateEconomy);
        }

        public void CalculateEconomy()
        {
            int villageLevel = CoreScript.Instance.SavedData.VillageLevel.Get();
            var buildings = CoreScript.Instance.SavedData.Buildings.Get();
            int farms = buildings.list.Count(c => c.Type == BuildingTypes.Farm);
            int windmills = buildings.list.Count(c => c.Type == BuildingTypes.Windmill);

            CurrentBreadToGoldMultiplier.Set(Balance.GetBreadToGoldMultiplier(villageLevel).ToString());
            if (villageLevel < Balance.MaxVillageLevel)
                NextBreadToGoldMultiplier.Set(Balance.GetBreadToGoldMultiplier(villageLevel + 1).ToString());
            RoundFinishedBonusGold.Set(Balance.CalculateRoundFinishedBonusGold(villageLevel, farms, windmills).ToString());
            CastleUpgradeCost.Set(Balance.GetCastleUpgradeCost(villageLevel).ToString());

            TotalFood.Set((Balance.CalculateFarmsFood(farms) + Balance.CalculateWindmillsFood(windmills, farms)).ToString());
        }

        public UiBalanceData(string prefix)
        {
            Register(prefix);
        }
    }
}