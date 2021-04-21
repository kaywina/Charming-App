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

    public void SetPlayerPrefName(string name)
    {
        playerPrefName = name;
    }

    public bool GetPlayerPrefValue()
    {
        string value = PlayerPrefs.GetString(playerPrefName);

        switch (value)
        {
            case "true":
                return true;
            case "false":
                return false;
            default:
                Debug.LogWarning("Hit default case in GetPlayerPrefValue");
                return toggle.isOn; // use scene object setting as default value; this case should not be hit
        }
    }

    protected void OnEnable()
    {
        SetToggleFromPlayerPref();
    }

    public void SetToggleFromPlayerPref()
    {
        CheckForKey();
        // retrieve the value and set the toggle
        string prefString = PlayerPrefs.GetString(playerPrefName);
        if (prefString == "false")
        {
            toggle.isOn = false;
            //Debug.Log("Set PlayerPref " + playerPrefName + " toggle off OnEnable");
        }
        else if (prefString == "true")
        {
            toggle.isOn = true;
            //Debug.Log("Set PlayerPref  " + playerPrefName + " toggle on OnEnable");
        }
        else
        {
            Debug.Log("Invalid player pref data");
        }
    }

    private void CheckForKey()
    {
        //Debug.Log("Check if PlayerPref " + playerPrefName + " exists");
        // set the default value if this is the first time this player pref is accessed
        if (!PlayerPrefs.HasKey(playerPrefName))
        {
            if (onByDefault)
            {
                PlayerPrefs.SetString(playerPrefName, "true");
                //Debug.Log("Default " + playerPrefName + " to true OnEnable");
            }
            else
            {
                PlayerPrefs.SetString(playerPrefName, "false");
                //Debug.Log("Default " + playerPrefName + " to false OnEnable");
            }
        }
    }

    public void TogglePlayerPref()
    {
        string prefString = PlayerPrefs.GetString(playerPrefName);
        if (prefString == "false")
        {
            PlayerPrefs.SetString(playerPrefName, "true");
            toggle.isOn = true;
            //Debug.Log("Toggle on the PlayerPref " + playerPrefName);
        }
        else
        {
            PlayerPrefs.SetString(playerPrefName, "false");
            toggle.isOn = false;
            //Debug.Log("Toggle off the PlayerPref " + playerPrefName);
        }
    }
}
