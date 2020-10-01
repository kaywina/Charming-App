using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RememberGameSlider : MonoBehaviour
{
    public Slider slider;
    public GameRemember gameRemember;

    private float defaultIndex = 2;

    private string playerPrefName = "RememberGameNumberOfButtons"; // don't change this in production

    public void OnEnable()
    {
        Debug.Log("On Enable");
        Initialize();
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
   
        gameRemember.EnableButtonsByIndex((int)slider.value);
        
    }

    public int GetValue()
    {
        return (int)slider.value;
    }
}
