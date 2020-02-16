using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoveManager : MonoBehaviour
{

    public LocalizationText loveText;

    private const string PLAYER_PREF_NAME = "LoveNumber"; // don't change in production
    private const string PLAYER_PREF_NAME_MAX_UNLOCKED = "LoveNumberMaxUnlocked"; // don't change in production

    private int maxLocStrings = 69; // number of LOVE_X key + value pairs in loc csv; i.e. values go from LOVE_0 to LOVE_68
    private static bool unlockedThisSession = false;

    void OnEnable()
    {
        string locKey = "LOVE_" + PlayerPrefs.GetInt(PLAYER_PREF_NAME).ToString();

        // only update once per session
        if (unlockedThisSession) {
            //Debug.Log("Already unlocked love value for this session");
            loveText.SetLocalizationKey(locKey);
            return;
        }

        // only update once per day
        if (!CurrencyManager.IsNewDay(true, "Day_Love", "Day_Year")) // make sure all calls to IsNewDay pass in different string player pref names
        {
            //Debug.Log("Already unlocked love value for today");
            loveText.SetLocalizationKey(locKey);
            return;
        }

        int newIndex = 0;

        // enforce max limit on number of null strings to avoid null refs; maxLocStrings should be same as number of key-value pairs in localization dictionary
        if (PlayerPrefs.GetInt(PLAYER_PREF_NAME) >= maxLocStrings)
        {
            PlayerPrefs.SetInt(PLAYER_PREF_NAME, newIndex);
        }

        // if there is still a valid increment to be made
        else
        { 
            // we want to start off the first day with a zero index and increment it every day after
            if (PlayerPrefs.GetString("FirstRun").Equals("False"))
            {
                //Debug.Log("First run is false, increment index");
                newIndex = PlayerPrefs.GetInt(PLAYER_PREF_NAME) + 1;
                PlayerPrefs.SetInt(PLAYER_PREF_NAME_MAX_UNLOCKED, newIndex);
            }

            if (newIndex > PlayerPrefs.GetInt(PLAYER_PREF_NAME_MAX_UNLOCKED))
            {
                //Debug.Log("Exceeded number of max love loc strings, resetting index to 0");    
                newIndex = 0;      
            }
        }
        PlayerPrefs.SetInt(PLAYER_PREF_NAME, newIndex);
        locKey = "LOVE_" + newIndex.ToString();
        loveText.SetLocalizationKey(locKey);
        unlockedThisSession = true;
    }
}
