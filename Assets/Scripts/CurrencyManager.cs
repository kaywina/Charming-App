using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour {

    public static CurrencyManager Instance;

    public GameObject welcomePanel;
    public GameObject infoPanel;
    public GameObject bonusPanel;
    public StorePanel storePanel;
    public Text currencyText;

    private static int welcomeBonusSilver = 32;
    public Text welcomeBonusText;

    private static int currencyInBank = 0;

    public delegate void CurrencyAddedAction(int n);
    public static event  CurrencyAddedAction OnCurrencyAdded;

    private static int stackedBonus = 0; // to handle multiple store purchases before leaving store

    private bool canOpenBonusPanel = false;

    public static string currencyPlayerPref = "Currency";

    public CurrencyIndicator currencyIndicator;

    // Use this for initialization
    void Start ()
    {
        Instance = this;

        welcomeBonusText.text = welcomeBonusSilver.ToString();
        canOpenBonusPanel = SetCanOpenBonusPanel();

        // on first time running app
        if (!PlayerPrefs.GetString("FirstRun").Equals("False"))
        {
            //Debug.Log("Give currency bonus on first run");
            SetCurrencyOnStart(welcomeBonusSilver);
            welcomePanel.SetActive(true);
        }
        else
        {
            currencyInBank = PlayerPrefs.GetInt(currencyPlayerPref);
            infoPanel.SetActive(true);
        }

        SetCurrencyText();
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

    public void GiveBonus(int bonus, bool isPurchase = false, bool isDailyBonus = false)
    {
        if (bonus <= 0) { return; }
        currencyInBank += bonus;
        PlayerPrefs.SetInt(currencyPlayerPref, currencyInBank);
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
        SetCurrencyText();
    }
#endif

    public static void SetCurrencyText()
    {

        Instance.currencyText.text = currencyInBank.ToString();
    }

    public static void SetCurrencyInBank(int amount)
    {
        currencyInBank = amount;
        PlayerPrefs.SetInt(currencyPlayerPref, currencyInBank);
        SetCurrencyText();
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
            Instance.StartIndicatorFlashing();
            return false;
        }
    }

    private int flashCount = 0;
    private int maxFlashes = 4;

    private void StartIndicatorFlashing()
    {
        CancelInvoke("ResetToWhite");

        float repeatRate = 0.5f;
        InvokeRepeating("FlashIndicatorYellow", 0, repeatRate);
        InvokeRepeating("ResetToWhite", repeatRate * 0.5f, repeatRate);
    }

    private void FlashIndicatorYellow()
    {
        //Debug.Log("Flash indicator yellow");

        if (flashCount >= maxFlashes)
        {
            CancelInvoke("FlashIndicatorYellow");
            CancelInvoke("ResetToWhite");
            flashCount = 0;
            //Debug.Log("Stop flashing indicator");
            return;
        }

        currencyText.color = Color.yellow;
        flashCount++;
    }

    private void ResetToWhite()
    {
        //Debug.Log("Reset indicator ot white");
        currencyText.color = Color.white;
    }

    public static bool CanWithdrawAmount(int amount)
    {
        if (amount <= currencyInBank)
        {
            return true;
        }
        return false;
    }

    public void ShowBonusIndicator(int amount)
    {
        currencyIndicator.ShowBonusTextCustomAmount(amount);
    }
}
