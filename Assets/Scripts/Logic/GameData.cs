using System.Linq;
using Shibari;

namespace VillageKeeper.Model
{
    public class GameData : BindableData
    {
        public PrimaryValue<int> CurrentHelpTip { get; private set; }
        public PrimaryValue<int> HelpTipsCount { get; private set; }

        public PrimaryValue<int> TotalFood { get; private set; }

        public PrimaryValue<int> CurrentBreadToGoldMultiplier { get; private set; }
        public PrimaryValue<int> NextBreadToGoldMultiplier { get; private set; }
        public PrimaryValue<int> RoundFinishedBonusGold { get; private set; }
        public PrimaryValue<int> CastleUpgradeCost { get; private set; }

        public PrimaryValue<float> ClampedMonsterHealth { get; private set; }
        public PrimaryValue<float> ClampedArrowForce { get; private set; }

        public PrimaryValue<bool> IsArrowForceOverThreshold { get; private set; }

        public void Init()
        {
            CalculateEconomy();

            Data.Saved.VillageLevel.OnValueChanged += CalculateEconomy;
            Core.Instance.FSM.SubscribeToEnter(FSM.States.RoundFinished, CalculateEconomy);
            ClampedArrowForce.OnValueChanged += () => IsArrowForceOverThreshold.Set(ClampedArrowForce >= Data.Balance.ArrowForceThreshold);
        }

        public void CalculateEconomy()
        {
            int villageLevel = Data.Saved.VillageLevel;
            SerializableBuilding[] buildings = Data.Saved.Buildings;
            int farms = buildings?.Count(c => c.Type == BuildingTypes.Farm) ?? 0;
            int windmills = buildings?.Count(c => c.Type == BuildingTypes.Windmill) ?? 0;

            CurrentBreadToGoldMultiplier.Set(Data.Balance.GetBreadToGoldMultiplier(villageLevel));
            if (villageLevel < Data.Balance.MaxVillageLevel)
                NextBreadToGoldMultiplier.Set(Data.Balance.GetBreadToGoldMultiplier(villageLevel + 1));
            RoundFinishedBonusGold.Set(Data.Balance.CalculateRoundFinishedBonusGold(villageLevel, farms, windmills));
            CastleUpgradeCost.Set(Data.Balance.GetCastleUpgradeCost(villageLevel));

            TotalFood.Set((Data.Balance.CalculateFarmsFood(farms) + Data.Balance.CalculateWindmillsFood(windmills, farms)));
        }
    }
}