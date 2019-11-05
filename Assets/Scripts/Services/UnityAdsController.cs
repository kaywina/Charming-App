using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsController : MonoBehaviour
{

#if UNITY_IOS
    private static string gameId = "3033752";
#elif UNITY_ANDROID
    private static string gameId = "3033753";
#endif

    public static string GetGameId()
    {
#if UNITY_IOS || UNITY_ANDROID
        return gameId;
#else
        return null;
#endif
    }

    private void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(UnityAdsController.GetGameId());
        }
    }

}
