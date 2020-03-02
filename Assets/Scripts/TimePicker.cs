using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePicker : MonoBehaviour
{

    public Text hourText;

    private int hour = 11; // 11:00 is default time
    private const int MAX_HOUR = 23; // set for a 24-hour day cycle

    public void HourUp()
    {
        hour++;

        // cycle back to zero if too high
        if (hour > MAX_HOUR)
        {
            hour = 0;
        }

        SetHourText();
        Debug.Log("Hour up");
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
        Debug.Log("Hour down");
    }

    private void SetHourText()
    {
        hourText.text = hour.ToString();
    }
}
