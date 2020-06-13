using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitchSlider : MonoBehaviour
{

    public AudioSource[] sources;
    public Slider slider;
    public Text valueText;

    public void SetPitchFromSliderValue()
    {
        for (int s = 0; s < sources.Length; s++)
        {
            sources[s].pitch = slider.value;
        }

        valueText.text = slider.value.ToString("F2");
    }

    public void Reset()
    {
        slider.value = 1f;
        ResetSources();
        valueText.text = "1.00";
    }

    public void ResetSources()
    {
        for (int s = 0; s < sources.Length; s++)
        {
            sources[s].pitch = 1;
        }
    }
}
