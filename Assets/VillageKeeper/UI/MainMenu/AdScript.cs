﻿using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
public class AdScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		BannerView bannerView = new BannerView ("ca-app-pub-4407695866243191/3291644660", AdSize.Banner, AdPosition.Bottom);
		AdRequest adRequest = new AdRequest.Builder ().Build ();
		bannerView.LoadAd (adRequest);
        CoreScript.Instance.Data.DataFieldChanged += (sender, e) =>
        {
            if (e.FieldChanged == DataScript.DataFields.HasPremium)
            {
                bannerView.Hide();
            }
        };
		CoreScript.Instance.GameStateChanged += (sender, e) =>
        {
            if (!CoreScript.Instance.Data.HasPremium)
            {
                switch (e.NewState)
                {
                    case CoreScript.GameStates.InMenu:
                        bannerView.Show();
                        break;
                    default:
                        bannerView.Hide();
                        break;
                }
            }
        };
	}
}