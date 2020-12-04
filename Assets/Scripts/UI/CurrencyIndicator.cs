using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyIndicator : MonoBehaviour
{
    public Text currencyAmountText;

    private float repeatRate = 0.1f;

    private int newCurrencyAmount = 0;

    private bool animating = false;

    private void OnEnable()
    {
        UpdateIndicatorWithoutAnimation();
    }

    private void OnDisable()
    {
        CancelAnimation();
    }

    public void UpdateIndicatorWithoutAnimation()
    {
        //Debug.Log("Update Indicator");
        currencyAmountText.text = CurrencyManager.GetCurrencyInBank().ToString();
    }
    
    public void UpdateIndicatorAnimated()
    {
        // currency indicator gets told to start animating by all the iap buttons at once (because it is called in the listener for iap success, which triggers one each button)
        // so we use the flag to make sure the animation is only triggered once
        if (!animating)
        {
            //Debug.Log("Update Indicator Animated");
            newCurrencyAmount = int.Parse(currencyAmountText.text);
            currencyAmountText.color = Color.cyan;
            InvokeRepeating("IncrementCurrencyIndicator", 0, repeatRate);
            animating = true;
        }  
    }

    private void IncrementCurrencyIndicator()
    {
        //Debug.Log("Increment Currency Indicator");
        newCurrencyAmount++;

        if (newCurrencyAmount > CurrencyManager.GetCurrencyInBank())
        {
            CancelAnimation();
        }

        else
        {
            currencyAmountText.text = newCurrencyAmount.ToString();
        }
        
    }

    private void CancelAnimation()
    {
        //Debug.Log("Cancel currency indicator animation");
        CancelInvoke("IncrementCurrencyIndicator");
        currencyAmountText.color = Color.white;
        animating = false;
    }


}
