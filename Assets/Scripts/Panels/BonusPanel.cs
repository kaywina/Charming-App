using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusPanel : CharmsPanel
{
    public GameObject overlayControls;
    public BonusWheel bonusWheel;
    public GameObject[] deactivateOnReadySpin;
    public GameObject[] activateAfterSpin;
    public GameObject rewardedAdButton;
    public SetPlayerPrefFromToggle goldTogglePrefab;
    public Text prizeText;
    public GameObject strikeout;
    public GameObject doubleBonusText;
    public Text totalBonusText;
    public ParticleSystem fireworks;
    public GameObject skipButton;

    private bool hasSpun;
    private int storedBonus;

    new void OnEnable()
    {
        bonusWheel.gameObject.SetActive(true);
        StartCoroutine(Enable());
        rewardedAdButton.SetActive(false);
        skipButton.SetActive(false);
        base.OnEnable();
    }

    IEnumerator Enable()
    {
        strikeout.SetActive(false);
        doubleBonusText.SetActive(false);
        totalBonusText.gameObject.SetActive(false);

        overlayControls.SetActive(false);
        hasSpun = false;

        for (int i = 0; i < activateAfterSpin.Length; i++)
        {
            activateAfterSpin[i].SetActive(false);
        }

        for (int i = 0; i < deactivateOnReadySpin.Length; i++)
        {
            deactivateOnReadySpin[i].SetActive(true);
        }

        yield return new WaitForEndOfFrame();
    }

    new void OnDisable()
    {
        if (storedBonus != 0) // on in case where bonus wheel is not skipped
        {
            CurrencyManager.Instance.GiveBonus(storedBonus, false, true); // false because not a purchase, then true so that bonus wheel doesn't show again today
        }

        storedBonus = 0;
        if (overlayControls != null) { overlayControls.SetActive(true); }
        if (fireworks != null) { fireworks.Stop(); }
        base.OnDisable();
    }

    public void ReadySpin()
    {
        if (hasSpun) { return; }
        for (int i = 0; i < deactivateOnReadySpin.Length; i++)
        {
            deactivateOnReadySpin[i].SetActive(false);
        }
    }

    public void Spin()
    {
        if (hasSpun) { return; }
        if (fireworks != null) { fireworks.Play(); }
        bonusWheel.Spin();
        skipButton.SetActive(true);
    }

    public void SkipSpinWithTimeScale()
    {
        Time.timeScale = 100.0f;
        skipButton.SetActive(false);
    }

    public void CompleteSpin(int bonus)
    {
        skipButton.SetActive(false);
        Time.timeScale = 1f; // reset timescale in case user chose to use skip button
        
        //Debug.Log("Complete bonus wheel spin");
        storedBonus = bonus;
        prizeText.text = "+" + bonus.ToString();

        // get and store playerpref for gold subscribers
        bool isGold = false;
        if (PlayerPrefs.GetString(UnityIAPController.goldSubscriptionPlayerPref) == "true")
        {
            isGold = true;
        }

        // gold subscribers receive double bonus automatically;
        if (isGold)
        {
            //Debug.Log("Gold subscribers automatically get double bonus");
            rewardedAdButton.SetActive(false); //this needs to be done before the code below or the double bonus objects don't show up
            DoubleBonus();
            doubleBonusText.SetActive(true);
            strikeout.SetActive(true);
        }
        // if not gold show the rewarded ad button and ads are allowed
        else if (UnityAdsController.GetAllowAds())
        {
            rewardedAdButton.SetActive(true);
        }

        for (int i = 0; i < activateAfterSpin.Length; i++)
        {
            activateAfterSpin[i].SetActive(true);
        }

        bonusWheel.gameObject.SetActive(false);
        hasSpun = true;
    }

    public void DoubleBonus()
    {
        storedBonus = storedBonus * 2;
        totalBonusText.text = "+" + storedBonus.ToString();
        if (totalBonusText != null) { totalBonusText.gameObject.SetActive(true); }
    }

    public void SkipBonus()
    {
        //DeleteBonusWheelPlayerPrefs();
        gameObject.SetActive(false);
    }

    public int GetStoredBonus()
    {
        return storedBonus;
    }
}
