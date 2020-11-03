using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : CharmsPanel
{
    bool respectPlayerPref = true;
    public GoToBonusPanelButton goToBonusPanelButton;

    /*
    public GameObject welcomeBonusIndicator;
    public GameObject alternativeIndicator; // collect keys text
    */

    new void OnEnable()
    {
        /*
        // only show the welcome bonus indicator on first time running the app; but always show the welcome screen if tutorial is enabled
        if (CurrencyManager.IsFirstRun())
        {
            welcomeBonusIndicator.SetActive(true);
            alternativeIndicator.SetActive(false);
        }
        else
        {
            welcomeBonusIndicator.SetActive(false);
            alternativeIndicator.SetActive(true);
        }
        */

        // if tutorial is not enabled, then try to open the bonus panel
        if (PlayerPrefs.GetString("ShowInfo") == "false" && respectPlayerPref)
        {
            gameObject.SetActive(false);
            respectPlayerPref = true; // set it back to true after disrespecting playerpref once
            goToBonusPanelButton.TryOpenBonusPanel();
            return;
        }

        base.OnEnable();
    }

    // this is used for the ? button in options to show the tutorial even if it has been toggled off on start
    public void DoNotRespectPlayerPrefThisTime ()
    {
        respectPlayerPref = false;
    }
}
