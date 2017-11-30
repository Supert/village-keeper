using UnityEngine;
using System.Collections;
using Soomla.Store;
using System.Runtime.Serialization;
namespace VillageKeeper
{
    public class Shop
    {
        public void OnItemPurchased(PurchasableVirtualItem pvi, string payload)
        {
            if (pvi.ItemId == EconomyAssets.THOUSAND_COINS.ItemId)
                CoreScript.Instance.Data.Gold.Set(CoreScript.Instance.Data.Gold.Get() + 1000);
            if (pvi.ItemId == EconomyAssets.TEN_THOUSAND_COINS.ItemId)
                CoreScript.Instance.Data.Gold.Set(CoreScript.Instance.Data.Gold.Get() + 10000);
            CoreScript.Instance.Data.HasPremium.Set(true);
        }

        public void Init()
        {
            StoreEvents.OnItemPurchased += OnItemPurchased;
        }
    }
}
namespace VillageKeeper.Data
{
    public class Data : MonoBehaviour
    {
        public BuildingsDataField Buildings { get; } = new BuildingsDataField("Buildings", new SerializableBuildingsList());

        public IntDataField VillageLevel { get; } = new IntDataField("VillageLevel");
        public IntDataField Gold { get; } = new IntDataField("Gold");

        public BoolDataField HasPremium { get; } = new BoolDataField("HasPremium");

        public BoolDataField WasBuildTipShown { get; } = new BoolDataField("WasBuildTipShow");
        public BoolDataField WasBattleTipShown { get; } = new BoolDataField("WasBattleTipShown");


        public IntDataField MonstersDefeated { get; } = new IntDataField("MonstersDefeated");

        public BoolDataField IsSoundEffectsEnabled { get; } = new BoolDataField("IsSoundEffectsEnabled");
        public BoolDataField IsMusicEnabled { get; } = new BoolDataField("IsMusicEnabled");
    }
}