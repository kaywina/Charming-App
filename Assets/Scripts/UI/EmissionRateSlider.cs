using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmissionRateSlider : MonoBehaviour
{
    public BackgroundParticles bgParticles;
    public Slider slider;

    private float defaultRate = 50;

    private string playerPrefName = "EmissionSliderValue"; // don't change this in production

    public void Initialize()
    {
        if (!PlayerPrefs.HasKey(playerPrefName))
        {
            return; // just rely on default scene values if player pref hasn't been set yet
        }

        slider.value = PlayerPrefs.GetFloat(playerPrefName);
    }

    public void SetEmissionRateFromSliderValues()
    {
        if (slider.value <= 0)
        {
            Debug.Log("Slider value is less than zero; that shouldn't happen");
            bgParticles.SetEmissionRateMultiplier(defaultRate / 2);
            slider.value = 0;
            return;
        }

        // this is what should happen
        float emissionRateMultiplier = defaultRate * slider.value;
        bgParticles.SetEmissionRateMultiplier(emissionRateMultiplier);
        Debug.Log("Emission of background particles changed to " + emissionRateMultiplier);
        PlayerPrefs.SetFloat(playerPrefName, slider.value);
    }
}
