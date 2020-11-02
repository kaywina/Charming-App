using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : CharmsPanel
{
    bool respectPlayerPref = true; // this is used for the ? button in options
    public GoToBonusPanelButton goToBonusPanelButton;

    new void OnEnable()
    {
        if (PlayerPrefs.GetString("ShowInfo") == "false" && respectPlayerPref)
        {
            gameObject.SetActive(false);
            respectPlayerPref = true; // set it back to true after disrespecting playerpref once
            goToBonusPanelButton.TryOpenBonusPanel();
            return;
        }

        base.OnEnable();
    }

    public void DoNotRespectPlayerPrefThisTime ()
    {
        respectPlayerPref = false;
    }
}
