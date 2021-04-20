using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPassFilterFrequencySlider : PlayerPrefSlider
{
    public AudioLowPassFilter lowPassFilter;

    new private void OnEnable()
    {
        Debug.Log("OnEnable in LowPassFilterFrequencySlider");
        lowPassFilter.cutoffFrequency = targetSlider.value;
        base.OnEnable();
    }

    public void UpdateValue()
    {
        lowPassFilter.cutoffFrequency = targetSlider.value;
        UpdatePlayerPrefFromSlider();
    }
}
