using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Soomla.Store;

namespace VillageKeeper.UI
{
    public class ShopScript : MonoBehaviour
    {
        OffScreenMenuScript offScreenMenu;
        public Button thounsandButton;
        public Button tenThousandButton;

        void Start()
        {
            offScreenMenu = GetComponent<OffScreenMenuScript>() as OffScreenMenuScript;
            StartCoroutine(InitCoroutine());
        }

        public void Show()
        {
            offScreenMenu.Show();
        }

        public void Hide()
        {
            offScreenMenu.Hide();
        }

        IEnumerator InitCoroutine()
        {
            yield return null;
            thounsandButton.onClick.AddListener(() =>
            {
                SoomlaStore.BuyMarketItem(EconomyAssets.THOUSAND_COINS.ItemId, "thousand");
                CoreScript.Instance.Audio.PlayClick();
            });

            tenThousandButton.onClick.AddListener(() =>
            {
                SoomlaStore.BuyMarketItem(EconomyAssets.TEN_THOUSAND_COINS.ItemId, "ten thousand");
                CoreScript.Instance.Audio.PlayClick();
            });
        }
    }
}