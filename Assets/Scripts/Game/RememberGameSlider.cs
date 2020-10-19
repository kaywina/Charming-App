using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RememberGameSlider : MonoBehaviour
{
    public Slider slider;
    public RememberGame gameRemember;
    public GameObject lockObject;

    private bool initialized = false;
    private float defaultIndex = 2;

    private string playerPrefName = "RememberGameNumberOfButtons"; // don't change this in production

    public GameObject sliderHintObject;
    public Text scoreMultiplierText;

    void OnEnable()
    {
        lockObject.SetActive(false);
        Initialize();
    }

    void OnDisable()
    {
        sliderHintObject.SetActive(false);
    }



    public void Initialize()
    {
        Debug.Log("Initialize Remember game slider");
        if (!PlayerPrefs.HasKey(playerPrefName))
        {
            slider.value = defaultIndex;
        }
        else
        {
            slider.value = PlayerPrefs.GetInt(playerPrefName);
        }

        SetNumberOfButtonsFromSliderValues();
        initialized = true;
        sliderHintObject.SetActive(true);
    }

    public void SetNumberOfButtonsFromSliderValues()
    {
        if (slider.value < 0)
        {
            Debug.Log("Slider value is less than zero; that shouldn't happen");
            slider.value = 0;
            return;
        }
        else // this is what should happen
        {
            PlayerPrefs.SetInt(playerPrefName, (int)slider.value);
        }

        if (initialized) {

            // there is where we control whether to show the buttons if a particular rank has been achieved, or the lock if not
            int rank = RankManager.GetRank();

            // for unranked players; lock difficulties above 5 buttons
            if (rank == 0)
            {
                int lockoutAboveThisIndex = 2;
                if (slider.value <= lockoutAboveThisIndex)
                {
                    Debug.Log("Show and setup the buttons");
                    lockObject.SetActive(false);
                    gameRemember.EnableButtonsByIndex((int)slider.value);
                    gameRemember.SetupButtons(false);
                }
                else
                {
                    Debug.Log("Show the lock");
                    lockObject.SetActive(true);
                    gameRemember.HideAllButtons(); // hide the last shown set of buttons; ugh this is awful
                }
            }
        }

        int scoreMultiplier = (int)slider.value + 1;
        scoreMultiplierText.text = Localization.GetTranslationByKey("SCORE_MULTIPLIER") + " = " + scoreMultiplier.ToString();
    }

    public int GetValue()
    {
        return (int)slider.value;
    }
}
