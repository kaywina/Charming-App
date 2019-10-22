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

    private float secondsBeforeHide = 3f;

    private Color storedColor;

    void Awake()
    {
        CurrencyManager.OnCurrencyAdded += CurrencyIndicator.UpdateRegularBonusData;
        bonusGivenText.gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        CurrencyManager.OnCurrencyAdded -= CurrencyIndicator.UpdateRegularBonusData;
    }

    void OnEnable()
    {
        storedColor = totalAmountText.color;

        // this block handles case when returning from IAP store
        int stackedBonusRegular = CurrencyManager.GetStackedBonusRegular();
        int stackedBonusPremium = CurrencyManager.GetStackedBonusPremium();
        if (stackedBonusRegular > 0 || stackedBonusPremium > 0)
        {
            bonusGivenText.text = "+" + stackedBonusRegular.ToString();
            CurrencyManager.ResetStackedBonuseRegular();
            ShowBonusText();
            return;
        }

        if (currencyWasUpdated)
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
        currencyWasUpdated = false;
        updatedByAmountRegular = 0;
        bonusGivenText.gameObject.SetActive(false);
        totalAmountText.color = storedColor;
    }

    public static void UpdateRegularBonusData(int newValue)
    {
        currencyWasUpdated = true;
        updatedByAmountRegular = newValue;
    }

    private void HideBonusGivenText()
    {
        bonusGivenText.gameObject.SetActive(false);
        totalAmountText.color = storedColor;
    }

}
