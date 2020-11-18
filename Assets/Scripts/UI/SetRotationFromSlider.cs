using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetRotationFromSlider : MonoBehaviour
{
    public Transform t;
    public Slider slider;

    public enum RotationAxis { X, Y, Z };
    public RotationAxis axis;

    public void SetLocalRotation()
    {
        Quaternion newRot;
        // using zero values here instead of storing original values (like in SetPositionFromSlider) because of weird glitch bug on x axis
        switch (axis)
        {
            case RotationAxis.X:
                newRot = Quaternion.Euler(slider.value, 0, 0);
                //Debug.Log("Set X rotation from slider to " + slider.value);
                break;
            case RotationAxis.Y:
                newRot = Quaternion.Euler(0, slider.value, 0);
                //Debug.Log("Set Y rotation from slider");
                break;
            case RotationAxis.Z:
                newRot = Quaternion.Euler(0, 0, slider.value);
                //Debug.Log("Set Z rotation from slider");
                break;
            default:
                newRot = Quaternion.Euler(0, 0, 0);
                Debug.Log("Hit default case in SetRotationFromSlider; should not occur");
                break;
        }
        t.localRotation = newRot;
    }

    public void ResetSlider()
    {
        slider.value = 0;
    }
}
