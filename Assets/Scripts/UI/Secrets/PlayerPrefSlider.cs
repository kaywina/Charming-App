using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefSlider : MonoBehaviour
{

    public string playerPrefName = ""; // change this in inspector for production!
    public Slider targetSlider;
    
    protected void OnEnable()
    {
        //Debug.Log("Start in PlayerPrefSlider");
        if (PlayerPrefs.HasKey(playerPrefName)) { UpdateSliderFromPlayerPref(); }
    }

    // Update is called once per frame
    private void UpdateSliderFromPlayerPref()
    {
        //Debug.Log("Update slider value from player pref");
        targetSlider.value = PlayerPrefs.GetFloat(playerPrefName);
    }

    protected void UpdatePlayerPrefFromSlider() {
        //Debug.Log("Update player pref from slider");
        PlayerPrefs.SetFloat(playerPrefName, targetSlider.value);
    }
}
