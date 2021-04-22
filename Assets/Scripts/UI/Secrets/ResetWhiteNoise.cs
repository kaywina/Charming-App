using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWhiteNoise : MonoBehaviour
{

    public LowPassFilterFrequencySlider lowPassFilterFrequencySlider;
    public LowPassFilterResonanceSlider lowPassFilterResonanceSlider;
    public HighPassFilterFrequencySlider highPassFilterFrequencySlider;
    public HighPassFilterResonanceSlider highPassFilterResonanceSlider;

    public void ResetToDefaultValues()
    {
        lowPassFilterFrequencySlider.ResetToDefault();
        lowPassFilterResonanceSlider.ResetToDefault();
        highPassFilterFrequencySlider.ResetToDefault();
        highPassFilterResonanceSlider.ResetToDefault();
    }
}
