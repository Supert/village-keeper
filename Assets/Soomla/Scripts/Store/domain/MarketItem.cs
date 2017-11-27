/// Copyright (C) 2012-2014 Soomla Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///      http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.

using UnityEngine;
using System.Collections;

namespace Soomla.Store {

	/// <summary>
	/// This class represents an item in the market.
	/// <c>MarketItem</c> is only used for <c>PurchaseWithMarket</c> purchase type.
	/// </summary>
	public class MarketItem {

		/// <summary>
		/// The product id as defined in itunesconnect or Google Play
		/// </summary>
		public string ProductId;
		/// <summary>
		/// A default price for the item in case the fetching of information from the App Store or Google Play fails.
		/// </summary>
		public double Price;

		/** These variable will contain information about the item as fetched from the App Store or Google Play. **/
		public string MarketPriceAndCurrency;
		public string MarketTitle;
		public string MarketDescription;
		public string MarketCurrencyCode;
		public long MarketPriceMicros;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="productId">The id of the current item in the market.</param>
		/// <param name="price">The actual $$ cost of the current item in the market.</param>
		public MarketItem(string productId, double price){
            ProductId = productId;
            Price = price;
		}

		/// <summary>
		/// Constructor.
		/// Generates an instance of <c>MarketItem</c> from a <c>JSONObject<c>.
		/// </summary>
		/// <param name="jsonObject">A <c>JSONObject</c> representation of the wanted 
		/// <c>MarketItem</c>.</param>
		public MarketItem(JSONObject jsonObject) {
			string keyToLook = "";
#if UNITY_IOS && !UNITY_EDITOR
			keyToLook = StoreJSONConsts.MARKETITEM_IOS_ID;
#elif UNITY_ANDROID && !UNITY_EDITOR
			keyToLook = StoreJSONConsts.MARKETITEM_ANDROID_ID;
#endif
			if (!string.IsNullOrEmpty(keyToLook) && jsonObject.HasField(keyToLook)) {
				ProductId = jsonObject[keyToLook].str;
			} else {
				ProductId = jsonObject[StoreJSONConsts.MARKETITEM_PRODUCT_ID].str;
			}
			Price = jsonObject[StoreJSONConsts.MARKETITEM_PRICE].n;

			if (jsonObject[StoreJSONConsts.MARKETITEM_MARKETPRICE]) {
                MarketPriceAndCurrency = jsonObject[StoreJSONConsts.MARKETITEM_MARKETPRICE].str;
			} else {
                MarketPriceAndCurrency = "";
			}
			if (jsonObject[StoreJSONConsts.MARKETITEM_MARKETTITLE]) {
                MarketTitle = jsonObject[StoreJSONConsts.MARKETITEM_MARKETTITLE].str;
			} else {
                MarketTitle = "";
			}
			if (jsonObject[StoreJSONConsts.MARKETITEM_MARKETDESC]) {
                MarketDescription = jsonObject[StoreJSONConsts.MARKETITEM_MARKETDESC].str;
			} else {
                MarketDescription = "";
			}
			if (jsonObject[StoreJSONConsts.MARKETITEM_MARKETCURRENCYCODE]) {
                MarketCurrencyCode = jsonObject[StoreJSONConsts.MARKETITEM_MARKETCURRENCYCODE].str;
			} else {
                MarketCurrencyCode = "";
			}
			if (jsonObject[StoreJSONConsts.MARKETITEM_MARKETPRICEMICROS]) {
                MarketPriceMicros = System.Convert.ToInt64(jsonObject[StoreJSONConsts.MARKETITEM_MARKETPRICEMICROS].n);
			} else {
                MarketPriceMicros = 0;
			}
		}

		/// <summary>
		/// Converts the current <c>MarketItem</c> to a <c>JSONObject</c>.
		/// </summary>
		/// <returns>A <c>JSONObject</c> representation of the current 
		/// <c>MarketItem</c>.</returns>
		public JSONObject toJSONObject() {
			JSONObject obj = new JSONObject(JSONObject.Type.OBJECT);
			obj.AddField (Soomla.JSONConsts.SOOM_CLASSNAME, SoomlaUtils.GetClassName (this));
			obj.AddField(StoreJSONConsts.MARKETITEM_PRODUCT_ID, ProductId);
			obj.AddField(StoreJSONConsts.MARKETITEM_PRICE, (float)Price);

			obj.AddField(StoreJSONConsts.MARKETITEM_MARKETPRICE, MarketPriceAndCurrency);
			obj.AddField(StoreJSONConsts.MARKETITEM_MARKETTITLE, MarketTitle);
			obj.AddField(StoreJSONConsts.MARKETITEM_MARKETDESC, MarketDescription);
			obj.AddField(StoreJSONConsts.MARKETITEM_MARKETCURRENCYCODE, MarketCurrencyCode);
			obj.AddField(StoreJSONConsts.MARKETITEM_MARKETPRICEMICROS, (float)MarketPriceMicros);

			return obj;
		}

	}
}
