using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefSlider : MonoBehaviour
{

    public string playerPrefName = ""; // change this in inspector for production!
    public Slider targetSlider;

    public Text minValueText;
    public Text maxValueText;
    
    protected void OnEnable()
    {
        Debug.Log("Start in PlayerPrefSlider");
        if (PlayerPrefs.HasKey(playerPrefName)) { UpdateSliderFromPlayerPref(); }
        if (minValueText != null) { minValueText.text = targetSlider.minValue.ToString(); }
        if (maxValueText != null) { maxValueText.text = targetSlider.maxValue.ToString(); }
    }

    // Update is called once per frame
    private void UpdateSliderFromPlayerPref()
    {
        Debug.Log("Update slider value from player pref");
        targetSlider.value = PlayerPrefs.GetFloat(playerPrefName);
    }

    protected void UpdatePlayerPrefFromSlider() {
        Debug.Log("Update player pref from slider");
        PlayerPrefs.SetFloat(playerPrefName, targetSlider.value);
    }
}
