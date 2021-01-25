using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrateSpeedButton : MonoBehaviour
{

    public BreatheControl breatheControl;
    public Sprite slowSprite;
    public Sprite fastSprite;
    public Image buttonImage;
    public Text label;

    private int numberOfSpeeds;

    // these keys must correspond to values defined in Resources -> Localization.csv
    private string slowLocKey = "SLOW";
    private string fastLocKey = "FAST";

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        Debug.Log("Initialize vibrate speed button");
        // this is opposite that in ChangeSpeed, since we are setting the speed instead of toggling it
        switch (breatheControl.GetVibrateFast())
        {
            case true:
                Debug.Log("Initialize vibrate button to slow");
                buttonImage.sprite = slowSprite;
                label.text = Localization.GetTranslationByKey(slowLocKey);
                break;
            case false:
                Debug.Log("Initialize vibrate button to fast");
                buttonImage.sprite = fastSprite;
                label.text = Localization.GetTranslationByKey(fastLocKey);
                break;
            default:
                Debug.Log("Deefault case; initialize vibrate button to slow");
                buttonImage.sprite = slowSprite;
                label.text = Localization.GetTranslationByKey(slowLocKey);
                break;
        }
    }

    public void ChangeSpeed()
    {
        Debug.Log("Change vibrate speed");
        switch (breatheControl.GetVibrateFast())
        {
            case false:
                SetSpeedToFast();
                break;
            case true:
                SetSpeedToSlow();
                break;
            default:
                SetSpeedToSlow();
                break;
        }
    }

    private void SetSpeedToSlow()
    {
        breatheControl.SetVibrateFast(false);
        buttonImage.sprite = fastSprite;
        label.text = Localization.GetTranslationByKey(fastLocKey);
        Debug.Log("Set speed to slow");
    }

    private void SetSpeedToFast()
    {
        breatheControl.SetVibrateFast(true);
        buttonImage.sprite = slowSprite;
        label.text = Localization.GetTranslationByKey(slowLocKey);
        Debug.Log("Set speed to fast");
    }
}
