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
            upgradeButton.interactable = CoreScript.Instance.SavedData.Gold.Get() >= Balance.Balance.GetCastleUpgradeCost(CoreScript.Instance.SavedData.VillageLevel.Get());
        }

        public void Show()
        {
            int villageLevel = CoreScript.Instance.SavedData.VillageLevel.Get();
            castleUpgradeWindow.SetActive(villageLevel < Balance.Balance.MaxVillageLevel);
            SetUpgradeButton();
        }

        void Start()
        {
            CoreScript.Instance.SavedData.Gold.OnValueChanged += () => SetUpgradeButton();
            CoreScript.Instance.SavedData.VillageLevel.OnValueChanged += () => Show();
            upgradeButton.onClick.AddListener(() =>
            {
                if (CoreScript.Instance.SavedData.Gold.Get() >= Balance.Balance.GetCastleUpgradeCost(CoreScript.Instance.SavedData.VillageLevel.Get()))
                {
                    CoreScript.Instance.SavedData.Gold.Set(CoreScript.Instance.SavedData.Gold.Get() - Balance.Balance.GetCastleUpgradeCost(CoreScript.Instance.SavedData.VillageLevel.Get()));
                    CoreScript.Instance.SavedData.VillageLevel.Set(CoreScript.Instance.SavedData.VillageLevel.Get() + 1);
                    CoreScript.Instance.AudioManager.PlayClick();
                }
            });
            CoreScript.Instance.FSM.SubscribeToEnter(FSM.States.Build, Show);
        }
    }
}