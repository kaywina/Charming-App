using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour {

    public GameObject bonusPanel;
    public Text currencyText;

    private int dailyBonusAmount = 10;

    public static int currencyInBank = 0;
    private int storedDayOfYear = 0;
    private int storedYear = 0;

	// Use this for initialization
	void Start ()
    {
        currencyInBank = PlayerPrefs.GetInt("Currency");
        CloseBonusPanel();
        SetCurrencyText();
        DateTime currentDateTime = System.DateTime.Now;
        int currentDayOfYear = currentDateTime.DayOfYear;
        int currentYear = currentDateTime.Year;

        if (currentDayOfYear > PlayerPrefs.GetInt("Day") && currentYear >= PlayerPrefs.GetInt("Year"))
        {
            GiveBonus();
        }

        PlayerPrefs.SetInt("Day", currentDayOfYear);
        PlayerPrefs.SetInt("Year", currentYear);
    }
	
	void GiveBonus()
    {
        bonusPanel.SetActive(true);
        currencyInBank += dailyBonusAmount;
        PlayerPrefs.SetInt("Currency", currencyInBank);
        SetCurrencyText();
    }

    public void CloseBonusPanel()
    {
        bonusPanel.SetActive(false);
    }

    public void SetCurrencyText()
    {
        currencyText.text = currencyInBank.ToString();
    }
}
