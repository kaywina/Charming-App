using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour {

    public static CurrencyManager Instance;

    public GameObject welcomePanel;
    public GameObject bonusPanel;
    public Text currencyTextSilver;
    public Text currencyTextGold;

    private static int welcomeBonusSilver = 10;
    private static int welcomeBonusGold = 0;
    public Text welcomeBonusText;

    private static int currencyInBankSilver = 0;
    private static int currencyInBankGold = 0;

	// Use this for initialization
	void Start ()
    {
        Instance = this;

        CloseBonusPanel();
        CloseWelcomePanel();

        welcomeBonusText.text = welcomeBonusSilver.ToString();

        // on first time running app
        if (PlayerPrefs.GetString("FirstRun") != "False")
        {
            //Debug.Log("Give currency bonus on first run");
            SetCurrencyOnStart(welcomeBonusSilver, welcomeBonusGold);
        }
        else
        {
            currencyInBankSilver = PlayerPrefs.GetInt("Currency");
            currencyInBankGold = PlayerPrefs.GetInt("PremiumCurrency");
        }

        SetCurrencyText();
        DateTime currentDateTime = System.DateTime.Now;
        int currentDayOfYear = currentDateTime.DayOfYear;
        int currentYear = currentDateTime.Year;

        int storedDayOfYear = PlayerPrefs.GetInt("Day");
        int storedYear = PlayerPrefs.GetInt("Year");

        
        // not first time running app then daily bonus applies
        if (PlayerPrefs.GetString("FirstRun") == "False")
        {
            if (currentDayOfYear > storedDayOfYear && currentYear >= storedYear)
            {
                OpenBonusPanel();
            }
        }
        PlayerPrefs.SetInt("Day", currentDayOfYear);
        PlayerPrefs.SetInt("Year", currentYear);
        PlayerPrefs.SetString("FirstRun", "False");
    }

    private void Update()
    {
#if UNITY_EDITOR // disable cheats in builds
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            CurrencyManager.Instance.GiveBonus(100);
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
        welcomePanel.SetActive(true);
        SetCurrencyText();
    }

	public void OpenBonusPanel()
    {
        bonusPanel.SetActive(true);
    }

    public void GiveBonus(int bonus)
    {
        currencyInBankSilver += bonus;
        PlayerPrefs.SetInt("Currency", currencyInBankSilver);
        SetCurrencyText();
    }

    public void GivePremiumBonus(int bonus)
    {
        currencyInBankGold += bonus;
        PlayerPrefs.SetInt("PremiumCurrency", currencyInBankGold);
        SetCurrencyText();
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

    public void CloseBonusPanel()
    {
        bonusPanel.SetActive(false);
    }

    public void CloseWelcomePanel()
    {
        welcomePanel.SetActive(false);
    }

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
