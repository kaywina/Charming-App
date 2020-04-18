using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPriceTextFromPlayerPref : MonoBehaviour
{
    public Text t;
    private string defaultPriceString = "$8.49 CAD";

    void OnEnable()
    {
        string localizedPrice = PlayerPrefs.GetString(UnityIAPController.GetLocalizedPricePlayerPrefName());
        if (string.IsNullOrEmpty(localizedPrice))
        {
            localizedPrice = defaultPriceString;
        }
        t.text = localizedPrice;
    }
}