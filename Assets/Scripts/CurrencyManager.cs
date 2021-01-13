using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour {

    public static CurrencyManager Instance;

    public GameObject tutorialPanel;
    public GameObject adsOptInPanel;
    public GameObject bonusPanel;

    private static int welcomeBonus = 32;
    public Text welcomeBonusText;

    private static int currencyInBank = 0;

    private static int stackedBonus = 0; // to handle multiple store purchases before leaving store

    private bool canOpenBonusPanel = false;

    public static string currencyPlayerPref = "Currency";

    // Use this for initialization
    void Start ()
    {
        Instance = this;

        welcomeBonusText.text = welcomeBonus.ToString();
        canOpenBonusPanel = SetCanOpenBonusPanel();

        // on first time running app
        if (!PlayerPrefs.GetString("FirstRun").Equals("False"))
        {
            //Debug.Log("Give currency bonus on first run");
            SetCurrencyOnStart(welcomeBonus);

            // Ads opt-in panel is deprecated with removal of Unity Ads
            // so we always show tutorial panel
            tutorialPanel.SetActive(true);

            /* This is no longer required
            if (UnityAdsController.IsAllowAdsSet())
            {
                tutorialPanel.SetActive(true);
            }
            else
            {
                adsOptInPanel.SetActive(true);
            }
            */
        }
        else
        {
            currencyInBank = PlayerPrefs.GetInt(currencyPlayerPref);

            // Ads opt-in panel is deprecated with removal of Unity Ads
            // so we always show tutorial panel
            tutorialPanel.SetActive(true);

            /* This is no longer required
            if (UnityAdsController.IsAllowAdsSet())
            {
                tutorialPanel.SetActive(true);
            }
            else
            {
                adsOptInPanel.SetActive(true);
            }
            */
        }
    }

    public static bool IsFirstRun()
    {
        if (!PlayerPrefs.GetString("FirstRun").Equals("False"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("FirstRun", "False");
    }

    public bool SetCanOpenBonusPanel()
    {
        if (TimeManager.IsNewDay(TimeManager.TimeType.DailySpin))
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

    public bool GetCanOpenBonusPanel()
    {
        return canOpenBonusPanel;
    }

    void SetCurrencyOnStart(int amount)
    {
        currencyInBank = PlayerPrefs.GetInt(currencyPlayerPref); // we do this to respect persistent data file (see DataManager class)
        currencyInBank += amount;
        PlayerPrefs.SetInt(currencyPlayerPref, currencyInBank);
    }

    public static int GetCurrencyInBank()
    {
        return currencyInBank;
    }

    public static int GetStackedBonus()
    {
        return stackedBonus;
    }

    public static void ResetStackedBonus()
    {
        stackedBonus = 0;
    }

    public void GiveBonus(int bonus, bool isPurchase = false, bool isDailyBonus = false)
    {
        if (bonus <= 0) { return; }
        currencyInBank += bonus;
        PlayerPrefs.SetInt(currencyPlayerPref, currencyInBank);

        if (isPurchase) // this case handles times when a purchase is made in the store, and we want to be sure that the currency indicator on main ui is updated properly if multiple purchases are made before leaving store
        {
            stackedBonus += bonus;
            //Debug.Log("Stacked Bonus Regular is " + stackedBonusRegular);
        }
        if (isDailyBonus) {
            canOpenBonusPanel = false;
            TimeManager.SetPrefsForDailySpin();
        }
    }

#if UNITY_EDITOR // disable cheats in builds
    public void ClearCurrency()
    {
        currencyInBank = 0;
        PlayerPrefs.SetInt(currencyPlayerPref, currencyInBank);
    }
#endif

    public static void SetCurrencyInBank(int amount)
    {
        currencyInBank = amount;
        PlayerPrefs.SetInt(currencyPlayerPref, currencyInBank);
    }

    public static bool WithdrawAmount(int amount)
    {
        if (currencyInBank >= amount)
        {
            SetCurrencyInBank(currencyInBank - amount);
            return true;
        }
        else
        {
            return false;
        }
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
