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

        interstitialAd = MobileAds.Instance
            .GetAd<InterstitialAdGameObject>("Interstitial Ad");

        bannerAd = MobileAds.Instance.GetAd<BannerAdGameObject>("Banner Ad");

        // Initialize the Mobile Ads SDK.
        MobileAds.Initialize((initStatus) =>
        {
            // SDK initialization is complete
            Debug.Log("Google Mobile Ads initialized");
        });

        interstitialAd.LoadAd();
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

        bannerAd.Hide();
        //Debug.Log("Hide banner ad");
    }

    public static void ShowInterstitialAd()
    {
        if (UnityIAPController.IsGold())
        {
            return; // don't show ads for gold users
        }

        interstitialAd.ShowIfLoaded();
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
        interstitialAd.LoadAd(); // this allows us to re-use the interstital object (otherwise no ad will play next time ShowInterstitialAd is called)
        //Debug.Log("On Ad Closed");
    }
}
