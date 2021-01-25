using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrateSpeedButton : MonoBehaviour
{

    private string playerPrefName = "VibrateSpeed";
    public Sprite slowSprite;
    public Sprite fastSprite;
    public Image buttonImage;
    public Text label;

    private int numberOfSpeeds;

    // these keys must correspond to values defined in Resources -> Localization.csv
    private string slowLocKey = "SLOW";
    private string fastLocKey = "FAST";


    private void Start()
    {
        Initialize();
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        string slowOrFast = PlayerPrefs.GetString(playerPrefName);

        // this is opposite that in ChangeSpeed, since we are setting the speed instead of toggling it
        switch (slowOrFast)
        {
            case "Slow":
                SetSpeedToSlow();
                break;
            case "Fast":
                SetSpeedToFast();
                break;
            default:
                SetSpeedToSlow();
                break;
        }
    }

    public void ChangeSpeed()
    {
        string slowOrFast = PlayerPrefs.GetString(playerPrefName);

        switch (slowOrFast)
        {
            case "Slow":
                SetSpeedToFast();
                break;
            case "Fast":
                SetSpeedToSlow();
                break;
            default:
                SetSpeedToSlow();
                break;
        }
    }

    private void SetSpeedToSlow()
    {
        PlayerPrefs.SetString(playerPrefName, "Slow");
        buttonImage.sprite = slowSprite;
        label.text = Localization.GetTranslationByKey(slowLocKey); 
        Debug.Log("Set speed to slow");
    }

    private void SetSpeedToFast()
    {
        PlayerPrefs.SetString(playerPrefName, "Fast");
        buttonImage.sprite = fastSprite;
        label.text = Localization.GetTranslationByKey(fastLocKey);
        Debug.Log("Set speed to fast");
    }
}
