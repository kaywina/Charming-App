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

    private int maxLocIndex = 68; // number of LOVE_X key + value pairs in loc csv; i.e. values go from LOVE_0 to LOVE_68
    private static bool unlockedThisSession = false;

    private int originalIndex = 0;
    private int tempIndex = 0;

    private void Awake()
    {
        // hack to make indexing work properly
        if (TimeManager.IsNewDay(TimeManager.TimeType.DailyLove)) {
            originalIndex = PlayerPrefs.GetInt(PLAYER_PREF_NAME);
        }
        else
        {
            originalIndex = PlayerPrefs.GetInt(PLAYER_PREF_NAME) - 1;
        }

        Debug.Log("Assign original index in LoveManager");
    }
    void OnEnable()
    {
        int loveIndex = originalIndex; // original index ensures that only one value is unlocked per day
        tempIndex = loveIndex; // temp index is used for arrow button/swipe functionality to read previous unlocked messages

        string locKey = "LOVE_" + loveIndex.ToString();

        Debug.Log("Starting loveIndex for this session is " + loveIndex);
        Debug.Log("Max unlocked so far is " + PlayerPrefs.GetInt(PLAYER_PREF_NAME_MAX_UNLOCKED));

        // only update once per session
        if (unlockedThisSession) {
            Debug.Log("Already unlocked love value for this session");
            loveText.SetLocalizationKey(locKey);
            return;
        }

        // only update once per day
        if (!TimeManager.IsNewDay(TimeManager.TimeType.DailyLove)) // make sure all calls to IsNewDay pass in different string player pref names
        {
            Debug.Log("Already unlocked love value for today");
            loveText.SetLocalizationKey(locKey);
            return;
        }

        // enforce max limit on number of null strings to avoid null refs; maxLocStrings should be same as number of key-value pairs in localization dictionary
        if (loveIndex > maxLocIndex)
        {
            loveIndex = 0;
            PlayerPrefs.SetInt(PLAYER_PREF_NAME, loveIndex);
            Debug.Log("Exceeded number of max love loc strings, resetting index to 0");
        }
        else
        {
            PlayerPrefs.SetInt(PLAYER_PREF_NAME_MAX_UNLOCKED, loveIndex);
        }

        // display the correct love statement
        locKey = "LOVE_" + loveIndex.ToString();
        loveText.SetLocalizationKey(locKey);

        // increment love index for next session
        loveIndex++;
        PlayerPrefs.SetInt(PLAYER_PREF_NAME, loveIndex);
        Debug.Log("Incrementing daily love index to " + loveIndex + " for next session");
        TimeManager.SetPrefsForDailyLove();
        unlockedThisSession = true;
    }

    public void PreviousLove()
    {
        Debug.Log("Go to previous love");

        tempIndex--;
        Debug.Log("temp index is now " + tempIndex);

        if (tempIndex < 0)
        {
            tempIndex = PlayerPrefs.GetInt(PLAYER_PREF_NAME_MAX_UNLOCKED);
            Debug.Log("temp index less than zero, reset to max unlocked index");
        }

        string tempLocKey = "LOVE_" + tempIndex.ToString();
        loveText.SetLocalizationKey(tempLocKey);
    }

    public void NextLove()
    {
        Debug.Log("Go to next love");

        tempIndex++;
        Debug.Log("temp index is now " + tempIndex);

        if (tempIndex > PlayerPrefs.GetInt(PLAYER_PREF_NAME_MAX_UNLOCKED))
        {
            Debug.Log("temp index greater than max unlocked, reset to zero");
            tempIndex = 0;
        }

        string tempLocKey = "LOVE_" + tempIndex.ToString();
        loveText.SetLocalizationKey(tempLocKey);
    }
}
