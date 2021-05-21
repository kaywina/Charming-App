using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreatheControl : MonoBehaviour
{
    private float breatheInOutSeconds = 3f;
    private int numberOfBreaths = 0;
    private int breathsUntilBonus = 10;

    private static string vibratePlayerPrefName = "VibrateSpeed";
    public SetPlayerPrefFromToggle vibrateToggle;
    private bool fastVibrating = false;
    private float fastVibrationInterval = 1.5f;
    private string vibrateIntervalPlayerPrefName = "Vibrateinterval";

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

    public static string GetVibratePlayerPrefName()
    {
        return vibratePlayerPrefName;
    }

    void OnEnable()
    {
        DisableBonusIndicators();
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

        // set the vibrate interval from data
        if (PlayerPrefs.HasKey(vibrateIntervalPlayerPrefName)) 
        {
            fastVibrationInterval = PlayerPrefs.GetFloat(vibrateIntervalPlayerPrefName);
        }

        // this allows for initial vibration on meditate panel open for slow speed setting
        if (vibrateToggle.GetPlayerPrefValue())
        {
            if (GetVibrateFast())
            {
                if (!fastVibrating)
                {
                    //Debug.Log("Start fast vibration in OnEnable");
                    StartFastVibration();
                }
            }
            else
            {
                //Debug.Log("Start slow vibration in OnEnable");
                Vibrate();
            }
            
        }
    }

    public float GetFastVibrationInteval()
    {
        return PlayerPrefs.GetFloat(vibrateIntervalPlayerPrefName);
    }

    public void SetFastVibrationInterval(float interval)
    {
        fastVibrationInterval = interval;
        PlayerPrefs.SetFloat(vibrateIntervalPlayerPrefName, interval);
    }

    public void ToggleVibration(bool toggleOn)
    {
        if (toggleOn)
        {
            if (GetVibrateFast()) { StartFastVibration(); }
        }
        else
        {
            if (GetVibrateFast()) { StopFastVibration(); }
        }
    }

    private void OnDisable()
    {
        ResetBreaths();
        breatheIn = true;
        StopFastVibration();
    }

    public float GetBreatheInOutSeconds()
    {
        return breatheInOutSeconds;
    }

    public void SetBreatheInOutSeconds(float seconds)
    {
        breatheInOutSeconds = seconds;
        PlayerPrefs.SetFloat(playerPrefName, breatheInOutSeconds);
        if (vibrateToggle.GetPlayerPrefValue() && GetVibrateFast())
        {
            //Debug.Log("Reset fast vibration for SetBreathInOutSeconds");
            ResetFastVibration();
        }
    }

    public void SetSecondsFromSlider()
    {
        SetBreatheInOutSeconds(secondsSlider.value);
        secondsValueText.text = breatheInOutSeconds.ToString();
        if (OnSliderChanged != null) { OnSliderChanged(); } // this triggers the method in BreatheAnimation to update the frame time and re-invoke animation method
    }

    public void StartFastVibration()
    {
        InvokeRepeating("Vibrate", fastVibrationInterval, fastVibrationInterval);
        //Debug.Log("Start fast vibration");
        fastVibrating = true;
    }

    public void StopFastVibration()
    {
        CancelInvoke("Vibrate");
        //Debug.Log("Stop fast vibration");
        fastVibrating = false;
    }

    public void ResetFastVibration()
    {
        StopFastVibration();
        StartFastVibration();
    }

    private void Vibrate()
    {
        Handheld.Vibrate();
    }

    public void ResetBreaths()
    {
        //Debug.Log("Reset breaths");
        numberOfBreaths = 0;
        breathsText.text = breathsUntilBonus.ToString();
        if (Localization.CheckLocalization()) { breatheInOutLocMesh.localizationKey = "BREATHE_IN"; }
    }

    public void Breathe(bool breatheIsIn) // true if breathing in; false if breathing out
    {
        //Debug.Log("Breathe");

        // For SLOW vibration speed - vibrate every breathe in/out cycle
        if (vibrateToggle.GetPlayerPrefValue() && !GetVibrateFast())
        {
            Vibrate();
        }

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
            GoogleMobileAdsController.ShowInterstitialAd();
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


    public void SetVibrateFast(bool fast)
    {
        if (fast)
        {
            PlayerPrefs.SetString(GetVibratePlayerPrefName(), "Fast");
            if (vibrateToggle.GetPlayerPrefValue()) { ResetFastVibration(); }
            
        }
        else
        {
            PlayerPrefs.SetString(GetVibratePlayerPrefName(), "Slow");
            if (vibrateToggle.GetPlayerPrefValue()) { StopFastVibration(); }
        }
    }

    public bool GetVibrateFast()
    {
        string slowOrFast = PlayerPrefs.GetString(GetVibratePlayerPrefName());

        switch (slowOrFast)
        {
            case "Slow":
                return false;
            case "Fast":
                return true;
            default:
                return false;
        }
    }

}
