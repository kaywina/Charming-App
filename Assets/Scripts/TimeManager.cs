using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager
{
    private const string DAILY_SPIN_PREF_DAY_NAME = "Day";
    private const string DAILY_SPIN_PREF_YEAR_NAME = "Year";

    private const string DAILY_LOVE_PREF_DAY_NAME = "Day_Love";
    private const string DAILY_LOVE_PREF_YEAR_NAME = "Year_Love";

    private const string RANK_PREF_DAY_NAME = "Day_Rank";
    private const string RANK_PREF_YEAR_NAME = "Year_Rank";

    private const string REMEMBER_GAME_DAY_NAME = "Day_Remember_Game";
    private const string REMEMBER_GAME_YEAR_NAME = "Year_Remember_Game";

    private static int currentDayOfYear;
    private static int currentYear;

    public enum TimeType {  DailySpin, DailyLove, Rank, RememberGame };

    public static void SetPrefsForDailyLove()
    {
        PlayerPrefs.SetInt(DAILY_LOVE_PREF_DAY_NAME, currentDayOfYear);
        PlayerPrefs.SetInt(DAILY_LOVE_PREF_YEAR_NAME, currentYear);
        //Debug.Log("Set day and year for daily love");
    }

    public static void SetPrefsForDailySpin()
    {
        PlayerPrefs.SetInt(DAILY_SPIN_PREF_DAY_NAME, currentDayOfYear);
        PlayerPrefs.SetInt(DAILY_SPIN_PREF_YEAR_NAME, currentYear);
        //Debug.Log("Set day and year for daily spin");
    }

    public static void SetPrefsForRank()
    {
        PlayerPrefs.SetInt(RANK_PREF_DAY_NAME, currentDayOfYear);
        PlayerPrefs.SetInt(RANK_PREF_YEAR_NAME, currentYear);
        //Debug.Log("Set day and year for rank");
    }

    public static void SetPrefsForRememberGame()
    {
        PlayerPrefs.SetInt(REMEMBER_GAME_DAY_NAME, currentDayOfYear);
        PlayerPrefs.SetInt(REMEMBER_GAME_YEAR_NAME, currentYear);
    }

    public static bool IsNewDay(TimeType timeType)
    {
        DateTime currentDateTime = DateTime.Now;
        currentDayOfYear = currentDateTime.DayOfYear;
        currentYear = currentDateTime.Year;

        int storedDayOfYear = 0;
        int storedYear = 0;

        switch (timeType) {
            case TimeType.DailySpin:
                storedDayOfYear = PlayerPrefs.GetInt(DAILY_SPIN_PREF_DAY_NAME);
                storedYear = PlayerPrefs.GetInt(DAILY_SPIN_PREF_YEAR_NAME);
                //Debug.Log("Get stored day and year for daily spin");
                break;
            case TimeType.DailyLove:
                storedDayOfYear = PlayerPrefs.GetInt(DAILY_LOVE_PREF_DAY_NAME);
                storedYear = PlayerPrefs.GetInt(DAILY_LOVE_PREF_YEAR_NAME);
                //Debug.Log("Get stored day and year for daily love");
                break;
            case TimeType.Rank:
                storedDayOfYear = PlayerPrefs.GetInt(RANK_PREF_DAY_NAME);
                storedYear = PlayerPrefs.GetInt(RANK_PREF_YEAR_NAME);
                //Debug.Log("Get stored day and year for rank");
                break;
            case TimeType.RememberGame:
                storedDayOfYear = PlayerPrefs.GetInt(REMEMBER_GAME_DAY_NAME);
                storedYear = PlayerPrefs.GetInt(REMEMBER_GAME_YEAR_NAME);
                //Debug.Log("Get stored day and year for rememeber game");
                break;
            default:
                Debug.Log("Reached default case in IsNewDay, this should not happen");
                break;
        }
 
        if (currentDayOfYear > storedDayOfYear && currentYear >= storedYear)
        {
            //Debug.Log("It's a new day!");
            return true;
        }

        //Debug.Log("It is not a new day!");
        return false;
    }
}
