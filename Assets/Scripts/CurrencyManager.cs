using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour {

    public static CurrencyManager instance;

    public GameObject bonusPanel;
    public Text currencyText;

    private int dailyBonusAmount = 10;

    public static int currencyInBank = 0;

	// Use this for initialization
	void Start ()
    {
        instance = this;

        if (PlayerPrefs.GetString("FirstRun") != "False")
        {
            AddCurrencyOnStart(100);
            PlayerPrefs.SetString("FirstRun", "False");
        }

        currencyInBank = PlayerPrefs.GetInt("Currency");
        CloseBonusPanel();
        SetCurrencyText();
        DateTime currentDateTime = System.DateTime.Now;
        int currentDayOfYear = currentDateTime.DayOfYear;
        int currentYear = currentDateTime.Year;

        int storedDayOfYear = PlayerPrefs.GetInt("Day");
        int storedYear = PlayerPrefs.GetInt("Year");

        if (currentDayOfYear >  storedDayOfYear && currentYear >= storedYear)
        {
            if (PlayerPrefs.GetString("FirstRun") != "False") {
                GiveBonus(dailyBonusAmount);
            }  
        }

        PlayerPrefs.SetInt("Day", currentDayOfYear);
        PlayerPrefs.SetInt("Year", currentYear);
    }
	
    void AddCurrencyOnStart(int amount)
    {
        PlayerPrefs.SetInt("Currency", amount);
    }

	void GiveBonus(int bonus)
    {
        bonusPanel.SetActive(true);
        currencyInBank += bonus;
        PlayerPrefs.SetInt("Currency", currencyInBank);
        SetCurrencyText();
    }

    public void CloseBonusPanel()
    {
        bonusPanel.SetActive(false);
    }

    public static void SetCurrencyText()
    {

        instance.currencyText.text = currencyInBank.ToString();
    }

    public static void SetCurrencyInBank(int amount)
    {
        currencyInBank = amount;
        PlayerPrefs.SetInt("Currency", currencyInBank);
        SetCurrencyText();
    }

    public static bool WithdrawAmount(int amount)
    {
        if (amount <= currencyInBank)
        {
            SetCurrencyInBank(currencyInBank - amount);
            return true;
        }
        return false;
    }
}
