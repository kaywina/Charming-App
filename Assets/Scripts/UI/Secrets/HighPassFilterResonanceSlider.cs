using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPassFilterResonanceSlider : PlayerPrefSlider
{
    public AudioHighPassFilter highPassFilter;
    private float defaultResonance = 1;

    new private void OnEnable()
    {
        //Debug.Log("OnEnable in LowPassFilterFrequencySlider");
        highPassFilter.highpassResonanceQ = targetSlider.value;
        base.OnEnable();
    }

    public void UpdateValue()
    {
        highPassFilter.highpassResonanceQ = targetSlider.value;
        UpdatePlayerPrefFromSlider();
    }

    public void ResetToDefault()
    {
        highPassFilter.highpassResonanceQ = defaultResonance;
        targetSlider.value = defaultResonance;
        UpdatePlayerPrefFromSlider();
    }
}
