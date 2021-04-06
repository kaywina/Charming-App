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
     * In-App Purchase / Subscription analytics calls (see SendSubscribeVisitEventOnEnable.cs on Panel_Subscribe scene object)
    * */
    public static void SendVisitSubscribeScreenEvent()
    {
        //Debug.Log("Send subscribe screen visit analytics events");
        AnalyticsEvent.ScreenVisit("Visit_Subscribe_Screen");
    }

    /*
    * Achievement ranks analytics calls (see RankManager.cs)
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
     * Reward ad analytics calls (see UnityRewardedAdsButton.cs)
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

    /*
     * User interaction analytics calls
     * */
    public static void SendEvent(string eventName)
    {
        switch (eventName)
        {
            case "FirstRun":
                AnalyticsEvent.FirstInteraction(eventName); // send event on first running Charming App
                //Debug.Log("Send FirstRun analytics event");
                break;
            default:
                Debug.Log("Event name not recognized; do not send Unity Analytics event");
                break;
        }
    }

    public static void SendAttentionGameLevelCompletedEvent(int level)
    {
        string eventName = "Completed_Attention_Level_" + level.ToString();
        AnalyticsEvent.LevelComplete(eventName);
    }

    public static void SendShareAnalyticsEvent(bool includeImage)
    {
        if (includeImage)
        {
            AnalyticsEvent.SocialShare(ShareType.Image, SocialNetwork.None);
            //Debug.Log("Send share anlalytics event for image");
        }
        else
        {
            AnalyticsEvent.SocialShare(ShareType.TextOnly, SocialNetwork.None);
            //Debug.Log("Send share anlalytics event for text only");
        }
    }
}
