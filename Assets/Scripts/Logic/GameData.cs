using System.Linq;
using Shibari;

namespace VillageKeeper.Data
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

        public void Init()
        {
            CalculateEconomy();

            Core.Instance.Data.Saved.VillageLevel.OnValueChanged += CalculateEconomy;
            Core.Instance.FSM.SubscribeToEnter(FSM.States.RoundFinished, CalculateEconomy);
            ClampedArrowForce.OnValueChanged += () => IsArrowForceOverThreshold.Set(ClampedArrowForce.Get() >= Core.Instance.Data.Balance.ArrowForceThreshold.Get());
        }

        public void CalculateEconomy()
        {
            int villageLevel = Core.Instance.Data.Saved.VillageLevel.Get();
            var buildings = Core.Instance.Data.Saved.Buildings.Get();
            int farms = buildings?.Count(c => c.Type == BuildingTypes.Farm) ?? 0;
            int windmills = buildings?.Count(c => c.Type == BuildingTypes.Windmill) ?? 0;

            CurrentBreadToGoldMultiplier.Set(Core.Instance.Data.Balance.GetBreadToGoldMultiplier(villageLevel));
            if (villageLevel < Core.Instance.Data.Balance.MaxVillageLevel.Get())
                NextBreadToGoldMultiplier.Set(Core.Instance.Data.Balance.GetBreadToGoldMultiplier(villageLevel + 1));
            RoundFinishedBonusGold.Set(Core.Instance.Data.Balance.CalculateRoundFinishedBonusGold(villageLevel, farms, windmills));
            CastleUpgradeCost.Set(Core.Instance.Data.Balance.GetCastleUpgradeCost(villageLevel));

            TotalFood.Set((Core.Instance.Data.Balance.CalculateFarmsFood(farms) + Core.Instance.Data.Balance.CalculateWindmillsFood(windmills, farms)));
        }
    }
}