using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmSetButton : EnableDisableObjects
{
    public int setToOpen = 0;

    // Update is called once per frame
    public void OpenCharmSet()
    {
        base.EnableDisableGameObjects();
        PlayerPrefs.SetInt(Charms.GetCharmSetPlayerPrefName(), setToOpen);
    }
}
