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

    private static string ALLOW_ADS_PREF_NAME = "AllowAds"; // don't change this in production

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

    // Returns true if user has opted-in to ads
    public static bool GetAllowAds()
    {
        if (!PlayerPrefs.HasKey(ALLOW_ADS_PREF_NAME))
        {
            return false;
        }

        if (PlayerPrefs.GetString(ALLOW_ADS_PREF_NAME) == "true")
        {
            return true;
        }

        return false;
    }

    public static void AllowAds()
    {
        PlayerPrefs.SetString(ALLOW_ADS_PREF_NAME, "true");
    }

    public static void DisallowAds()
    {
        PlayerPrefs.SetString(ALLOW_ADS_PREF_NAME, "false");
    }

}
