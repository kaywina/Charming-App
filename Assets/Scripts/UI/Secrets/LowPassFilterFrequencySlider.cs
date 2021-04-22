using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPassFilterFrequencySlider : PlayerPrefSlider
{
    public AudioLowPassFilter lowPassFilter;
    private float defaultFrequency = 5000;

    new private void OnEnable()
    {
        //Debug.Log("OnEnable in LowPassFilterFrequencySlider");
        lowPassFilter.cutoffFrequency = targetSlider.value;
        base.OnEnable();
    }

    public void UpdateValue()
    {
        lowPassFilter.cutoffFrequency = targetSlider.value;
        UpdatePlayerPrefFromSlider();
    }

    public void ResetToDefault()
    {
        lowPassFilter.cutoffFrequency = defaultFrequency;
        targetSlider.value = defaultFrequency;
        UpdatePlayerPrefFromSlider();
    }
}
