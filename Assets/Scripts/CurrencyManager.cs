using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour {

    public static CurrencyManager Instance;

    public GameObject welcomePanel;
    public GameObject bonusPanel;
    public Text currencyText;

    private static int welcomeBonus = 10;
    public Text welcomeBonusText;
    private int dailyBonus = 16;

    public static int currencyInBank = 0;

	// Use this for initialization
	void Start ()
    {
        Instance = this;

        CloseBonusPanel();
        CloseWelcomePanel();

        welcomeBonusText.text = welcomeBonus.ToString();

        // on first time running app
        if (PlayerPrefs.GetString("FirstRun") != "False")
        {
            //Debug.Log("Give currency bonus on first run");
            SetCurrencyOnStart(welcomeBonus);
        }
        else
        {
            currencyInBank = PlayerPrefs.GetInt("Currency");
            SetCurrencyText();
        }

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

    void SetCurrencyOnStart(int amount)
    {
        currencyInBank = amount;
        PlayerPrefs.SetInt("Currency", currencyInBank);
        welcomePanel.SetActive(true);
        SetCurrencyText();
    }

	public void OpenBonusPanel()
    {
        bonusPanel.SetActive(true);
    }

    public void GiveBonus(int bonus)
    {
        currencyInBank += bonus;
        PlayerPrefs.SetInt("Currency", currencyInBank);
        SetCurrencyText();
    }

    public void ClearCurrency()
    {
        currencyInBank = 0;
        PlayerPrefs.SetInt("Currency", currencyInBank);
        SetCurrencyText();
    }

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

        Instance.currencyText.text = currencyInBank.ToString();
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
