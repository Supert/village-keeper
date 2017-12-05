using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    public class ShopScript : MonoBehaviour
    {
        public Button thounsandButton;
        public Button tenThousandButton;

        void Start()
        {
            thounsandButton.onClick.AddListener(() =>
            {
                //SoomlaStore.BuyMarketItem(EconomyAssets.THOUSAND_COINS.ItemId, "thousand");
                Core.Instance.AudioManager.PlayClick();
            });

            tenThousandButton.onClick.AddListener(() =>
            {
                //SoomlaStore.BuyMarketItem(EconomyAssets.TEN_THOUSAND_COINS.ItemId, "ten thousand");
                Core.Instance.AudioManager.PlayClick();
            });
        }
    }
}