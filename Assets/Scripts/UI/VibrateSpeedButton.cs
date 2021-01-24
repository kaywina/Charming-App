using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrateSpeedButton : MonoBehaviour
{

    private string playerPrefName = "VibrateSpeed";
    public Sprite[] buttonImageFiles;
    public Image buttonImage;

    private int speedIndex;
    private int numberOfSpeeds;

    public void CycleSpeed()
    {
        speedIndex++;
        if (speedIndex >= buttonImageFiles.Length)
        {
            speedIndex = 0;
        }

        buttonImage.sprite = buttonImageFiles[speedIndex];
        PlayerPrefs.SetInt(playerPrefName, speedIndex);
        Debug.Log("Speed is " + speedIndex);
    }

    private void OnEnable()
    {
        speedIndex = PlayerPrefs.GetInt(playerPrefName);
        buttonImage.sprite = buttonImageFiles[speedIndex];
    }

    public int GetSpeedIndex()
    {
        return speedIndex;
    }

}
