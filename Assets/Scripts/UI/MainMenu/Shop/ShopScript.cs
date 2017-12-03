using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Soomla.Store;

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
                SoomlaStore.BuyMarketItem(EconomyAssets.THOUSAND_COINS.ItemId, "thousand");
                CoreScript.Instance.AudioManager.PlayClick();
            });

            tenThousandButton.onClick.AddListener(() =>
            {
                SoomlaStore.BuyMarketItem(EconomyAssets.TEN_THOUSAND_COINS.ItemId, "ten thousand");
                CoreScript.Instance.AudioManager.PlayClick();
            });
        }
    }
}