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
        Debug.Log("Send subscribe screen visit analytics events");
        AnalyticsEvent.ScreenVisit("Subscribe");
    }
}
