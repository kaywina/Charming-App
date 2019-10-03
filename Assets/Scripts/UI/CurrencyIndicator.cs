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

    void Awake()
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

        // this block handles case when returning from IAP store
        int stackedBonusRegular = CurrencyManager.GetStackedBonusRegular();
        int stackedBonusPremium = CurrencyManager.GetStackedBonusPremium();
        if (stackedBonusRegular > 0 || stackedBonusPremium > 0)
        {
            if (isForPremiumCurrency)
            {
                //Debug.Log("Show premium stacked bonus amount");
                bonusGivenText.text = "+" + stackedBonusPremium.ToString();
                CurrencyManager.ResetStackedBonusePremium();
            }
            else
            {
                //Debug.Log("Show regular stacked bonus amount");
                bonusGivenText.text = "+" + stackedBonusRegular.ToString();
                CurrencyManager.ResetStackedBonuseRegular();
            }
            ShowBonusText();
            return;
        }

        // cases below are for when bonus is given not from store (i.e. after watching a rewarded ad or spinning bonus wheel)
        if (isForPremiumCurrency && premiumCurrencyWasUpdated)
        {
            bonusGivenText.text = "+" + updatedByAmountPremium.ToString();
            ShowBonusText();
        }

        if (!isForPremiumCurrency && currencyWasUpdated)
        {
            bonusGivenText.text = "+" + updatedByAmountRegular.ToString();
            ShowBonusText();
        }
    }

    private void ShowBonusText()
    {
        bonusGivenText.gameObject.SetActive(true);
        totalAmountText.color = bonusGivenText.color;
        Invoke("HideBonusGivenText", secondsBeforeHide);
    }

    void OnDisable()
    {
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
        totalAmountText.color = storedColor;
    }

    public static void UpdateRegularBonusData(int newValue)
    {
        currencyWasUpdated = true;
        updatedByAmountRegular = newValue;
    }

    public static void UpdatePremiumBonusData(int newValue)
    {
        premiumCurrencyWasUpdated = true;
        updatedByAmountPremium = newValue;
    }

    private void HideBonusGivenText()
    {
        bonusGivenText.gameObject.SetActive(false);
        totalAmountText.color = storedColor;
    }

}
