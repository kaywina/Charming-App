using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CongratsPanel : CharmsPanel
{
    public GameObject shareBonusIndicator;

    // this is janky, but make sure these lists match up
    public List<GameObject> rigModels;
    public List<GameObject> fireworks;

    public GameObject pillar;
    public GameObject charmButtons;

    private GameObject unlocked;

    public ShareScreenshotAndroid shareScreenshotAndroid;

    new void OnEnable()
    {
        base.OnEnable();
        shareBonusIndicator.SetActive(true);
        pillar.SetActive(true); // do this after calling base.OnEnable to re-enable
        charmText.SetActive(true);
        charmButtons.SetActive(false);
        worldUI.SetActive(true);
    }

    new void OnDisable()
    {
        pillar.SetActive(false);
        charmText.SetActive(false); // do this before base.OnEnable to avoid disabling on main UI
        DisableRigModels();
        base.OnDisable();
        charmButtons.SetActive(true);

    }

    public void ShowPanel(bool isCharm)
    {
        SetRig(unlocked.name);
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        if (shareScreenshotAndroid != null && shareScreenshotAndroid.bonusGiven == true)
        {
            CurrencyManager.Instance.GiveBonus(shareScreenshotAndroid.givenBonusAmount);
        }
        gameObject.SetActive(false);
    }

    public void SetUnlockedObject(GameObject toSet)
    {
        unlocked = toSet;
    }

    public void SetRig(string nameOfCharm)
    {
        for (int i = 0; i < rigModels.Count; i++)
        {
            if (rigModels[i].name == nameOfCharm)
            {
                rigModels[i].SetActive(true);
                //fireworks[i].SetActive(true);
            }
            else
            {
                rigModels[i].SetActive(false);
                //fireworks[i].SetActive(false);
            }
        }
    }

    public void DisableRigModels()
    {
        for (int i = 0; i < rigModels.Count; i++)
        {
            if (rigModels[i] != null)
            {
                rigModels[i].SetActive(false);
                //fireworks[i].SetActive(false);
            }
        }
    }
}
