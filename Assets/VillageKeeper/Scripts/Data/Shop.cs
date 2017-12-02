using Soomla.Store;

namespace VillageKeeper
{
    public class Shop
    {
        public void OnItemPurchased(PurchasableVirtualItem pvi, string payload)
        {
            if (pvi.ItemId == EconomyAssets.THOUSAND_COINS.ItemId)
                CoreScript.Instance.CommonData.Gold.Set(CoreScript.Instance.CommonData.Gold.Get() + 1000);
            if (pvi.ItemId == EconomyAssets.TEN_THOUSAND_COINS.ItemId)
                CoreScript.Instance.CommonData.Gold.Set(CoreScript.Instance.CommonData.Gold.Get() + 10000);
            CoreScript.Instance.CommonData.HasPremium.Set(true);
        }

        public void Init()
        {
            StoreEvents.OnItemPurchased += OnItemPurchased;
        }
    }
}
