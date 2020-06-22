using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleSlider : MonoBehaviour
{
    public Slider slider;
    public Text valueText;

    public void SetTimeScaleFromSliderValue()
    {
        Time.timeScale = slider.value;
        valueText.text = slider.value.ToString("F2");
    }

    public void Reset()
    {
        slider.value = 1f;
        Time.timeScale = 1f;
        valueText.text = "1.00";
    }
}
