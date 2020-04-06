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
        numberOfBreaths = 0;
        breathsText.text = "0";
    }

    public void Breathe(bool breatheIsIn) // true if breathing in; false if breathing out
    {
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

        soundManager.PlayChimeNoteInScale();
    }

    public bool GetBreatheInOutFlag()
    {
        return breatheIn;
    }
}
