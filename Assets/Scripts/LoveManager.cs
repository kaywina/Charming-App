using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoveManager : MonoBehaviour
{

    public LocalizationText loveText;
    public LocalizationText toughLoveText;

    private const string PLAYER_PREF_NAME = "LoveNumber"; // don't change in production
    private const string PLAYER_PREF_NAME_MAX_UNLOCKED = "LoveNumberMaxUnlocked"; // don't change in production

    private const string SECOND_PLAYER_PREF_NAME = "ToughLoveNumber"; // don't change in production
    private const string SECOND_PLAYER_PREF_NAME_MAX_UNLOCKED = "ToughLoveNumberMaxUnlocked"; // don't change in production

    private int maxLoveLocIndex = 72; // number of LOVE_X key + value pairs in loc csv; i.e. values go from LOVE_0 to LOVE_68
    private int maxToughLoveLocIndex = 72; // number of TOUGH_LOVE_X key + value pairs in loc csv; i.e. values go from TOUGH_LOVE_0 to TOUGH_LOVE_68

    private static bool unlockedThisSession = false;
    private static bool secondUnlockedThisSession = false;

    // indexing for easy love feature
    private int originalIndex = 0;
    private int tempIndex = 0;

    // indexing for tough love feature
    private int secondOriginalIndex = 0;
    private int secondTempIndex = 0;

    public void Initialize()
    {
        if (TimeManager.IsNewDay(TimeManager.TimeType.DailyLove))
        {
            originalIndex = PlayerPrefs.GetInt(PLAYER_PREF_NAME); // new day case
            secondOriginalIndex = PlayerPrefs.GetInt(SECOND_PLAYER_PREF_NAME);
        }
        else
        {
            originalIndex = PlayerPrefs.GetInt(PLAYER_PREF_NAME) - 1; // not a new day; player pref was already incremented so less one to make indexing work
            secondOriginalIndex = PlayerPrefs.GetInt(SECOND_PLAYER_PREF_NAME) - 1;
        }

        InitializeLove();
        InitializeToughLove();
        SetPrefs();
    }

    public void SetPrefs()
    {
        //Debug.Log("Set prefs for daily love");
        TimeManager.SetPrefsForDailyLove();
    }

    private void InitializeLove()
    {
        int loveIndex = originalIndex; // original index ensures that only one value is unlocked per day
        tempIndex = loveIndex; // temp index is used for arrow button/swipe functionality to read previous unlocked messages

        if (loveIndex < 0) { tempIndex = 0; } // force index to zero if something has gone wrong and index is less than zero

        string locKey = "LOVE_" + loveIndex.ToString();

        //Debug.Log("Starting loveIndex for this session is " + loveIndex);
        //Debug.Log("Max unlocked easy love at start of this session is " + PlayerPrefs.GetInt(PLAYER_PREF_NAME_MAX_UNLOCKED));

        // only update once per session
        if (unlockedThisSession)
        {
            //Debug.Log("Already unlocked love value for this session");
            loveText.SetLocalizationKey(locKey);
            return;
        }

        // only update once per day
        if (!TimeManager.IsNewDay(TimeManager.TimeType.DailyLove)) // make sure all calls to IsNewDay pass in different string player pref names
        {
            //Debug.Log("Already unlocked love value for today");
            loveText.SetLocalizationKey(locKey);
            return;
        }

        //Debug.Log("Unlock next love sayings");
        // display the correct love statement
        locKey = "LOVE_" + loveIndex.ToString();
        loveText.SetLocalizationKey(locKey);

        // increment love index for next session
        loveIndex++;
        if (loveIndex > PlayerPrefs.GetInt(PLAYER_PREF_NAME_MAX_UNLOCKED))
        {
            PlayerPrefs.SetInt(PLAYER_PREF_NAME_MAX_UNLOCKED, loveIndex); // do this before check max loc index so max unlocked does not get reset to zero
        }

        // enforce max limit on number of null strings to avoid null refs; maxLocStrings should be same as number of key-value pairs in localization dictionary
        if (loveIndex > maxLoveLocIndex)
        {
            loveIndex = 0;
            Debug.Log("Exceeded number of max love loc strings, resetting index to 0");
        }

        // set the player pref to be used next session
        PlayerPrefs.SetInt(PLAYER_PREF_NAME, loveIndex);
        unlockedThisSession = true;
        
        //Debug.Log("Ending loveIndex for this session is " + loveIndex);
        //Debug.Log("Max easy love unlocked at end of this session is " + PlayerPrefs.GetInt(PLAYER_PREF_NAME_MAX_UNLOCKED));
    }

    private void InitializeToughLove()
    {
        int secondLoveIndex = secondOriginalIndex; // original index ensures that only one value is unlocked per day
        secondTempIndex = secondLoveIndex; // temp index is used for arrow button/swipe functionality to read previous unlocked messages

        if (secondLoveIndex < 0) { secondTempIndex = 0; } // force index to zero if something has gone wrong and index is less than zero

        string locKey = "TOUGH_LOVE_" + secondLoveIndex.ToString();

        //Debug.Log("Starting secondLoveIndex for this session is " + secondLoveIndex);
        //Debug.Log("Max unlocked tough love at start of this session is " + PlayerPrefs.GetInt(SECOND_PLAYER_PREF_NAME_MAX_UNLOCKED));

        // only update once per session
        if (secondUnlockedThisSession)
        {
            //Debug.Log("Already unlocked love value for this session");
            toughLoveText.SetLocalizationKey(locKey);
            return;
        }

        // only update once per day
        if (!TimeManager.IsNewDay(TimeManager.TimeType.DailyLove)) // make sure all calls to IsNewDay pass in different string player pref names
        {
            //Debug.Log("Already unlocked love value for today");
            toughLoveText.SetLocalizationKey(locKey);
            return;
        }

        // display the correct love statement
        locKey = "TOUGH_LOVE_" + secondLoveIndex.ToString();
        toughLoveText.SetLocalizationKey(locKey);

        // increment love index for next session
        secondLoveIndex++;
        if (secondLoveIndex > PlayerPrefs.GetInt(SECOND_PLAYER_PREF_NAME_MAX_UNLOCKED))
        {
            PlayerPrefs.SetInt(SECOND_PLAYER_PREF_NAME_MAX_UNLOCKED, secondLoveIndex); // do this before check max loc index so max unlocked does not get reset to zero
        }

        // enforce max limit on number of null strings to avoid null refs; maxLocStrings should be same as number of key-value pairs in localization dictionary
        if (secondLoveIndex > maxToughLoveLocIndex)
        {
            secondLoveIndex = 0;
            Debug.Log("Exceeded number of max tough love loc strings, resetting index to 0");
        }

        // set the player pref to be used next session
        PlayerPrefs.SetInt(SECOND_PLAYER_PREF_NAME, secondLoveIndex);
        secondUnlockedThisSession = true;

        TimeManager.SetPrefsForDailyLove();
        //Debug.Log("Ending secondLoveIndex for this session is " + secondLoveIndex);
        //Debug.Log("Max tough love unlocked at end of this session is " + PlayerPrefs.GetInt(SECOND_PLAYER_PREF_NAME_MAX_UNLOCKED));
    }

    public void PreviousLove()
    {
        //Debug.Log("Go to previous love");

        tempIndex--;
        //Debug.Log("temp index is now " + tempIndex);

        if (tempIndex < 0)
        {
            tempIndex = PlayerPrefs.GetInt(PLAYER_PREF_NAME_MAX_UNLOCKED) - 1; // this is less one, because max has been incremented in OnEnable
            //Debug.Log("temp index less than zero, reset to max unlocked index");
        }

        string tempLocKey = "LOVE_" + tempIndex.ToString();
        loveText.SetLocalizationKey(tempLocKey);
    }

    public void NextLove()
    {
        //Debug.Log("Go to next love");

        tempIndex++;
        //Debug.Log("temp index is now " + tempIndex);

        if (tempIndex >= PlayerPrefs.GetInt(PLAYER_PREF_NAME_MAX_UNLOCKED))
        {
            //Debug.Log("temp index greater than max unlocked, reset to zero");
            tempIndex = 0;
        }

        string tempLocKey = "LOVE_" + tempIndex.ToString();
        loveText.SetLocalizationKey(tempLocKey);
    }

    public void PreviousToughLove()
    {
        //Debug.Log("Go to previous tough love");

        secondTempIndex--;

        if (secondTempIndex < 0)
        {
            secondTempIndex = PlayerPrefs.GetInt(SECOND_PLAYER_PREF_NAME_MAX_UNLOCKED) - 1; // this is less one, because max has been incremented in OnEnable
            //Debug.Log("temp index less than zero, reset to max unlocked index");
        }

        string tempLocKey = "TOUGH_LOVE_" + secondTempIndex.ToString();
        toughLoveText.SetLocalizationKey(tempLocKey);
    }

    public void NextToughLove()
    {
        //Debug.Log("Go to next tough love");

        secondTempIndex++;

        if (secondTempIndex >= PlayerPrefs.GetInt(SECOND_PLAYER_PREF_NAME_MAX_UNLOCKED))
        {
            //Debug.Log("temp index greater than max unlocked, reset to zero");
            secondTempIndex = 0;
        }

        string tempLocKey = "TOUGH_LOVE_" + secondTempIndex.ToString();
        toughLoveText.SetLocalizationKey(tempLocKey);
    }
}
