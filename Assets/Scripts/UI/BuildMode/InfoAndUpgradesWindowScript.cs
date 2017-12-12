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
            upgradeButton.interactable = Core.Instance.SavedData.Gold.Get() >= Core.Instance.Balance.GetCastleUpgradeCost(Core.Instance.SavedData.VillageLevel.Get());
        }

        public void Show()
        {
            int villageLevel = Core.Instance.SavedData.VillageLevel.Get();
            castleUpgradeWindow.SetActive(villageLevel < Core.Instance.Balance.MaxVillageLevel.Get());
            SetUpgradeButton();
        }

        void Start()
        {
            Core.Instance.SavedData.Gold.OnValueChanged += () => SetUpgradeButton();
            Core.Instance.SavedData.VillageLevel.OnValueChanged += () => Show();
            upgradeButton.onClick.AddListener(() =>
            {
                if (Core.Instance.SavedData.Gold.Get() >= Core.Instance.Balance.GetCastleUpgradeCost(Core.Instance.SavedData.VillageLevel.Get()))
                {
                    Core.Instance.SavedData.Gold.Set(Core.Instance.SavedData.Gold.Get() - Core.Instance.Balance.GetCastleUpgradeCost(Core.Instance.SavedData.VillageLevel.Get()));
                    Core.Instance.SavedData.VillageLevel.Set(Core.Instance.SavedData.VillageLevel.Get() + 1);
                    Core.Instance.AudioManager.PlayClick();
                }
            });
            Core.Instance.FSM.SubscribeToEnter(FSM.States.Build, Show);
        }
    }
}