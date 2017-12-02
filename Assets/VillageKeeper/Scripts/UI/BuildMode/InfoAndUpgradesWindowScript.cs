using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.Game;

namespace VillageKeeper.UI
{
    public class InfoAndUpgradesWindowScript : MonoBehaviour
    {
        public GameObject castleUpgradeWindow;
        public Sprite firstCastleUpgradeSprite;
        public Sprite secondCastleUpgradeSprite;
        public Image castleUpgradeIcon;
        public Text upgradePriceText;
        public Button upgradeButton;
        private OffScreenMenuScript offscreen;

        void SetUpgradeButton()
        {
            if (CoreScript.Instance.CommonData.Gold.Get() >= CoreScript.Instance.Balance.GetCastleUpgradeCost())
                upgradeButton.interactable = true;
            else
                upgradeButton.interactable = false;
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
            upgradePriceText.text = CoreScript.Instance.Balance.GetCastleUpgradeCost().ToString();
            switch (villageLevel)
            {
                case 0:
                    castleUpgradeWindow.SetActive(true);
                    castleUpgradeIcon.sprite = firstCastleUpgradeSprite;
                    upgradePriceText.text = CoreScript.Instance.Balance.GetCastleUpgradeCost().ToString();
                    break;
                case 1:
                    castleUpgradeWindow.SetActive(true);
                    castleUpgradeIcon.sprite = secondCastleUpgradeSprite;
                    break;
                case 2:
                    castleUpgradeWindow.SetActive(false);
                    break;
                default:
                    break;
            }
            SetUpgradeButton();
        }

        void Init()
        {
            offscreen = GetComponent<OffScreenMenuScript>() as OffScreenMenuScript;
            CoreScript.Instance.CommonData.Gold.OnValueChanged += () => SetUpgradeButton();
            CoreScript.Instance.CommonData.VillageLevel.OnValueChanged += () => SetValues();
            upgradeButton.onClick.AddListener(() =>
            {
                if (CoreScript.Instance.CommonData.Gold.Get() >= CoreScript.Instance.Balance.GetCastleUpgradeCost())
                {
                    CoreScript.Instance.CommonData.Gold.Set(CoreScript.Instance.CommonData.Gold.Get() - CoreScript.Instance.Balance.GetCastleUpgradeCost());
                    CoreScript.Instance.CommonData.VillageLevel.Set(CoreScript.Instance.CommonData.VillageLevel.Get() + 1);
                    CoreScript.Instance.AudioManager.PlayClick();
                }
            });
        }
    }
}