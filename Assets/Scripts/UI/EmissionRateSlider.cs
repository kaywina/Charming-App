using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmissionRateSlider : MonoBehaviour
{
    public BackgroundParticles bgParticles;
    public Slider slider;

    private float defaultRate = 50;

    public void SetEmissionRateFromSliderValues()
    {
        Debug.Log("Emission of background particles changed");

        if (slider.value <= 0)
        {
            bgParticles.SetEmissionRateMultiplier(defaultRate / 2);
        }
        else
        {
            bgParticles.SetEmissionRateMultiplier(defaultRate * slider.value);
        }
    }
}
