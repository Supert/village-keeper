using UnityEngine;
using System.Collections;
using Soomla.Store;
namespace AssemblyCSharp {
	public class EconomyAssets : IStoreAssets {
		public int GetVersion () {
			return 0;
		}
		public VirtualCurrency[] GetCurrencies () {
			return new VirtualCurrency[]{COIN_CURRENCY};
		}
		public VirtualCurrencyPack[] GetCurrencyPacks () {
			return new VirtualCurrencyPack[]{THOUSAND_COIN_PACK, TEN_THOUSAND_COIN_PACK};
		}
		public VirtualGood[] GetGoods () {
			return new VirtualGood[] {};
		}
		public VirtualCategory[] GetCategories () {
			return new VirtualCategory[] {};
		}
		public static VirtualCurrency COIN_CURRENCY = new VirtualCurrency(
			"coin",                               // Name
			"coin currency",                      // Description
			"coin_currency_ID"                    // Item ID
			);
		
		/** Virtual Currency Packs **/
		
		public static VirtualCurrencyPack THOUSAND_COIN_PACK = new VirtualCurrencyPack(
			"1000 Сoins",                          // Name
			"1000 coin currency units",            // Description
			"coins_1000_ID",                       // Item ID
			1000,                                  // Number of currencies in the pack
			"coin_currency_ID" ,                   // ID of the currency associated with this pack
			new PurchaseWithMarket(               // Purchase type (with real money $)
		    	"coins_1000_PROD_ID",                   // Product ID
		        1                                  // Price (in real money $)
		    )
		);
		public static VirtualCurrencyPack TEN_THOUSAND_COIN_PACK = new VirtualCurrencyPack(
			"10000 Coins",
			"10000 coin currency units",
			"coins_10000_ID",
			10000,
			"coin_currency_ID",
			new PurchaseWithMarket(
				"coins_10000_PROD_ID",
				5
			)
		);
	}
}
