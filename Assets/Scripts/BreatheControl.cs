using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreatheControl : MonoBehaviour
{
    private float breatheInOutSeconds = 3f;
    public Slider secondsSlider;
    public LocalizationTextMesh breatheInOutLocMesh;
    public Text secondsValueText;

    private bool breatheIn = true; // flag for breathing in or out

    public delegate void SliderChangedAction();
    public static event SliderChangedAction OnSliderChanged;

    private string playerPrefName = "BreatheSeconds";

    void OnEnable()
    {
        float storedSecondsValue = PlayerPrefs.GetFloat(playerPrefName);
        //Debug.Log("storedSecondsValue = " + storedSecondsValue);
        if (storedSecondsValue >= secondsSlider.minValue && storedSecondsValue <= secondsSlider.maxValue) // if in valid range
        {
            //Debug.Log("set breathe in/out seconds from stored value");
            secondsSlider.value = storedSecondsValue;
            breatheInOutSeconds = storedSecondsValue;
        }
        secondsValueText.text = breatheInOutSeconds.ToString();
    }

    public float GetBreatheInOutSeconds()
    {
        return breatheInOutSeconds;
    }

    public void SetBreatheInOutSeconds(float seconds)
    {
        breatheInOutSeconds = seconds;
        PlayerPrefs.SetFloat(playerPrefName, breatheInOutSeconds);
    }

    public void SetSecondsFromSlider()
    {
        SetBreatheInOutSeconds(secondsSlider.value);
        secondsValueText.text = breatheInOutSeconds.ToString();
        OnSliderChanged(); // this triggers the method in BreatheAnimation to update the frame time and re-invoke animation method
    }

    public void SetBreatheInOutFlag(bool newFlag)
    {
        breatheIn = newFlag;
        if (breatheIn == true)
        {
            breatheInOutLocMesh.localizationKey = "BREATHE_IN";
        }
        else
        {
            breatheInOutLocMesh.localizationKey = "BREATHE_OUT";
        }
        breatheInOutLocMesh.ChangeText();
    }

    public bool GetBreatheInOutFlag()
    {
        return breatheIn;
    }
}
