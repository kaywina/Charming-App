using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPositionFromSlider : MonoBehaviour
{
    public Transform t;
    public Slider slider;

    public enum PositionAxis { X, Y, Z };
    public PositionAxis axis;

    public void SetLocalPosition()
    {
        Vector3 origPos = t.localPosition;
        switch (axis)
        { 
            case PositionAxis.X:
                t.localPosition = new Vector3(slider.value, origPos.y, origPos.z);
                //Debug.Log("Set X position from slider to " + slider.value);
                break;
            case PositionAxis.Y:
                t.localPosition = new Vector3(origPos.x, slider.value, origPos.z);
                //Debug.Log("Set Y position from slider");
                break;
            case PositionAxis.Z:
                t.localPosition = new Vector3(origPos.x, origPos.y, slider.value);
                //Debug.Log("Set Z position from slider");
                break;
            default:
                Debug.Log("Hit default case in SetPositionFromSlider; should not occur");
                break;
        }
    }
}
