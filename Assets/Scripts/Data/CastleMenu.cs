using Shibari;

namespace VillageKeeper.Model
{
    public class CastleMenu : BindableData
    {
        [ShowInEditor]
        public CalculatedValue<int> NextBreadToGoldMultiplier { get; } = new CalculatedValue<int>(
           () => Core.Data.Balance.GetBreadToGoldMultiplier(Core.Data.Saved.VillageLevel + 1),
           Core.Data.Saved.VillageLevel);

        [ShowInEditor]
        public CalculatedValue<int> CurrentBreadToGoldMultiplier { get; } = new CalculatedValue<int>(
            () => Core.Data.Balance.GetBreadToGoldMultiplier(Core.Data.Saved.VillageLevel),
            Core.Data.Saved.VillageLevel);

        [ShowInEditor]
        public CalculatedValue<bool> IsUpgradeCastleButtonInteractable { get; } = new CalculatedValue<bool>(
            () => Core.Data.Saved.Gold.Get() >= Core.Data.Balance.GetCastleUpgradeCost(Core.Data.Saved.VillageLevel.Get()),
            Core.Data.Saved.Gold,
            Core.Data.Saved.VillageLevel);

        [ShowInEditor]
        public CalculatedValue<bool> IsCastleUpgradeWindowVisible { get; } = new CalculatedValue<bool>(
            () => Core.Data.Saved.VillageLevel < Core.Data.Balance.MaxVillageLevel,
            Core.Data.Saved.VillageLevel,
            Core.Data.Balance.MaxVillageLevel);

        [ShowInEditor]
        public CalculatedValue<int> CastleUpgradeCost { get; }

        public CastleMenu()
        {
            CastleUpgradeCost = new CalculatedValue<int>(() => Core.Data.Balance.GetCastleUpgradeCost(Core.Data.Saved.VillageLevel), Core.Data.Saved.VillageLevel);
        }
        
        [ShowInEditor]
        protected void UpgradeCastle()
        {
            if (Core.Data.Saved.Gold.Get() >= Core.Data.Balance.GetCastleUpgradeCost(Core.Data.Saved.VillageLevel.Get()))
            {
                Core.Data.Saved.Gold.Set(Core.Data.Saved.Gold.Get() - Core.Data.Balance.GetCastleUpgradeCost(Core.Data.Saved.VillageLevel.Get()));
                Core.Data.Saved.VillageLevel.Set(Core.Data.Saved.VillageLevel.Get() + 1);
            }
        }
    }
}