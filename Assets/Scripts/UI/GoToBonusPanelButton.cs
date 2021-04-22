using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBonusPanelButton : MonoBehaviour
{
    public CurrencyManager currencyManager;
    public GameObject bonusPanel;
    public GameObject tutorialPanel;
    public GameObject subscribePanel;

    private string playerPrefName = "ShowBonus"; // don't change this in production!

    public void TryOpenBonusPanel()
    {
        // we show the bonus wheel every new day
        bool canOpen = currencyManager.GetCanOpenBonusPanel();

        // this flag allows the user to choose whether or not have bonus wheel appear on start with a toggle
        bool shouldOpen = true;
        if (PlayerPrefs.HasKey(playerPrefName)) {
            if (PlayerPrefs.GetString(playerPrefName) == "false")
            {
                shouldOpen = false;
            }
        }

        // if we want to should the bonus wheel
        if (canOpen && shouldOpen)
        {
            CharmsPanel charmsPanel = tutorialPanel.GetComponent<CharmsPanel>();
            charmsPanel.DeactivateObjects();
            tutorialPanel.SetActive(false);
            bonusPanel.SetActive(true);
        }

        // otherwise we don't show the bonus wheel, and show ad + subscrbie panel instead
        else
        {
            //Debug.Log("Go directly to main UI");
            GoogleMobileAdsController.ShowInterstitialAd();

            if (UnityIAPController.IsGold())
            {
                CharmsPanel charmsPanel = tutorialPanel.GetComponent<CharmsPanel>();
                charmsPanel.SetReturnToMain(true);
                charmsPanel.DisableCharmsPanel();
            }
            else
            {
                tutorialPanel.SetActive(false);
                subscribePanel.SetActive(true);
            }
        }
    }
}
