using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreatheControl : MonoBehaviour
{
    private float breatheInOutSeconds = 3f;
    private int numberOfBreaths = 0;

    public Text breathsText;
    public Slider secondsSlider;
    public LocalizationTextMesh breatheInOutLocMesh;
    public Text secondsValueText;

    private bool breatheIn = true; // flag for breathing in or out

    public delegate void SliderChangedAction();
    public static event SliderChangedAction OnSliderChanged;

    private string playerPrefName = "BreatheSeconds";

    public SoundManager soundManager;

    public ParticleSystem yayParticles;

    void OnEnable()
    {
        ResetBreaths(); // not tracking number of breaths between sessions
        float storedSecondsValue = PlayerPrefs.GetFloat(playerPrefName);
        //Debug.Log("storedSecondsValue = " + storedSecondsValue);
        if (storedSecondsValue >= secondsSlider.minValue && storedSecondsValue <= secondsSlider.maxValue) // if in valid range
        {
            //Debug.Log("set breathe in/out seconds from stored value");
            secondsSlider.value = storedSecondsValue;
            breatheInOutSeconds = storedSecondsValue;
        }
        secondsValueText.text = breatheInOutSeconds.ToString();
    }

    private void OnDisable()
    {
        ResetBreaths();
        breatheIn = true;
    }

    public float GetBreatheInOutSeconds()
    {
        return breatheInOutSeconds;
    }

    public void SetBreatheInOutSeconds(float seconds)
    {
        breatheInOutSeconds = seconds;
        PlayerPrefs.SetFloat(playerPrefName, breatheInOutSeconds);
    }

    public void SetSecondsFromSlider()
    {
        SetBreatheInOutSeconds(secondsSlider.value);
        secondsValueText.text = breatheInOutSeconds.ToString();
        if (OnSliderChanged != null) { OnSliderChanged(); } // this triggers the method in BreatheAnimation to update the frame time and re-invoke animation method
    }

    public void ResetBreaths()
    {
        //Debug.Log("Reset breaths");
        numberOfBreaths = 0;
        breathsText.text = "0";
        if (Localization.CheckLocalization()) { breatheInOutLocMesh.localizationKey = "BREATHE_IN"; }
    }

    public void Breathe(bool breatheIsIn) // true if breathing in; false if breathing out
    {
        //Debug.Log("Breathe");
        breatheIn = breatheIsIn;
        if (breatheIn == true)
        {
            if (Localization.CheckLocalization()) { breatheInOutLocMesh.localizationKey = "BREATHE_IN"; }
            numberOfBreaths++;
            breathsText.text = numberOfBreaths.ToString();
        }
        else
        {
            if (Localization.CheckLocalization()) { breatheInOutLocMesh.localizationKey = "BREATHE_OUT"; }
        }

        if (Localization.CheckLocalization()) { breatheInOutLocMesh.ChangeText(); }

        soundManager.PlayBreathNoteInScale();

        if (numberOfBreaths != 0 && numberOfBreaths % 10 == 0) // after 10, 20, 30, breaths etc
        {
            yayParticles.Play();
        }
    }

    public bool GetBreatheInOutFlag()
    {
        return breatheIn;
    }
}
