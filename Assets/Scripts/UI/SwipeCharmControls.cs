using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCharmControls : SwipeFunction
{
    public GameObject[] charmSets;
    public GameObject[] unlockSets;
    public GameObject[] arrowButtons;

    private int charmSet;

    new void Start()
    {
        base.Start();
        charmSet = 0;
    }

    new void OnEnable()
    {
        base.OnEnable();
        charmSet = Charms.GetCharmSet();
    }

    public override void SwipeLeft()
    {
        // go to previous charm set if possible otherwise do nothing
        //Debug.Log("Swipe Left");
        if (charmSet == 0)
        {
            //Debug.Log("Go to charm set 1");
            DeactivateObjects(charmSet);
            charmSet = 1;
            ActivateObjects(charmSet);
            PlayerPrefs.SetInt(Charms.GetCharmSetPlayerPrefName(), charmSet);
        }
    }

    public override void SwipeRight()
    {
        // go to next charm set if possible otherwise do nothing
        //Debug.Log("Swipe Right");
        if (charmSet == 1)
        {
            //Debug.Log("Go to charm set 0");
            DeactivateObjects(charmSet);
            charmSet = 0;
            ActivateObjects(charmSet);
            PlayerPrefs.SetInt(Charms.GetCharmSetPlayerPrefName(), charmSet);
        }
    }

    private void DeactivateObjects(int index)
    {
        charmSets[charmSet].SetActive(false);
        unlockSets[charmSet].SetActive(false);
        arrowButtons[charmSet].SetActive(false);
    }

    private void ActivateObjects(int index)
    {
        charmSets[charmSet].SetActive(true);
        unlockSets[charmSet].SetActive(true);
        arrowButtons[charmSet].SetActive(true);
    }
}