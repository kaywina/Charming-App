using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBackgroundColorFromImage : MonoBehaviour
{
    public SetBackgroundColor setBackgroundColor;
    public Image image;

    public void SetColor()
    {
        setBackgroundColor.SetColor(image.color);
    }
}
