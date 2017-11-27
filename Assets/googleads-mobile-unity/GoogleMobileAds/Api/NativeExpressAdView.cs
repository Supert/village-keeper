// Copyright (C) 2016 Google, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Reflection;

using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
    public class NativeExpressAdView
    {
        private INativeExpressAdClient client;

        // Creates a NativeExpressAd and adds it to the view hierarchy.
        public NativeExpressAdView(string adUnitId, AdSize adSize, AdPosition position)
        {
            Type googleMobileAdsClientFactory = Type.GetType(
                "GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
            MethodInfo method = googleMobileAdsClientFactory.GetMethod(
                "BuildNativeExpressAdClient",
                BindingFlags.Static | BindingFlags.Public);
            client = (INativeExpressAdClient)method.Invoke(null, null);
            client.CreateNativeExpressAdView(adUnitId, adSize, position);

            ConfigureNativeExpressAdEvents();
        }

        // Creates a NativeExpressAd with a custom position.
        public NativeExpressAdView(string adUnitId, AdSize adSize, int x, int y)
        {
            Type googleMobileAdsClientFactory = Type.GetType(
                "GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
            MethodInfo method = googleMobileAdsClientFactory.GetMethod(
                "BuildNativeExpressAdClient",
                BindingFlags.Static | BindingFlags.Public);
            client = (INativeExpressAdClient)method.Invoke(null, null);
            client.CreateNativeExpressAdView(adUnitId, adSize, x, y);

            ConfigureNativeExpressAdEvents();
        }

        // These are the ad callback events that can be hooked into.
        public event EventHandler<EventArgs> OnAdLoaded;

        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        public event EventHandler<EventArgs> OnAdOpening;

        public event EventHandler<EventArgs> OnAdClosed;

        public event EventHandler<EventArgs> OnAdLeavingApplication;

        // Loads a native express ad.
        public void LoadAd(AdRequest request)
        {
            client.LoadAd(request);
        }

        // Hides the NativeExpressAdView from the screen.
        public void Hide()
        {
            client.HideNativeExpressAdView();
        }

        // Shows the NativeExpressAdView on the screen.
        public void Show()
        {
            client.ShowNativeExpressAdView();
        }

        // Destroys the NativeExpressAdView.
        public void Destroy()
        {
            client.DestroyNativeExpressAdView();
        }

        private void ConfigureNativeExpressAdEvents()
        {
            client.OnAdLoaded += (sender, args) =>
            {
                if(OnAdLoaded != null)
                {
                    OnAdLoaded(this, args);
                }
            };

            client.OnAdFailedToLoad += (sender, args) =>
            {
                if(OnAdFailedToLoad != null)
                {
                    OnAdFailedToLoad(this, args);
                }
            };

            client.OnAdOpening += (sender, args) =>
            {
                if(OnAdOpening != null)
                {
                    OnAdOpening(this, args);
                }
            };

            client.OnAdClosed += (sender, args) =>
            {
                if(OnAdClosed != null)
                {
                    OnAdClosed(this, args);
                }
            };

            client.OnAdLeavingApplication += (sender, args) =>
            {
                if(OnAdLeavingApplication != null)
                {
                    OnAdLeavingApplication(this, args);
                }
            };
        }

        // Returns the mediation adapter class name.
        public string MediationAdapterClassName()
        {
            return client.MediationAdapterClassName();
        }
    }
}
