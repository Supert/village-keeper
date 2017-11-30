using UnityEngine;
using GoogleMobileAds.Api;

namespace VillageKeeper.UI
{
    public class AdScript : MonoBehaviour
    {
        void Start()
        {
            BannerView bannerView = new BannerView("ca-app-pub-4407695866243191/3291644660", AdSize.Banner, AdPosition.Bottom);
            AdRequest adRequest = new AdRequest.Builder().Build();
            bannerView.LoadAd(adRequest);

            CoreScript.Instance.Data.HasPremium.OnValueChanged += (b) =>
            {
                if (b)
                    bannerView.Hide();
            };

            //CoreScript.Instance.GameStateChanged += (sender, e) =>
            //{
            //    if (!CoreScript.Instance.Data.HasPremium)
            //    {
            //        switch (e.NewState)
            //        {
            //            case CoreScript.GameStates.InMenu:
            //                bannerView.Show();
            //                break;
            //            default:
            //                bannerView.Hide();
            //                break;
            //        }
            //    }
            //};
        }
    }
}