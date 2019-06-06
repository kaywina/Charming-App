using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMainCameraBackgroundColor : MonoBehaviour
{

    public Camera mainCamera;

    private string playerPrefName = "BackgroundColor";

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        Color bgColor = Color.black;

        // do not set color from playerpref on first run to avoid float parsing error; black is default
        if (PlayerPrefs.GetString("FirstRun") == "False")
        {
             bgColor = PlayerPrefsX.GetColor(playerPrefName);
        }
        else
        {
            PlayerPrefsX.SetColor(playerPrefName, Color.black);
        }
        
        SetColor(bgColor);
    }

    public void SetColor(Color newColor)
    {
        if (newColor != null)
        {
            mainCamera.backgroundColor = newColor;
            PlayerPrefsX.SetColor(playerPrefName, newColor);
        }
    }
}
