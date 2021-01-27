using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationSlider : MonoBehaviour
{
    public BreatheControl breatheControl;
    public Slider slider;
    public Text valueText;
    private int defaultValue = 2;

    public void SetIntervalFromSliderValue()
    {
        switch (slider.value)
        {
            case 1:
                breatheControl.SetFastVibrationInterval(2.0f);
                break;
            case 2:
                breatheControl.SetFastVibrationInterval(1.5f);
                break;
            case 3:
                breatheControl.SetFastVibrationInterval(1.0f);
                break;
            case 4:
                breatheControl.SetFastVibrationInterval(0.5f);
                break;
            default:
                breatheControl.SetFastVibrationInterval(1.0f);
                break;
        }
        valueText.text = slider.value.ToString();
    }

    public void Reset()
    {
        slider.value = defaultValue;
        breatheControl.SetFastVibrationInterval(1f);
        valueText.text = defaultValue.ToString();
    }
}
