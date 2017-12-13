using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.Game;

namespace VillageKeeper.UI
{
    public class InfoAndUpgradesWindowScript : MonoBehaviour
    {
        public GameObject castleUpgradeWindow;
        public Button upgradeButton;

        void SetUpgradeButton()
        {
            upgradeButton.interactable = Core.Instance.Data.Saved.Gold.Get() >= Core.Instance.Data.Balance.GetCastleUpgradeCost(Core.Instance.Data.Saved.VillageLevel.Get());
        }

        public void Show()
        {
            int villageLevel = Core.Instance.Data.Saved.VillageLevel.Get();
            castleUpgradeWindow.SetActive(villageLevel < Core.Instance.Data.Balance.MaxVillageLevel.Get());
            SetUpgradeButton();
        }

        void Start()
        {
            Core.Instance.Data.Saved.Gold.OnValueChanged += () => SetUpgradeButton();
            Core.Instance.Data.Saved.VillageLevel.OnValueChanged += () => Show();
            upgradeButton.onClick.AddListener(() =>
            {
                if (Core.Instance.Data.Saved.Gold.Get() >= Core.Instance.Data.Balance.GetCastleUpgradeCost(Core.Instance.Data.Saved.VillageLevel.Get()))
                {
                    Core.Instance.Data.Saved.Gold.Set(Core.Instance.Data.Saved.Gold.Get() - Core.Instance.Data.Balance.GetCastleUpgradeCost(Core.Instance.Data.Saved.VillageLevel.Get()));
                    Core.Instance.Data.Saved.VillageLevel.Set(Core.Instance.Data.Saved.VillageLevel.Get() + 1);
                    Core.Instance.AudioManager.PlayClick();
                }
            });
            Core.Instance.FSM.SubscribeToEnter(FSM.States.Build, Show);
        }
    }
}