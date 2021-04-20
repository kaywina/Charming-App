using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPassFilterFrequencySlider : PlayerPrefSlider
{
    public AudioHighPassFilter highPassFilter;

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
}
