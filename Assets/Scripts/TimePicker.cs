using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePicker : MonoBehaviour
{

    public NotificationManager notificationManager;

    public Text hourText;
    public Text minuteText;

    private int hour = 11; // 11:00 is default time
    private const int MAX_HOUR = 23; // set for a 24-hour day cycle

    private int minute = 00;
    private int minuteIncrement = 5; // each time the arrow is clicked minute increases/decreases by this amount
    private const int MAX_MINUTE = 59; // display can be up to 59 minutes


    private void OnEnable()
    {
        if (PlayerPrefs.HasKey(NotificationManager.PLAYERPREF_NAME_HOUR))
        {
            hour = PlayerPrefs.GetInt(NotificationManager.PLAYERPREF_NAME_HOUR);
        }
        else
        {
            //Debug.Log("Notification hour is not set; using default value");
        }

        if (PlayerPrefs.HasKey(NotificationManager.PLAYERPREF_NAME_MINUTE))
        {
            minute = PlayerPrefs.GetInt(NotificationManager.PLAYERPREF_NAME_MINUTE);
        }
        else
        {
            //Debug.Log("Notification minute is not set; using default value");
        }

        SetUIText();
    }

    private void OnDisable()
    {
        // We are only setting the data and rescheduling when the options panel is closed
        // to avoid case where UI shows incorrect time.
        // This means though that if the user closes the app before exiting the options, the new notification time will not be saved.
        SetPlayerPrefs();
        notificationManager.RescheduleDailyNotifications();
    }

    public void HourUp()
    {
        hour++;

        // cycle back to zero if too high
        if (hour > MAX_HOUR)
        {
            hour = 0;
        }

        SetHourText();
        //Debug.Log("Hour up");
    }

    public void HourDown()
    {
        hour--;

        // cycle back to 23 if too low
        if (hour < 0)
        {
            hour = MAX_HOUR;
        }

        SetHourText();
        //Debug.Log("Hour down");
    }

    public void MinuteUP()
    {
        minute += minuteIncrement;

        // cycle back to zero if too high
        if (minute > MAX_MINUTE)
        {
            minute = 0;
        }

        SetMinuteText();
        //Debug.Log("Minute up");
    }

    public void MinuteDown()
    {
        minute -= minuteIncrement;

        // cycle back to zero if too high
        if (minute < 0)
        {
            minute = MAX_MINUTE + 1 - minuteIncrement; // tweak to deal with cycling properly below zero
        }

        SetMinuteText();
        //Debug.Log("Minute down");
    }

    private void SetHourText()
    {
        hourText.text = hour.ToString("D2");
    }

    private void SetMinuteText()
    {
        minuteText.text = minute.ToString("D2");
    }

    private void SetUIText()
    {
        SetHourText();
        SetMinuteText();
    }

    public void SetPlayerPrefs()
    {
        //Debug.Log("Set player prefs for notification hour and minute");
        PlayerPrefs.SetInt(NotificationManager.PLAYERPREF_NAME_HOUR, hour);
        PlayerPrefs.SetInt(NotificationManager.PLAYERPREF_NAME_MINUTE, minute);
    }
}
