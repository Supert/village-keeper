namespace VillageKeeper
{
    public class Shop
    {
        //public void OnItemPurchased(PurchasableVirtualItem pvi, string payload)
        //{
        //    if (pvi.ItemId == EconomyAssets.THOUSAND_COINS.ItemId)
        //        Core.Instance.SavedData.Gold.Set(Core.Instance.SavedData.Gold.Get() + 1000);
        //    if (pvi.ItemId == EconomyAssets.TEN_THOUSAND_COINS.ItemId)
        //        Core.Instance.SavedData.Gold.Set(Core.Instance.SavedData.Gold.Get() + 10000);
        //    Core.Instance.SavedData.HasPremium.Set(true);
        //    Core.Instance.SavedData.SaveData();
        //}

        public void Init()
        {
            //StoreEvents.OnItemPurchased += OnItemPurchased;
        }
    }
}
