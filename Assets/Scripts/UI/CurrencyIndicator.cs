using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyIndicator : MonoBehaviour
{

    public Text bonusGivenText;
    public Text totalAmountText;

    private static bool currencyWasUpdated = false;
    private static int updatedByAmount = 0;

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
        int stackedBonus = CurrencyManager.GetStackedBonus();
        if (stackedBonus > 0)
        {
            bonusGivenText.text = "+" + stackedBonus.ToString();
            CurrencyManager.ResetStackedBonus();
            ShowBonusText();
            return;
        }

        if (currencyWasUpdated)
        {
            bonusGivenText.text = "+" + updatedByAmount.ToString();
            ShowBonusText();
        }
    }

    public void ShowBonusText()
    {
        bonusGivenText.gameObject.SetActive(true);
        totalAmountText.color = bonusGivenText.color;
        Invoke("HideBonusGivenText", secondsBeforeHide);
    }

    public void ShowBonusTextCustomAmount(int amount)
    {
        bonusGivenText.gameObject.SetActive(true);
        bonusGivenText.text = "+" + amount.ToString();
        totalAmountText.color = bonusGivenText.color;
        Invoke("HideBonusGivenText", secondsBeforeHide);
    }

    void OnDisable()
    {
        currencyWasUpdated = false;
        updatedByAmount = 0;
        bonusGivenText.gameObject.SetActive(false);
        totalAmountText.color = storedColor;
    }

    public static void UpdateRegularBonusData(int newValue)
    {
        currencyWasUpdated = true;
        updatedByAmount = newValue;
    }

    private void HideBonusGivenText()
    {
        bonusGivenText.gameObject.SetActive(false);
        totalAmountText.color = storedColor;
    }

}
