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

    private static int welcomeBonusSilver = 32;
    public Text welcomeBonusText;

    private static int currencyInBank = 0;

    public delegate void CurrencyAddedAction(int n);
    public static event  CurrencyAddedAction OnCurrencyAdded;

    private static int stackedBonus = 0; // to handle multiple store purchases before leaving store

    private bool canOpenBonusPanel = false;

    public static bool newDayThisSession = false;

    // Use this for initialization
    void Start ()
    {
        Instance = this;

        welcomeBonusText.text = welcomeBonusSilver.ToString();

        // on first time running app
        if (!PlayerPrefs.GetString("FirstRun").Equals("False"))
        {
            //Debug.Log("Give currency bonus on first run");
            SetCurrencyOnStart(welcomeBonusSilver);
            welcomePanel.SetActive(true);
        }
        else
        {
            currencyInBank = PlayerPrefs.GetInt("Currency");
            infoPanel.SetActive(true);
        }

        SetCurrencyText();
        canOpenBonusPanel = SetCanOpenBonusPanel();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("FirstRun", "False");
    }

    public bool SetCanOpenBonusPanel()
    {
        if (!PlayerPrefs.GetString("FirstRun").Equals("False") || IsNewDay(true, "Day", "Year")) // don't change string values in post-production
        {
            //Debug.Log("Yes can open bonus panel");
            return true;
        }
        else
        {
            //Debug.Log("No cannot open bonus panel");
            return false;
        }   
    }

    public static bool IsNewDay(bool setStoredValues, string dayPrefName, string yearPrefName)
    {
        DateTime currentDateTime = System.DateTime.Now;
        int currentDayOfYear = currentDateTime.DayOfYear;
        int currentYear = currentDateTime.Year;

        int storedDayOfYear = PlayerPrefs.GetInt(dayPrefName);
        int storedYear = PlayerPrefs.GetInt(yearPrefName);

        if (setStoredValues)
        {
            PlayerPrefs.SetInt(dayPrefName, currentDayOfYear);
            PlayerPrefs.SetInt(yearPrefName, currentYear);
        }
 
        if (currentDayOfYear > storedDayOfYear && currentYear >= storedYear)
        {
            newDayThisSession = true;
            //Debug.Log("It's a new day!");
            return true;
        }

        //Debug.Log("It is not a new day!");
        return false;
    }

    public bool GetCanOpenBonusPanel()
    {
        return canOpenBonusPanel;
    }

    void SetCurrencyOnStart(int amount)
    {
        currencyInBank = amount;
        PlayerPrefs.SetInt("Currency", currencyInBank);
        SetCurrencyText();
    }

    public static int GetStackedBonus()
    {
        return stackedBonus;
    }

    public static void ResetStackedBonus()
    {
        stackedBonus = 0;
    }

    public void GiveBonus(int bonus, bool isPurchase = false)
    {
        if (bonus <= 0) { return; }
        currencyInBank += bonus;
        PlayerPrefs.SetInt("Currency", currencyInBank);
        SetCurrencyText(); 

        if (isPurchase && storePanel != null) // this case handles times when a purchase is made in the store, and we want to be sure that the currency indicator on main ui is updated properly if multiple purchases are made before leaving store
        {
            stackedBonus += bonus;
            //Debug.Log("Stacked Bonus Regular is " + stackedBonusRegular);
            storePanel.ShowThankYou();
        }
        else // this case handles all other times a bonus is given; i.e. when stacking is not a problem
        {
            if (OnCurrencyAdded != null) { OnCurrencyAdded(bonus); }
            else { Debug.Log("delegate is null"); }
        }
        canOpenBonusPanel = false;
    }

#if UNITY_EDITOR // disable cheats in builds
    public void ClearCurrency()
    {
        currencyInBank = 0;
        PlayerPrefs.SetInt("Currency", currencyInBank);
        SetCurrencyText();
    }
#endif

    public static void SetCurrencyText()
    {

        Instance.currencyTextSilver.text = currencyInBank.ToString();
    }

    public static void SetCurrencyInBank(int amount)
    {
        currencyInBank = amount;
        PlayerPrefs.SetInt("Currency", currencyInBank);
        SetCurrencyText();
    }

    public static void WithdrawAmount(int amount)
    {
        SetCurrencyInBank(currencyInBank - amount);
    }

    public static bool CanWithdrawAmount(int amount)
    {
        if (amount <= currencyInBank)
        {
            return true;
        }
        return false;
    }
}
