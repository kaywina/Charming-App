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

        SetNumberOfButtonsFromSliderValues(); // this is getting called twice, also from OnValueChanged
        sliderHintObject.SetActive(true);
    }

    public void SetNumberOfButtonsFromSliderValues()
    {
        Debug.Log("Set number of buttons from slider values");
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

        // there is where we control whether to show the buttons if a particular rank has been achieved, or the lock if not
        int rank = RankManager.GetRank();

        // lock out buttons depending on rank of player
        if (rank == 0)
        {
            LockButtonsAboveIndex(2);
        }
        else if (rank == 1)
        {
            LockButtonsAboveIndex(3);
        }
        else if(rank == 2)
        {
            LockButtonsAboveIndex(4);
        }
        else if(rank == 3)
        {
            LockButtonsAboveIndex(5);
        }
        else if(rank == 4)
        {
            LockButtonsAboveIndex(6);
        }
        else if (rank == 5)
        {
            LockButtonsAboveIndex(7); // this should unlock everything as long as there are only seven difficulty levels
        }
        else
        {
            LockButtonsAboveIndex(7); // this should unlock everything as long as there are only seven difficulty levels
        }

        int scoreMultiplier = (int)slider.value + 1;
        scoreMultiplierText.text = Localization.GetTranslationByKey("SCORE_MULTIPLIER") + " = " + scoreMultiplier.ToString();
    }

    private void LockButtonsAboveIndex (int index)
    {
        if (slider.value <= index)
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

    public int GetValue()
    {
        return (int)slider.value;
    }
}
