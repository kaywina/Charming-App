using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVibrateFromToggle : SetPlayerPrefFromToggle
{
    public BreatheControl breatheControl;

    new public void TogglePlayerPref()
    {
        string prefString = PlayerPrefs.GetString(playerPrefName);
        if (prefString == "false")
        {
            PlayerPrefs.SetString(playerPrefName, "true");
            toggle.isOn = true;
            breatheControl.ToggleVibration(true);
            Debug.Log("Toggle on vibration");
        }
        else
        {
            PlayerPrefs.SetString(playerPrefName, "false");
            toggle.isOn = false;
            breatheControl.ToggleVibration(false);
            Debug.Log("Toggle off vibration");
        }
    }
}
