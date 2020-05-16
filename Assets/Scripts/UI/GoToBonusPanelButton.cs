using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class GoToBonusPanelButton : MonoBehaviour
{
    public CurrencyManager currencyManager;
    public GameObject bonusPanel;
    public GameObject infoPanel;

    public SetPlayerPrefFromToggle bonusWheelToggle;

    public void TryOpenBonusPanel()
    {
        // we show the bonus wheel every new day
        bool canOpen = currencyManager.GetCanOpenBonusPanel();

        // this falg allows the user to choose whether or not have bonus wheel appear on start with a toggle
        bool shouldOpen = true;
        if (PlayerPrefs.HasKey(bonusWheelToggle.GetPlayerPrefName())) {
            if (PlayerPrefs.GetString(bonusWheelToggle.GetPlayerPrefName()) == "false")
            {
                shouldOpen = false;
            }
        }

        // if we want to should the bonus wheel
        if (canOpen && shouldOpen)
        {
            CharmsPanel charmsPanel = infoPanel.GetComponent<CharmsPanel>();
            charmsPanel.DeactivateObjects();
            bonusPanel.SetActive(true);
            infoPanel.SetActive(false);
        }

        // otherwise we don't show the bonus wheel
        else
        {
            CharmsPanel charmsPanel = infoPanel.GetComponent<CharmsPanel>();
            charmsPanel.SetReturnToMain(true);
            charmsPanel.DisableCharmsPanel();
        }
    }
}
