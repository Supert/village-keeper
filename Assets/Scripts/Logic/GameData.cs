using System.Linq;
using VillageKeeper.Data;

namespace VillageKeeper.Balance
{
    public class GameData : BindedData
    {
        public BindableField<int> MonsterBonusGold { get; private set; }
        public BindableField<int> MaxVillageLevel { get; private set; }

        public BindableField<int> TotalFood { get; private set; }

        public BindableField<int> CurrentBreadToGoldMultiplier { get; private set; }
        public BindableField<int> NextBreadToGoldMultiplier { get; private set; }
        public BindableField<int> RoundFinishedBonusGold { get; private set; }
        public BindableField<int> CastleUpgradeCost { get; private set; }

        public BindableField<float> ClampedMonsterHealth { get; private set; }
        public BindableField<float> ClampedArrowForce { get; private set; }

        public BindableField<float> Wind { get; private set; }

        public override void Register(string prefix)
        {
            base.Register(prefix);

            CalculateEconomy();

            MonsterBonusGold.Set(Balance.MonsterBonusGold);
            MaxVillageLevel.Set(Balance.MaxVillageLevel);

            CoreScript.Instance.SavedData.VillageLevel.OnValueChanged += CalculateEconomy;
            CoreScript.Instance.FSM.SubscribeToEnter(FSM.States.RoundFinished, CalculateEconomy);
        }

        public void CalculateEconomy()
        {
            int villageLevel = CoreScript.Instance.SavedData.VillageLevel.Get();
            var buildings = CoreScript.Instance.SavedData.Buildings.Get();
            int farms = buildings.list.Count(c => c.Type == BuildingTypes.Farm);
            int windmills = buildings.list.Count(c => c.Type == BuildingTypes.Windmill);

            CurrentBreadToGoldMultiplier.Set(Balance.GetBreadToGoldMultiplier(villageLevel));
            if (villageLevel < Balance.MaxVillageLevel)
                NextBreadToGoldMultiplier.Set(Balance.GetBreadToGoldMultiplier(villageLevel + 1));
            RoundFinishedBonusGold.Set(Balance.CalculateRoundFinishedBonusGold(villageLevel, farms, windmills));
            CastleUpgradeCost.Set(Balance.GetCastleUpgradeCost(villageLevel));

            TotalFood.Set((Balance.CalculateFarmsFood(farms) + Balance.CalculateWindmillsFood(windmills, farms)));
        }

        public GameData(string prefix)
        {
            Register(prefix);
        }
    }
}