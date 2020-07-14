using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

public class AdmobController : MonoBehaviour
{
    private RewardedAd rewardedAd;  

    public void Start()
    {
        this.rewardedAd = new RewardedAd(GetAdUnitID());

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    private string GetAdUnitID()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
                adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
                adUnitId = "unexpected_platform";
#endif
        return adUnitId;
    }
}
