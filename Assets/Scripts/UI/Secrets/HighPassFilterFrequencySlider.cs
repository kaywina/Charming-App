using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPassFilterFrequencySlider : PlayerPrefSlider
{
    public AudioHighPassFilter highPassFilter;
    private float defaultFrequency = 5000;

    new private void OnEnable()
    {
        //Debug.Log("OnEnable in LowPassFilterFrequencySlider");
        highPassFilter.cutoffFrequency = targetSlider.value;
        base.OnEnable();
    }

    public void UpdateValue()
    {
        highPassFilter.cutoffFrequency = targetSlider.value;
        UpdatePlayerPrefFromSlider();
    }

    public void ResetToDefault()
    {
        highPassFilter.cutoffFrequency = defaultFrequency;
        targetSlider.value = defaultFrequency;
        UpdatePlayerPrefFromSlider();
    }
}
