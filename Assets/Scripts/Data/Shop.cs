using Soomla.Store;

namespace VillageKeeper
{
    public class Shop
    {
        public void OnItemPurchased(PurchasableVirtualItem pvi, string payload)
        {
            if (pvi.ItemId == EconomyAssets.THOUSAND_COINS.ItemId)
                CoreScript.Instance.SavedData.Gold.Set(CoreScript.Instance.SavedData.Gold.Get() + 1000);
            if (pvi.ItemId == EconomyAssets.TEN_THOUSAND_COINS.ItemId)
                CoreScript.Instance.SavedData.Gold.Set(CoreScript.Instance.SavedData.Gold.Get() + 10000);
            CoreScript.Instance.SavedData.HasPremium.Set(true);
            CoreScript.Instance.SavedData.SaveData();
        }

        public void Init()
        {
            StoreEvents.OnItemPurchased += OnItemPurchased;
        }
    }
}
