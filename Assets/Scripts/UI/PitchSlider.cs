using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitchSlider : MonoBehaviour
{

    public AudioSource[] sources;
    public Slider slider;

    public void SetPitchFromSliderValue()
    {
        for (int s = 0; s < sources.Length; s++)
        {
            sources[s].pitch = slider.value;
        }
    }

    public void Reset()
    {
        slider.value = 1f;
        ResetSources();
    }

    public void ResetSources()
    {
        for (int s = 0; s < sources.Length; s++)
        {
            sources[s].pitch = 1;
        }
    }
}
