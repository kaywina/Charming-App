using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreatheControl : MonoBehaviour
{
    private float breatheInOutSeconds = 3f;
    public Slider secondsSlider;

    public delegate void SliderChangedAction();
    public static event SliderChangedAction OnSliderChanged;

    private void OnEnable()
    {

        int storedSecondsValue = PlayerPrefs.GetInt("BreatheSeconds");
        if (storedSecondsValue >= secondsSlider.minValue || storedSecondsValue <= secondsSlider.maxValue) // if in valid range
        {
            breatheInOutSeconds = storedSecondsValue;
        }
    }

    public float GetBreatheInOutSeconds()
    {
        return breatheInOutSeconds;
    }

    public void SetBreatheInOutSeconds(float seconds)
    {
        breatheInOutSeconds = seconds;
    }

    public void SetSecondsFromSlider()
    {
        breatheInOutSeconds = secondsSlider.value;
        OnSliderChanged(); // this triggers the method in BreatheAnimation to update the frame time and re-invoke animation method
    }
}
