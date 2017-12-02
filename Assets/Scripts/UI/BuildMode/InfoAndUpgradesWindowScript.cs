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
            upgradeButton.interactable = CoreScript.Instance.CommonData.Gold.Get() >= Balance.Balance.GetCastleUpgradeCost(CoreScript.Instance.CommonData.VillageLevel.Get());
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
            int villageLevel = CoreScript.Instance.CommonData.VillageLevel.Get();
            castleUpgradeWindow.SetActive(villageLevel < Balance.Balance.MaxVillageLevel);
            SetUpgradeButton();
        }

        void Init()
        {
            offscreen = GetComponent<OffScreenMenuScript>() as OffScreenMenuScript;
            CoreScript.Instance.CommonData.Gold.OnValueChanged += () => SetUpgradeButton();
            CoreScript.Instance.CommonData.VillageLevel.OnValueChanged += () => SetValues();
            upgradeButton.onClick.AddListener(() =>
            {
                if (CoreScript.Instance.CommonData.Gold.Get() >= Balance.Balance.GetCastleUpgradeCost(CoreScript.Instance.CommonData.VillageLevel.Get()))
                {
                    CoreScript.Instance.CommonData.Gold.Set(CoreScript.Instance.CommonData.Gold.Get() - Balance.Balance.GetCastleUpgradeCost(CoreScript.Instance.CommonData.VillageLevel.Get()));
                    CoreScript.Instance.CommonData.VillageLevel.Set(CoreScript.Instance.CommonData.VillageLevel.Get() + 1);
                    CoreScript.Instance.AudioManager.PlayClick();
                }
            });
        }
    }
}