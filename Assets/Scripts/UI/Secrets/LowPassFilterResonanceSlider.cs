using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPassFilterResonanceSlider : PlayerPrefSlider
{
    public AudioLowPassFilter lowPassFilter;
    private float defaultResonance = 1;

    new private void OnEnable()
    {
        //Debug.Log("OnEnable in LowPassFilterFrequencySlider");
        lowPassFilter.lowpassResonanceQ = targetSlider.value;
        base.OnEnable();
    }

    public void UpdateValue()
    {
        lowPassFilter.lowpassResonanceQ = targetSlider.value;
        UpdatePlayerPrefFromSlider();
    }

    public void ResetToDefault()
    {
        lowPassFilter.lowpassResonanceQ = defaultResonance;
        targetSlider.value = defaultResonance;
        UpdatePlayerPrefFromSlider();
    }
}
