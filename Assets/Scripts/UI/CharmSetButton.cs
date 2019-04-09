using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmSetButton : EnableDisableObjects
{
    public int setToOpen = 0;

    void Start()
    {
        // ensure buttons are deactivated correctly when app starts up
        if (setToOpen == PlayerPrefs.GetInt(Charms.GetCharmSetPlayerPrefName())) {
            gameObject.SetActive(false);
        }
    }

    public void OpenCharmSet()
    {
        base.EnableDisableGameObjects();
        PlayerPrefs.SetInt(Charms.GetCharmSetPlayerPrefName(), setToOpen);
    }
}
