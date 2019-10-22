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
    public StorePanel storePanel;
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

    private static int stackedBonusRegular = 0; // to handle multiple store purchases before leaving store
    private static int stackedBonusPremium = 0; // to handle multiple store purchases before leaving store

    private bool canOpenBonusPanel = false;

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

        canOpenBonusPanel = SetCanOpenBonusPanel();
    }

    public bool SetCanOpenBonusPanel()
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

    public bool GetCanOpenBonusPanel()
    {
        return canOpenBonusPanel;
    }

    void SetCurrencyOnStart(int amountSilver, int amountGold)
    {
        currencyInBankSilver = amountSilver;
        currencyInBankGold = amountGold;
        PlayerPrefs.SetInt("Currency", currencyInBankSilver);
        PlayerPrefs.SetInt("PremiumCurrency", currencyInBankGold);
        SetCurrencyText();
    }

    public static int GetStackedBonusRegular()
    {
        return stackedBonusRegular;
    }

    public static int GetStackedBonusPremium()
    {
        return stackedBonusPremium;
    }

    public static void ResetStackedBonuseRegular()
    {
        stackedBonusRegular = 0;
    }

    public static void ResetStackedBonusePremium()
    {
        stackedBonusPremium = 0;
    }

    public void GiveRegularBonus(int bonus, bool isPurchase = false)
    {
        if (bonus <= 0) { return; }
        currencyInBankSilver += bonus;
        PlayerPrefs.SetInt("Currency", currencyInBankSilver);
        SetCurrencyText(); 

        if (isPurchase && storePanel != null) // this case handles times when a purchase is made in the store, and we want to be sure that the currency indicator on main ui is updated properly if multiple purchases are made before leaving store
        {
            stackedBonusRegular += bonus;
            //Debug.Log("Stacked Bonus Regular is " + stackedBonusRegular);
            storePanel.ShowThankYou();
        }
        else // this case handles all other times a bonus is given; i.e. when stacking is not a problem
        {
            if (OnCurrencyAdded != null) { OnCurrencyAdded(bonus); }
            else { Debug.Log("delegate is null"); }
        }
    }

    public void GivePremiumBonus(int bonus, bool isPurchase = false)
    {
        if (bonus <= 0) { return; }
        currencyInBankGold += bonus;
        PlayerPrefs.SetInt("PremiumCurrency", currencyInBankGold);
        SetCurrencyText();
        
        if (isPurchase && storePanel != null) // this case handles times when a purchase is made in the store, and we want to be sure that the currency indicator on main ui is updated properly if multiple purchases are made before leaving store
        {
            stackedBonusPremium += bonus;
            //Debug.Log("Stacked Bonus Premium is " + stackedBonusPremium);
            storePanel.ShowThankYou();
        }
        else // this case handles all other times a bonus is given; i.e. when stacking is not a problem
        {
            if (OnPremiumCurrencyAdded != null) { OnPremiumCurrencyAdded(bonus); }
            else { Debug.Log("delegate is null"); }
        }
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
