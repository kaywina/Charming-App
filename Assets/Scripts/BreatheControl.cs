using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreatheControl : MonoBehaviour
{
    private float breatheInOutSeconds = 3f;
    private int numberOfBreaths = 0;
    private int breathsUntilBonus = 10;

    public Text breathsText;
    public Slider secondsSlider;
    public LocalizationTextMesh breatheInOutLocMesh;
    public Text secondsValueText;

    public GameObject bonusIndicatorGold;
    public GameObject bonusIndicatorNonGold;
    public CurrencyManager currencyManager;
    public int bonusKeysForBreaths = 1;

    private bool breatheIn = true; // flag for breathing in or out

    public delegate void SliderChangedAction();
    public static event SliderChangedAction OnSliderChanged;

    private string playerPrefName = "BreatheSeconds";

    public SoundManager soundManager;

    public ParticleSystem fireworks;

    void OnEnable()
    {
        DisableBonusIndicators();
        ResetBreaths(true); // not tracking number of breaths between sessions
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
        ResetBreaths(false);
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

    private void Vibrate()
    {
        Handheld.Vibrate();
    }

    public void ResetBreaths(bool fromOnEnable)
    {
        //Debug.Log("Reset breaths");
        numberOfBreaths = 0;
        breathsText.text = breathsUntilBonus.ToString();
        if (Localization.CheckLocalization()) { breatheInOutLocMesh.localizationKey = "BREATHE_IN"; }

        // vibrate on first breathe in when meditation panel is open
        if (fromOnEnable)
        {
            Vibrate();
        }
    }

    public void Breathe(bool breatheIsIn) // true if breathing in; false if breathing out
    {
        //Debug.Log("Breathe");

        // vibrate every breathe in/out cycle
        Vibrate();

        breatheIn = breatheIsIn;
        if (breatheIn == true)
        {

            if (Localization.CheckLocalization()) { breatheInOutLocMesh.localizationKey = "BREATHE_IN"; }
            numberOfBreaths++;

            // instead of showing total number of breaths, now show the remainder until bonus is reached
            //breathsText.text = numberOfBreaths.ToString(); // this was when breaths text was just the total number
            breathsText.text = (breathsUntilBonus - numberOfBreaths % breathsUntilBonus).ToString();
        }
        else
        {
            if (Localization.CheckLocalization()) { breatheInOutLocMesh.localizationKey = "BREATHE_OUT"; }
        }

        if (Localization.CheckLocalization()) { breatheInOutLocMesh.ChangeText(); }

        soundManager.PlayBreathNoteInScale();

        if (breatheIn && numberOfBreaths != 0 && numberOfBreaths % breathsUntilBonus == 0) // after 10, 20, 30, breaths etc
        {
            fireworks.Play();
            float seconds = 5f;
            Invoke("StopFireworks", seconds);
            ShowBonusIndicatorAndGiveBonus();
        }
    }

    public bool GetBreatheInOutFlag()
    {
        return breatheIn;
    }

    private void StopFireworks()
    {
        if (fireworks != null) { fireworks.Stop(); }
    }

    private void ShowBonusIndicatorAndGiveBonus()
    {
        //Debug.Log("Show bonus indicator and give bonus");
        if (UnityIAPController.IsGold())
        {
            bonusIndicatorGold.SetActive(true);
            currencyManager.GiveBonus(bonusKeysForBreaths);
        }
        else
        {
            bonusIndicatorNonGold.SetActive(true);
            currencyManager.GiveBonus(bonusKeysForBreaths * 3);
        }
        CancelInvoke("DisableBonusIndicators");
        float secondsToShowBonusIndicator = 8f;
        Invoke("DisableBonusIndicators", secondsToShowBonusIndicator); // show indicator for two seconds
    }

    private void DisableBonusIndicators()
    {
        bonusIndicatorGold.SetActive(false);
        bonusIndicatorNonGold.SetActive(false);
    }

}
