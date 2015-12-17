using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
public class AdScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		BannerView bannerView = new BannerView ("ca-app-pub-4407695866243191/3291644660", AdSize.Banner, AdPosition.Bottom);
		AdRequest adRequest = new AdRequest.Builder ().Build ();
		bannerView.LoadAd (adRequest);
		CoreScript.Instance.GameStateChanged += (sender, e) => {
			switch (e.NewState) {
			case CoreScript.GameStates.InMenu:
				bannerView.Show ();
				break;
			default:
				bannerView.Hide ();
				break;
			}
		};
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
