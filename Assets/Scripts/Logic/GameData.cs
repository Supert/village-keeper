using System.Linq;
using VillageKeeper.Data;

namespace VillageKeeper.Balance
{
    public class GameData : BindedData
    {
        public BindableField<int> CurrentHelpTip { get; private set; }

        public BindableField<int> TotalFood { get; private set; }

        public BindableField<int> CurrentBreadToGoldMultiplier { get; private set; }
        public BindableField<int> NextBreadToGoldMultiplier { get; private set; }
        public BindableField<int> RoundFinishedBonusGold { get; private set; }
        public BindableField<int> CastleUpgradeCost { get; private set; }

        public BindableField<float> ClampedMonsterHealth { get; private set; }
        public BindableField<float> ClampedArrowForce { get; private set; }

        public BindableField<bool> IsArrowForceOverThreshold { get; private set; }

        public override void Register(string prefix)
        {
            base.Register(prefix);

            CalculateEconomy();
            
            CoreScript.Instance.SavedData.VillageLevel.OnValueChanged += CalculateEconomy;
            CoreScript.Instance.FSM.SubscribeToEnter(FSM.States.RoundFinished, CalculateEconomy);
            ClampedArrowForce.OnValueChanged += () => IsArrowForceOverThreshold.Set(ClampedArrowForce.Get() >= BalanceData.ArrowForceThreshold);
        }

        public void CalculateEconomy()
        {
            int villageLevel = CoreScript.Instance.SavedData.VillageLevel.Get();
            var buildings = CoreScript.Instance.SavedData.Buildings.Get();
            int farms = buildings.list.Count(c => c.Type == BuildingTypes.Farm);
            int windmills = buildings.list.Count(c => c.Type == BuildingTypes.Windmill);

            CurrentBreadToGoldMultiplier.Set(BalanceData.GetBreadToGoldMultiplier(villageLevel));
            if (villageLevel < BalanceData.MaxVillageLevel)
                NextBreadToGoldMultiplier.Set(BalanceData.GetBreadToGoldMultiplier(villageLevel + 1));
            RoundFinishedBonusGold.Set(BalanceData.CalculateRoundFinishedBonusGold(villageLevel, farms, windmills));
            CastleUpgradeCost.Set(BalanceData.GetCastleUpgradeCost(villageLevel));

            TotalFood.Set((BalanceData.CalculateFarmsFood(farms) + BalanceData.CalculateWindmillsFood(windmills, farms)));
        }

        public GameData(string prefix)
        {
            Register(prefix);
        }
    }
}