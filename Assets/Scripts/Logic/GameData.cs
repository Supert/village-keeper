using System.Linq;
using VillageKeeper.Data;
using Shibari;

namespace VillageKeeper.Balance
{
    public class GameData : BindableData
    {
        public BindableField<int> CurrentHelpTip { get; private set; }
        public BindableField<int> HelpTipsCount { get; private set; }

        public BindableField<int> TotalFood { get; private set; }

        public BindableField<int> CurrentBreadToGoldMultiplier { get; private set; }
        public BindableField<int> NextBreadToGoldMultiplier { get; private set; }
        public BindableField<int> RoundFinishedBonusGold { get; private set; }
        public BindableField<int> CastleUpgradeCost { get; private set; }

        public BindableField<float> ClampedMonsterHealth { get; private set; }
        public BindableField<float> ClampedArrowForce { get; private set; }

        public BindableField<bool> IsArrowForceOverThreshold { get; private set; }

        public override void Init(string prefix)
        {
            CalculateEconomy();

            Core.Instance.SavedData.VillageLevel.OnValueChanged += CalculateEconomy;
            Core.Instance.FSM.SubscribeToEnter(FSM.States.RoundFinished, CalculateEconomy);
            ClampedArrowForce.OnValueChanged += () => IsArrowForceOverThreshold.Set(ClampedArrowForce.Get() >= Core.Instance.Balance.ArrowForceThreshold.Get());
        }

        public void CalculateEconomy()
        {
            int villageLevel = Core.Instance.SavedData.VillageLevel.Get();
            var buildings = Core.Instance.SavedData.Buildings.Get();
            int farms = buildings.list.Count(c => c.Type == BuildingTypes.Farm);
            int windmills = buildings.list.Count(c => c.Type == BuildingTypes.Windmill);

            CurrentBreadToGoldMultiplier.Set(Core.Instance.Balance.GetBreadToGoldMultiplier(villageLevel));
            if (villageLevel < Core.Instance.Balance.MaxVillageLevel.Get())
                NextBreadToGoldMultiplier.Set(Core.Instance.Balance.GetBreadToGoldMultiplier(villageLevel + 1));
            RoundFinishedBonusGold.Set(Core.Instance.Balance.CalculateRoundFinishedBonusGold(villageLevel, farms, windmills));
            CastleUpgradeCost.Set(Core.Instance.Balance.GetCastleUpgradeCost(villageLevel));

            TotalFood.Set((Core.Instance.Balance.CalculateFarmsFood(farms) + Core.Instance.Balance.CalculateWindmillsFood(windmills, farms)));
        }
    }
}