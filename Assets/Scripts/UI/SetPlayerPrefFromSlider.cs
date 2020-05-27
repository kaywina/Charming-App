using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerPrefFromSlider : MonoBehaviour
{
    public string playerPrefName;
    public Slider slider;

    public string GetPlayerPrefName()
    {
        return playerPrefName;
    }

    private void OnEnable()
    {
        SetSliderValue();
    }

    public void SetPlayerPref()
    {
        PlayerPrefs.SetFloat(playerPrefName, slider.value);
        Debug.Log("Set player pref from slider");
    }

    public void SetSliderValue()
    {
        if (!PlayerPrefs.HasKey(playerPrefName))
        {
            return; // do nothing if slider has never been changed
        }

        // retrieve the value and set the slider value if possible
        float prefFloat = PlayerPrefs.GetFloat(playerPrefName);
        slider.value = prefFloat;
        Debug.Log("Set slider value from player pref");
    }
}
