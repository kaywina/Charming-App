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

    private static int currentDayOfYear;
    private static int currentYear;

    public enum TimeType {  DailySpin, DailyLove };

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
