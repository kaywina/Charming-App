using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerPrefFromToggle : MonoBehaviour
{
    public string playerPrefName;
    public Toggle toggle;
    public bool onByDefault = true;

    public string GetPlayerPrefName()
    {
        return playerPrefName;
    }

    private void CheckForKey()
    {
        Debug.Log("Check if player pref exists");
        // set the default value if this is the first time this player pref is accessed
        if (!PlayerPrefs.HasKey(playerPrefName))
        {
            if (onByDefault)
            {
                PlayerPrefs.SetString(playerPrefName, "true");
                Debug.Log("Default to true");
            }
            else
            {
                PlayerPrefs.SetString(playerPrefName, "false");
                Debug.Log("Default to false");
            }
        }
    }

    private void OnEnable()
    {
        CheckForKey();
        // retrieve the value and set the toggle
        string prefString = PlayerPrefs.GetString(playerPrefName);
        if (prefString == "false")
        {
            PlayerPrefs.SetString(playerPrefName, "false");
            toggle.isOn = false;
            Debug.Log("Set toggle off");
        }
        else
        {
            PlayerPrefs.SetString(playerPrefName, "true");
            toggle.isOn = true;
            Debug.Log("Set toggle on");
        }
    }

    public void TogglePlayerPref()
    {
        string prefString = PlayerPrefs.GetString(playerPrefName);
        if (prefString == "false")
        {
            PlayerPrefs.SetString(playerPrefName, "true");
            toggle.isOn = true;
            Debug.Log("Toggle on");
        }
        else
        {
            PlayerPrefs.SetString(playerPrefName, "false");
            toggle.isOn = false;
            Debug.Log("Toggle off");
        }
    }
}
