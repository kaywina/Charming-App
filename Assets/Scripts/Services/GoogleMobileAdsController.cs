using GoogleMobileAds.Api;
using UnityEngine;


public class GoogleMobileAdsController : MonoBehaviour
{

    public void Start()
    {
        // Initialize the Mobile Ads SDK.
        MobileAds.Initialize((initStatus) =>
        {
            // SDK initialization is complete
            Debug.Log("Google Mobile Ads initialized");
        });
    }
}
