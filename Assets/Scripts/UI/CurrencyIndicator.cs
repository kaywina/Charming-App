using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyIndicator : MonoBehaviour
{

    public Text bonusGivenText;
    public Text totalAmountText;

    private static bool currencyWasUpdated = false;
    private static int updatedByAmountRegular = 0;

    private static bool premiumCurrencyWasUpdated = false;
    private static int updatedByAmountPremium = 0;

    public bool isForPremiumCurrency = false;
    private float secondsBeforeHide = 3f;

    private Color storedColor;

    void Start()
    {
        if (isForPremiumCurrency)
        {
            CurrencyManager.OnPremiumCurrencyAdded += CurrencyIndicator.UpdatePremiumBonusData;
        }
        else
        {
            CurrencyManager.OnCurrencyAdded += CurrencyIndicator.UpdateRegularBonusData;
        }
        
        bonusGivenText.gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        if (isForPremiumCurrency)
        {
            CurrencyManager.OnPremiumCurrencyAdded -= CurrencyIndicator.UpdatePremiumBonusData;
        }
        else
        {
            CurrencyManager.OnCurrencyAdded -= CurrencyIndicator.UpdateRegularBonusData;
        }
    }

    void OnEnable()
    {
        storedColor = totalAmountText.color;
        
        //Debug.Log("OnEnable in " + gameObject.name);
        if (isForPremiumCurrency && premiumCurrencyWasUpdated)
        {
            bonusGivenText.text = "+" + updatedByAmountPremium.ToString();
            bonusGivenText.gameObject.SetActive(true);
            totalAmountText.color = bonusGivenText.color;
            Invoke("HideBonusGivenText", secondsBeforeHide);
        }

        if (!isForPremiumCurrency && currencyWasUpdated)
        {
            bonusGivenText.text = "+" + updatedByAmountRegular.ToString();
            bonusGivenText.gameObject.SetActive(true);
            totalAmountText.color = bonusGivenText.color;
            Invoke("HideBonusGivenText", secondsBeforeHide);
        } 
    }

    void OnDisable()
    {
        //Debug.Log("OnDisable in " + gameObject.name);
        if (isForPremiumCurrency)
        {
            premiumCurrencyWasUpdated = false;
            updatedByAmountPremium = 0;
        }
        else
        {
            currencyWasUpdated = false;
            updatedByAmountRegular = 0;
        }
        bonusGivenText.gameObject.SetActive(false);
    }

    public static void UpdateRegularBonusData(int newValue)
    {
        //Debug.Log("Update regular bonus data");
        currencyWasUpdated = true;
        updatedByAmountRegular = newValue;
    }

    public static void UpdatePremiumBonusData(int newValue)
    {
        //Debug.Log("Update premium bonus data");
        premiumCurrencyWasUpdated = true;
        updatedByAmountPremium = newValue;
    }

    private void HideBonusGivenText()
    {
        bonusGivenText.gameObject.SetActive(false);
        totalAmountText.color = storedColor;
    }

}
