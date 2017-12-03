using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.Game;

namespace VillageKeeper.UI
{
    public class InfoAndUpgradesWindowScript : MonoBehaviour
    {
        public GameObject castleUpgradeWindow;
        public Button upgradeButton;
        private OffScreenMenuScript offscreen;

        void SetUpgradeButton()
        {
            upgradeButton.interactable = CoreScript.Instance.SavedData.Gold.Get() >= Balance.Balance.GetCastleUpgradeCost(CoreScript.Instance.SavedData.VillageLevel.Get());
        }

        public void Show()
        {
            offscreen.Show();
        }

        public void Hide()
        {
            offscreen.Hide();
        }

        public void SetValues()
        {
            int villageLevel = CoreScript.Instance.SavedData.VillageLevel.Get();
            castleUpgradeWindow.SetActive(villageLevel < Balance.Balance.MaxVillageLevel);
            SetUpgradeButton();
        }

        void Init()
        {
            offscreen = GetComponent<OffScreenMenuScript>() as OffScreenMenuScript;
            CoreScript.Instance.SavedData.Gold.OnValueChanged += () => SetUpgradeButton();
            CoreScript.Instance.SavedData.VillageLevel.OnValueChanged += () => SetValues();
            upgradeButton.onClick.AddListener(() =>
            {
                if (CoreScript.Instance.SavedData.Gold.Get() >= Balance.Balance.GetCastleUpgradeCost(CoreScript.Instance.SavedData.VillageLevel.Get()))
                {
                    CoreScript.Instance.SavedData.Gold.Set(CoreScript.Instance.SavedData.Gold.Get() - Balance.Balance.GetCastleUpgradeCost(CoreScript.Instance.SavedData.VillageLevel.Get()));
                    CoreScript.Instance.SavedData.VillageLevel.Set(CoreScript.Instance.SavedData.VillageLevel.Get() + 1);
                    CoreScript.Instance.AudioManager.PlayClick();
                }
            });
        }
    }
}