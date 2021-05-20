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
    public CurrencyIndicator currencyIndicator;
    public GameObject rewardedAdButton;
    public SetPlayerPrefFromToggle goldTogglePrefab;
    public Text bonusAmountText;
    public GameObject doubleBonusLabelText;
    public Text doubleBonusAmountText;
    public ParticleSystem fireworks;
    public GameObject skipButton;
    public GameObject animatedArrow;
    public GameObject subscribePanel;

    private bool hasSpun;
    private int bonusAmount;
    private bool showAdOnDisable = true;

    new void OnEnable()
    {
        showAdOnDisable = true;
        bonusAmount = 0;
        bonusWheel.gameObject.SetActive(true);
        StartCoroutine(Enable());
        rewardedAdButton.SetActive(false);
        skipButton.SetActive(false);
        currencyIndicator.gameObject.SetActive(false);
        base.OnEnable();
    }

    IEnumerator Enable()
    {
        doubleBonusLabelText.SetActive(false);
        doubleBonusAmountText.gameObject.SetActive(false);

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
        if (showAdOnDisable) { GoogleMobileAdsController.ShowInterstitialAd(); }
        if (overlayControls != null) { overlayControls.SetActive(true); }
        if (fireworks != null) { fireworks.Stop(); }
        base.OnDisable();
    }

    public void SetShowAdOnDisable(bool toSet)
    {
        showAdOnDisable = toSet;
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
        animatedArrow.SetActive(false);
        GoogleMobileAdsController.ShowBannerAd();
    }

    public void SkipSpinWithTimeScale()
    {
        Time.timeScale = 100.0f;
        skipButton.SetActive(false);
    }

    public void CompleteSpin(int bonus)
    {
        GoogleMobileAdsController.HideBannerAd();
        skipButton.SetActive(false);
        Time.timeScale = 1f; // reset timescale in case user chose to use skip button
        
        //Debug.Log("Complete bonus wheel spin");
        bonusAmount = bonus;
        bonusAmountText.text = "+" + bonus.ToString();

        // get and store playerpref for gold subscribers
        bool isGold = false;
        if (UnityIAPController.IsGold())
        {
            isGold = true;
        }

        // gold subscribers receive double bonus automatically;
        if (isGold)
        {
            //Debug.Log("Gold subscribers automatically get double bonus");
            rewardedAdButton.SetActive(false); //this needs to be done before the code below or the double bonus objects don't show up
            GiveDoubleBonus();
            doubleBonusLabelText.SetActive(true);
        }
        // if not gold show the rewarded ad button and ads are allowed
        /* Unity Ads have been disabled
        else if (UnityAdsController.GetAllowAds())
        {
            rewardedAdButton.SetActive(true);
        }
        */

        for (int i = 0; i < activateAfterSpin.Length; i++)
        {
            activateAfterSpin[i].SetActive(true);
        }

        bonusWheel.gameObject.SetActive(false);
        hasSpun = true;

        // order matters here; first we activate the currency indicator so it has old number of keys, then we give the bonus, then we update the indicator so it shows keys being added
        currencyIndicator.gameObject.SetActive(true);
        CurrencyManager.Instance.GiveBonus(bonusAmount, false, true); // false because not a purchase, then true so that bonus wheel doesn't show again today
        currencyIndicator.UpdateIndicatorAnimated();
    }

    public void GiveDoubleBonus()
    {
        // give the bonus again and update relevant UI
        CurrencyManager.Instance.GiveBonus(bonusAmount, false, false); // passing false as second parameter this time since we only need to do that once, and it has already been done when spin was completed
        doubleBonusAmountText.text = "+" + (bonusAmount * 2).ToString();
        if (bonusAmountText != null) { bonusAmountText.gameObject.SetActive(false); }
        if (doubleBonusAmountText != null) { doubleBonusAmountText.gameObject.SetActive(true); }
    }

    public int GetStoredBonus()
    {
        return bonusAmount;
    }

    public void LeaveBonusPanel()
    {
        if (UnityIAPController.IsGold())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
            subscribePanel.SetActive(true);
        }
    }
}
