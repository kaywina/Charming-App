using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerPrefFromToggle : MonoBehaviour
{
    public string playerPrefName;
    public Toggle toggle;

    private void OnEnable()
    {
        string prefString = PlayerPrefs.GetString(playerPrefName);
        if (prefString == "false")
        {
            PlayerPrefs.SetString(playerPrefName, "false");
            toggle.isOn = false;
        }
        else
        {
            PlayerPrefs.SetString(playerPrefName, "true");
            toggle.isOn = true;
        }
    }

    public void TogglePlayerPref()
    {
        string prefString = PlayerPrefs.GetString(playerPrefName);
        if (prefString == "false")
        {
            PlayerPrefs.SetString(playerPrefName, "true");
            toggle.isOn = true;
        }
        else
        {
            PlayerPrefs.SetString(playerPrefName, "false");
            toggle.isOn = false;
        }
    }
}
