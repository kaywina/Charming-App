using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBackgroundColor : MonoBehaviour
{

    public Camera mainCamera;
    private string playerPrefName = "BackgroundColor";
    private string initPlayerPrefName = "BackgroundColorInitialized";

    // Start is called before the first frame update
    void Start()
    {
        Color bgColor = Color.black;

        // do not set color from playerpref on first run to avoid float parsing error; black is default
        if (PlayerPrefs.GetString(initPlayerPrefName).Equals("True"))
        {
            bgColor = PlayerPrefsX.GetColor(playerPrefName);
        }
        else
        {
            PlayerPrefsX.SetColor(playerPrefName, Color.black);
            PlayerPrefs.SetString(initPlayerPrefName, "True");
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
