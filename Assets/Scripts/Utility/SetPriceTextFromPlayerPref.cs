using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPriceTextFromPlayerPref : MonoBehaviour
{
    public Text t;

    void OnEnable()
    {
        t.text = PlayerPrefs.GetString(UnityIAPController.GetLocalizedPricePlayerPrefName());
    }
}