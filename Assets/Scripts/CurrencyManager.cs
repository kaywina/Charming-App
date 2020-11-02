using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour {

    public static CurrencyManager Instance;

    public GameObject infoPanel;
    public GameObject optInPanel;
    public GameObject bonusPanel;
    public StorePanel storePanel;

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
            if (UnityAdsController.IsAllowAdsSet())
            {
                infoPanel.SetActive(true);
            }
            else
            {
                optInPanel.SetActive(true);
            }
        }
        else
        {
            currencyInBank = PlayerPrefs.GetInt(currencyPlayerPref);
            if (UnityAdsController.IsAllowAdsSet())
            {
                infoPanel.SetActive(true);
            }
            else
            {
                optInPanel.SetActive(true);
            }
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

        if (isPurchase && storePanel != null) // this case handles times when a purchase is made in the store, and we want to be sure that the currency indicator on main ui is updated properly if multiple purchases are made before leaving store
        {
            stackedBonus += bonus;
            //Debug.Log("Stacked Bonus Regular is " + stackedBonusRegular);
            storePanel.ShowThankYou();
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
