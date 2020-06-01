using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class UnityAnalyticsController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (!Analytics.playerOptedOut)
        {
            Analytics.initializeOnStartup = true;
        }
        else
        {
            Analytics.initializeOnStartup = false;
        }
    }

    /*
     * IAP / Subscription analytics calls
    * */
    public static void SendVisitSubscribeScreenEvent()
    {
        //Debug.Log("Send subscribe screen visit analytics events");
        AnalyticsEvent.ScreenVisit("Visit_Subscribe_Screen");
    }

    /*
    * Achievement ranks analytics calls 
    * */
    public static void SendAchievedFirstRankEvent()
    {
        AnalyticsEvent.AchievementUnlocked("Achieved_Rank_1");
    }
    public static void SendAchievedSecondRankEvent()
    {
        AnalyticsEvent.AchievementUnlocked("Achieved_Rank_2");
    }
    public static void SendAchievedThirdRankEvent()
    {
        AnalyticsEvent.AchievementUnlocked("Achieved_Rank_3");
    }
    public static void SendAchievedFourthRankEvent()
    {
        AnalyticsEvent.AchievementUnlocked("Achieved_Rank_4");
    }
    public static void SendAchievedFifthRankEvent()
    {
        AnalyticsEvent.AchievementUnlocked("Achieved_Rank_5");
    }

    /*
     * Reward ad analytics calls
     * */
    public static void SendStartWatchingRewardedAdEvent()
    {
        AnalyticsEvent.AdStart(true, AdvertisingNetwork.UnityAds, "Start_Watching_Rewarded_Ad");
    }
    public static void SendCompleteWatchingRewardedAdEvent()
    {
        AnalyticsEvent.AdComplete(true, AdvertisingNetwork.UnityAds, "Complete_Watching_Rewarded_Ad");
    }
    public static void SendSkipWatchingRewardedAdEvent()
    {
        AnalyticsEvent.AdSkip(true, AdvertisingNetwork.UnityAds, "Skip_Watching_Rewarded_Ad");
    }
}
