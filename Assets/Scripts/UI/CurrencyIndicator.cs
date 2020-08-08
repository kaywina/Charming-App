using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyIndicator : MonoBehaviour
{
    public Text currencyAmountText;

    private void OnEnable()
    {
        UpdateIndicator();
    }

    public void UpdateIndicator()
    {
        currencyAmountText.text = CurrencyManager.GetCurrencyInBank().ToString();
    }
}
