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
            upgradeButton.interactable = Data.Saved.Gold.Get() >= Data.Balance.GetCastleUpgradeCost(Data.Saved.VillageLevel.Get());
        }

        public void Show()
        {
            int villageLevel = Data.Saved.VillageLevel.Get();
            castleUpgradeWindow.SetActive(villageLevel < Data.Balance.MaxVillageLevel.Get());
            SetUpgradeButton();
        }

        void Start()
        {
            Data.Saved.Gold.OnValueChanged += () => SetUpgradeButton();
            Data.Saved.VillageLevel.OnValueChanged += () => Show();
            upgradeButton.onClick.AddListener(() =>
            {
                if (Data.Saved.Gold.Get() >= Data.Balance.GetCastleUpgradeCost(Data.Saved.VillageLevel.Get()))
                {
                    Data.Saved.Gold.Set(Data.Saved.Gold.Get() - Data.Balance.GetCastleUpgradeCost(Data.Saved.VillageLevel.Get()));
                    Data.Saved.VillageLevel.Set(Data.Saved.VillageLevel.Get() + 1);
                    Core.Instance.AudioManager.PlayClick();
                }
            });
            Core.Instance.FSM.SubscribeToEnter(FSM.States.Build, Show);
        }
    }
}