using GoogleMobileAds.Api;
using GoogleMobileAds.Placement;
using UnityEngine;


public class GoogleMobileAdsController : MonoBehaviour
{

    static InterstitialAdGameObject interstitialAd;
    private GameObject[] taggedObjects; // used to control enabling/disabling of UI so it doesn't interfere with ad; this only works because interstitial ad is always shown on top of main ui instead of other panels at this point

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
        if (!UnityIAPController.IsGold())
        {
            interstitialAd.ShowIfLoaded();
        }
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
