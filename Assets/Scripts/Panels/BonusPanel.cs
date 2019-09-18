using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusPanel : CharmsPanel
{
    public GameObject header;
    public BonusWheel bonusWheel;
    public GameObject[] deactivateOnReadySpin;
    public GameObject[] activateAfterSpin;
    public Text prizeText;
    public GameObject strikeoutText;
    public GameObject[] regularCurrencyImages;
    public GameObject[] premiumCurrencyImages;
    public GameObject watchedRewardedAdText;
    public Text totalBonusText;

    private bool hasSpun;
    private int storedBonus;
    private bool prizeIsPremium;

    new void OnEnable()
    {
        StartCoroutine(Enable());
        base.OnEnable();
    }

    IEnumerator Enable()
    {
        watchedRewardedAdText.SetActive(false);
        header.SetActive(false);
        strikeoutText.SetActive(false);
        totalBonusText.gameObject.SetActive(false);
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
            if (prizeIsPremium)
            {
                CurrencyManager.Instance.GivePremiumBonus(storedBonus);
            }
            else
            {
                CurrencyManager.Instance.GiveRegularBonus(storedBonus);
            }
        }

        storedBonus = 0;
        header.SetActive(true);
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
        bonusWheel.Spin();
    }

    public void CompleteSpin(int bonus, bool premiumPrize)
    {
        prizeIsPremium = premiumPrize;

        // enable the correct images for regular or premium currency
        for (int i = 0; i < premiumCurrencyImages.Length; i++)
        {
            premiumCurrencyImages[i].SetActive(prizeIsPremium);
            regularCurrencyImages[i].SetActive(!prizeIsPremium);
        }

        storedBonus = bonus;
        //Debug.Log("Complete bonus wheel spin");
        prizeText.text = bonus.ToString();
        for (int i = 0; i < activateAfterSpin.Length; i++)
        {
            activateAfterSpin[i].SetActive(true);
        }

        bonusWheel.gameObject.SetActive(false);

        hasSpun = true;
    }

    public void DoubleBonus()
    {
        strikeoutText.SetActive(true);
        storedBonus = storedBonus * 2;
        totalBonusText.text = storedBonus.ToString();
        totalBonusText.gameObject.SetActive(true);
    }

    public void SkipBonus()
    {
        PlayerPrefs.DeleteKey("Day");
        PlayerPrefs.DeleteKey("Year");
        gameObject.SetActive(false);
    }
}
