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

    public static void SendVisitSubscribeScreenEvent()
    {
        //Debug.Log("Send subscribe screen visit analytics events");
        AnalyticsEvent.ScreenVisit("Subscribe");
    }

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
}
