using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleCameraBackgroundColor : MonoBehaviour
{
    public Camera cam;

    // black should be first color for all palettes
    private string[] hexStrings = { "#000000", "#ff00bf", "#9500ff", "#4800ff", "#00b7ff", "#00fffb" }; // cyan to magenta palette

    private int defaultIndex = 0;
    private int index = 0;

    private string playerPrefName = "BackgroundColor";

    // Start is called before the first frame update
    void Start()
    {
        if (defaultIndex >= hexStrings.Length)
        {
            defaultIndex = 0;
        }

        if (string.IsNullOrEmpty(PlayerPrefs.GetString(playerPrefName)))
        {
            index = defaultIndex;
        }
        else
        {
            index = int.Parse(PlayerPrefs.GetString(playerPrefName));
        }

        SetColor();
    }

    void SetColor()
    {
        Color newColor = new Color();
        ColorUtility.TryParseHtmlString(hexStrings[index], out newColor);
        cam.backgroundColor = newColor;
        PlayerPrefs.SetString(playerPrefName, index.ToString());
    }

    public void NextColor()
    {
        index++;
        if (index > hexStrings.Length - 1) { index = 0; }
        SetColor();
    }


}
