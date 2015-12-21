using UnityEngine;
using System.Collections;
using Soomla.Store;
namespace AssemblyCSharp {
	public class EconomyAssets : IStoreAssets {
		public int GetVersion () {
			return 0;
		}
		public VirtualCurrency[] GetCurrencies () {
			return new VirtualCurrency[]{};
		}
		public VirtualCurrencyPack[] GetCurrencyPacks () {
			return new VirtualCurrencyPack[]{};
		}
		public VirtualGood[] GetGoods () {
			return new VirtualGood[] {THOUSAND_COINS, TEN_THOUSAND_COINS};
		}
		public VirtualCategory[] GetCategories () {
			return new VirtualCategory[] {};
		}
		
		public static VirtualGood THOUSAND_COINS = new SingleUseVG(
			"Thousand coins",
			"Thousand coins",
			"thousand_coins",
			new PurchaseWithMarket ("thousand_coins", 1)
		);
		public static VirtualGood TEN_THOUSAND_COINS = new SingleUseVG(
			"Ten thousand coins",
			"Ten thousand coins",
			"ten_thousand_coins",
			new PurchaseWithMarket("ten_thousand_coins", 5)
		);
	}
}
