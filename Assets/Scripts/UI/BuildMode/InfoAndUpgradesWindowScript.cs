using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.Game;
using VillageKeeper.Model;

namespace VillageKeeper.UI
{
    public class InfoAndUpgradesWindowScript : MonoBehaviour
    {
        public GameObject castleUpgradeWindow;
        public Button upgradeButton;

        void SetUpgradeButton()
        {
            upgradeButton.interactable = Core.Data.Saved.Gold.Get() >= Core.Data.Balance.GetCastleUpgradeCost(Core.Data.Saved.VillageLevel.Get());
        }

        public void Show()
        {
            int villageLevel = Core.Data.Saved.VillageLevel.Get();
            castleUpgradeWindow.SetActive(villageLevel < Core.Data.Balance.MaxVillageLevel.Get());
            SetUpgradeButton();
        }

        void Start()
        {
            Core.Data.Saved.Gold.OnValueChanged += () => SetUpgradeButton();
            Core.Data.Saved.VillageLevel.OnValueChanged += () => Show();
            upgradeButton.onClick.AddListener(() =>
            {
                if (Core.Data.Saved.Gold.Get() >= Core.Data.Balance.GetCastleUpgradeCost(Core.Data.Saved.VillageLevel.Get()))
                {
                    Core.Data.Saved.Gold.Set(Core.Data.Saved.Gold.Get() - Core.Data.Balance.GetCastleUpgradeCost(Core.Data.Saved.VillageLevel.Get()));
                    Core.Data.Saved.VillageLevel.Set(Core.Data.Saved.VillageLevel.Get() + 1);
                    Core.Instance.AudioManager.PlayClick();
                }
            });
            Core.Instance.FSM.SubscribeToEnter(FSM.States.Build, Show);
        }
    }
}