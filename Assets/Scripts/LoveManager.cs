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

    private int maxLocStrings = 69;

    public static bool unlockedForToday = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (unlockedForToday == false && CheckIfCanUnlock())
        {
            string locKey = "LOVE_" + PlayerPrefs.GetInt(PLAYER_PREF_NAME).ToString();
            loveText.SetLocalizationKey(locKey);
            if (PlayerPrefs.GetInt(PLAYER_PREF_NAME) > maxLocStrings)
            {
                PlayerPrefs.SetInt(PLAYER_PREF_NAME, 0);
            }
            else
            {
                int newIndex = PlayerPrefs.GetInt(PLAYER_PREF_NAME) + 1;

                PlayerPrefs.SetInt(PLAYER_PREF_NAME, newIndex);

                if (newIndex > PlayerPrefs.GetInt(PLAYER_PREF_NAME_MAX_UNLOCKED))
                {
                    PlayerPrefs.SetInt(PLAYER_PREF_NAME_MAX_UNLOCKED, newIndex);
                }
            }
            unlockedForToday = true;
        }
    }

    private bool CheckIfCanUnlock()
    {
        DateTime currentDateTime = System.DateTime.Now;
        int currentDayOfYear = currentDateTime.DayOfYear;
        int currentYear = currentDateTime.Year;

        int storedDayOfYear = PlayerPrefs.GetInt("Day");
        int storedYear = PlayerPrefs.GetInt("Year");

        if (currentDayOfYear > storedDayOfYear && currentYear >= storedYear)
        {
            unlockedForToday = false;
            return true;
        }
        else
        {
            return false;
        }
    }
}
