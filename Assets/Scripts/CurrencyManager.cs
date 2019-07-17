using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour {

    public static CurrencyManager Instance;

    public GameObject welcomePanel;
    public GameObject infoPanel;
    public GameObject bonusPanel;
    public Text currencyTextSilver;
    public Text currencyTextGold;

    private static int welcomeBonusSilver = 8;
    private static int welcomeBonusGold = 0;
    public Text welcomeBonusText;

    private static int currencyInBankSilver = 0;
    private static int currencyInBankGold = 0;

    public delegate void CurrencyAddedAction(int n);
    public static event  CurrencyAddedAction OnCurrencyAdded;

    public delegate void PremiumCurrencyAddedAction(int n);
    public static event PremiumCurrencyAddedAction OnPremiumCurrencyAdded;

    // Use this for initialization
    void Start ()
    {
        Instance = this;

        welcomeBonusText.text = welcomeBonusSilver.ToString();

        // on first time running app
        if (!PlayerPrefs.GetString("FirstRun").Equals("False"))
        {
            //Debug.Log("Give currency bonus on first run");
            SetCurrencyOnStart(welcomeBonusSilver, welcomeBonusGold);
            welcomePanel.SetActive(true);
        }
        else
        {
            currencyInBankSilver = PlayerPrefs.GetInt("Currency");
            currencyInBankGold = PlayerPrefs.GetInt("PremiumCurrency");
            infoPanel.SetActive(true);
        }

        SetCurrencyText();
        PlayerPrefs.SetString("FirstRun", "False");
    }

    public bool CanOpenBonusPanel()
    {
        DateTime currentDateTime = System.DateTime.Now;
        int currentDayOfYear = currentDateTime.DayOfYear;
        int currentYear = currentDateTime.Year;

        int storedDayOfYear = PlayerPrefs.GetInt("Day");
        int storedYear = PlayerPrefs.GetInt("Year");
        PlayerPrefs.SetInt("Day", currentDayOfYear);
        PlayerPrefs.SetInt("Year", currentYear);

        if (!PlayerPrefs.GetString("FirstRun").Equals("False") || (currentDayOfYear > storedDayOfYear && currentYear >= storedYear)) 
        {
            return true;
        }
        else
        {
            return false;
        }   
    }

    private void Update()
    {
#if UNITY_EDITOR // disable cheats in builds
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            CurrencyManager.Instance.GiveRegularBonus(100);
            CurrencyManager.Instance.GivePremiumBonus(100);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            CurrencyManager.Instance.ClearCurrency();
        }
#endif
    }

    void SetCurrencyOnStart(int amountSilver, int amountGold)
    {
        currencyInBankSilver = amountSilver;
        currencyInBankGold = amountGold;
        PlayerPrefs.SetInt("Currency", currencyInBankSilver);
        PlayerPrefs.SetInt("PremiumCurrency", currencyInBankGold);
        SetCurrencyText();
    }

    public void GiveRegularBonus(int bonus)
    {
        if (bonus <= 0) { return; }
        currencyInBankSilver += bonus;
        PlayerPrefs.SetInt("Currency", currencyInBankSilver);
        SetCurrencyText();
        if (OnCurrencyAdded != null) { OnCurrencyAdded(bonus); }
        else { Debug.Log("delegate is null");  }
    }

    public void GivePremiumBonus(int bonus)
    {
        if (bonus <= 0) { return; }
        currencyInBankGold += bonus;
        PlayerPrefs.SetInt("PremiumCurrency", currencyInBankGold);
        SetCurrencyText();
        if (OnPremiumCurrencyAdded != null) { OnPremiumCurrencyAdded(bonus); }
        else { Debug.Log("delegate is null"); }
    }

#if UNITY_EDITOR // disable cheats in builds
    public void ClearCurrency()
    {
        currencyInBankSilver = 0;
        currencyInBankGold = 0;
        PlayerPrefs.SetInt("Currency", currencyInBankSilver);
        PlayerPrefs.SetInt("PemiumCurrency", currencyInBankGold);
        SetCurrencyText();
    }
    #endif

    public static void SetCurrencyText()
    {

        Instance.currencyTextSilver.text = currencyInBankSilver.ToString();
        Instance.currencyTextGold.text = currencyInBankGold.ToString();
    }

    public static void SetCurrencyInBankSilver(int amount)
    {
        currencyInBankSilver = amount;
        PlayerPrefs.SetInt("Currency", currencyInBankSilver);
        SetCurrencyText();
    }

    public static void SetCurrencyInBankGold(int amount)
    {
        currencyInBankGold = amount;
        PlayerPrefs.SetInt("PremiumCurrency", currencyInBankGold);
        SetCurrencyText();
    }

    public static void WithdrawAmountSilver(int amount)
    {
        SetCurrencyInBankSilver(currencyInBankSilver - amount);
    }

    public static bool CanWithdrawAmountSilver(int amount)
    {
        if (amount <= currencyInBankSilver)
        {
            return true;
        }
        return false;
    }

    public static void WithdrawAmountGold(int amount)
    {
        SetCurrencyInBankGold(currencyInBankGold - amount);
    }

    public static bool CanWithdrawAmountGold(int amount)
    {
        if (amount <= currencyInBankGold)
        {
            return true;
        }
        return false;
    }
}
