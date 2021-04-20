using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * When testing in Editor, you need to move mouse from game window over to inspector to see the script status (enabled/disabled) updated.
 * The Editor does not update in real-time, making it appears as though it isn't working when it is.
 * */

public class ToggleComponent : MonoBehaviour
{
    public SetPlayerPrefFromToggle playerPrefToggle;
    public GameObject targetObject;
    public enum ComponentType { AudioLowPassFilter, AudioHighPassFilter, AudioDistortionFilter, AudioChorusFilter };
    public ComponentType componentType;

    void OnEnable()
    {
        EnableDisableComponentFromPlayerPrefToggle();
    }

    public void EnableDisableComponentFromPlayerPrefToggle()
    {
        if (playerPrefToggle.GetPlayerPrefValue())
        {
            EnableComponentType();
        }
        else
        {
            DisableComponentType();
        }
    }

    private void EnableComponentType()
    {
        switch (componentType) {
            case ComponentType.AudioLowPassFilter:
                targetObject.GetComponent<AudioLowPassFilter>().enabled = true;
                break;
            case ComponentType.AudioHighPassFilter:
                targetObject.GetComponent<AudioHighPassFilter>().enabled = true;
                break;
            case ComponentType.AudioDistortionFilter:
                targetObject.GetComponent<AudioDistortionFilter>().enabled = true;
                break;
            case ComponentType.AudioChorusFilter:
                targetObject.GetComponent<AudioChorusFilter>().enabled = true;
                break;
            default:
                Debug.Log("Unsupported component type in ToggleComponent");
                break;
        }
    }

    private void DisableComponentType()
    {
        switch (componentType)
        {
            case ComponentType.AudioLowPassFilter:
                targetObject.GetComponent<AudioLowPassFilter>().enabled = false;
                break;
            case ComponentType.AudioHighPassFilter:
                targetObject.GetComponent<AudioHighPassFilter>().enabled = false;
                break;
            case ComponentType.AudioDistortionFilter:
                targetObject.GetComponent<AudioDistortionFilter>().enabled = false;
                break;
            case ComponentType.AudioChorusFilter:
                targetObject.GetComponent<AudioChorusFilter>().enabled = false;
                break;
            default:
                Debug.Log("Unsupported component type in ToggleComponent");
                break;
        }
    }
}
