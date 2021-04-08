using GoogleMobileAds.Api;
using GoogleMobileAds.Placement;
using UnityEngine;


public class GoogleMobileAdsController : MonoBehaviour
{

    static InterstitialAdGameObject interstitialAd;

    public void Start()
    {
        interstitialAd = MobileAds.Instance
            .GetAd<InterstitialAdGameObject>("Interstitial Ad");

        // Initialize the Mobile Ads SDK.
        MobileAds.Initialize((initStatus) =>
        {
            // SDK initialization is complete
            Debug.Log("Google Mobile Ads initialized");
        });

        interstitialAd.LoadAd();
    }

    public static void ShowInterstitialAd()
    {
        interstitialAd.ShowIfLoaded();
    }
}
