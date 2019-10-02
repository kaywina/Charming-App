using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePanel : CharmsPanel
{

    private bool fromUnlock = false;
    public GameObject unlockPanel;
    public GameObject thanks;

    new void OnEnable()
    {
        thanks.SetActive(false);

        base.OnEnable();

        if (fromUnlock)
        {
            unlockPanel.SetActive(false);
            returnToMain = false;
        }
    }

    new void OnDisable()
    {
        base.OnDisable();

        if (fromUnlock)
        {
            unlockPanel.SetActive(true);
            SetFromUnlock(false); // always reset this when leaving
            returnToMain = true;
        }
    }

    public void SetFromUnlock (bool toSet)
    {
        fromUnlock = toSet;
    }

    public void ShowThankYou()
    {
        thanks.SetActive(true);
    }
}
