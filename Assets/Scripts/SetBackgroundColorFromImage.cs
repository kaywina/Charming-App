using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBackgroundColorFromImage : MonoBehaviour
{

    public SetMainCameraBackgroundColor setMainCameraBackgroundColor;
    public Image image;

    public void SetColor()
    {
        setMainCameraBackgroundColor.SetColor(image.color);
    }
}
