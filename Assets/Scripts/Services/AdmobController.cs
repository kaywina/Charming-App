using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

public class AdmobController : MonoBehaviour
{
    private static RewardedAd rewardedAd;

    public delegate void RewardedAdWatchedAction();
    public static event RewardedAdWatchedAction OnRewardedAdWatched;

    public void Start()
    {
        string id = GetAdUnitID();
        CreateAndLoadRewardedAd(id);
    }

    private string GetAdUnitID()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-8051833160607351/9561028858"; // PRODUCTION ID!!! note this is different from the app id in settings
#elif UNITY_IPHONE
                adUnitId = "";
#else
                adUnitId = "unexpected_platform";
#endif
        return adUnitId;
    }

    public void CreateAndLoadRewardedAd(string adUnitId)
    {
        rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        UnityAnalyticsController.SendStartWatchingRewardedAdEvent();
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        this.CreateAndLoadRewardedAd(GetAdUnitID()); // load a new rewarded ad after this one watched
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        if (sender == null || args == null)
        {
            Debug.Log("Dummy reward");
            OnRewardedAdWatched();
            return;
        }

        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);

        OnRewardedAdWatched();
        UnityAnalyticsController.SendCompleteWatchingRewardedAdEvent();
    }

    public static void TryToShowRewardedAd()
    {
        if (IsRewardedAdReady())
        {
            rewardedAd.Show();
        }
    }

    public static bool IsRewardedAdReady()
    {
        return rewardedAd.IsLoaded();
    }
}
