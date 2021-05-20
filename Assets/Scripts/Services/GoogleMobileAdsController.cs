using GoogleMobileAds.Api;
using GoogleMobileAds.Placement;
using UnityEngine;


public class GoogleMobileAdsController : MonoBehaviour
{

    static InterstitialAdGameObject interstitialAd;
    static BannerAdGameObject bannerAd;
    private GameObject[] taggedObjects; // used to control enabling/disabling of UI so it doesn't interfere with ad; this only works because interstitial ad is always shown on top of main ui instead of other panels at this point

    public void Start()
    {
        if (UnityIAPController.IsGold())
        {
            Debug.Log("Gold subscriber; do not initialize Google Mobile Ads");
            return; // no ads for gold users; don't even initialize
        }

        // Initialize the Mobile Ads SDK.
        MobileAds.Initialize((initStatus) =>
        {
            // SDK initialization is complete
            Debug.Log("Google Mobile Ads initialized");
        });

        interstitialAd = MobileAds.Instance
            .GetAd<InterstitialAdGameObject>("Interstitial Ad");

#if UNITY_IOS
        MobileAds.SetiOSAppPauseOnBackground(true);
#endif
        if (interstitialAd != null) { interstitialAd.LoadAd(); }

        bannerAd = MobileAds.Instance.GetAd<BannerAdGameObject>("Banner Ad");
        bannerAd.LoadAd();
        HideBannerAd(); // or else it shows up on app load    
    }

    public static void ShowBannerAd()
    {
        if (UnityIAPController.IsGold())
        {
            return; // don't show ads for gold users
        }

        bannerAd.Show();
        //Debug.Log("Show banner ad");

    }

    public static void HideBannerAd()
    {
        if (UnityIAPController.IsGold())
        {
            return; // no ads for gold users
        }

        if (bannerAd != null) { bannerAd.Hide(); }
        //Debug.Log("Hide banner ad");
    }

    public static void ShowInterstitialAd()
    {
        if (UnityIAPController.IsGold())
        {
            return; // don't show ads for gold users
        }

        if (interstitialAd != null) { interstitialAd.ShowIfLoaded(); }
    }

    public void OnInterstitialAdOpening()
    {
        taggedObjects = GameObject.FindGameObjectsWithTag("MainUI");
        for (int i = 0; i < taggedObjects.Length; i++)
        {
            taggedObjects[i].SetActive(false);
        }
        //Debug.Log("On Ad Opening");
    }

    public void OnInterstitialAdClosed()
    {
        for (int i = 0; i < taggedObjects.Length; i++)
        {
            taggedObjects[i].SetActive(true);
        }
        if (interstitialAd != null) { interstitialAd.LoadAd(); } // this allows us to re-use the interstital object (otherwise no ad will play next time ShowInterstitialAd is called)
        //Debug.Log("On Ad Closed");
    }
}
