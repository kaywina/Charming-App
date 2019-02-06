using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CongratsPanel : CharmsPanel
{
    public LocalizationText charmText;

    // this is janky, but make sure these lists match up
    public List<GameObject> rigModels;
    public List<GameObject> fireworks;


    private GameObject unlocked;
    public GameObject headerControls;
    public GameObject standIcons;

    new void OnEnable()
    {
        headerControls.SetActive(false);
        standIcons.SetActive(false);

        base.OnEnable();
    }

    new void OnDisable()
    {
        DisableRigModels();
        if (headerControls != null) { headerControls.SetActive(true); }
        if (standIcons != null) { standIcons.SetActive(true); }
        base.OnDisable();
    }

    public void ShowPanel(bool isCharm)
    {
        charmText.SetLocalizationKey(unlocked.name.ToUpper());
        SetCharmRig(unlocked.name);
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }

    public void SetUnlockedObject(GameObject toSet)
    {
        unlocked = toSet;
    }

    public void SetCharmRig(string nameOfCharm)
    {
        for (int i = 0; i < rigModels.Count; i++)
        {
            if (rigModels[i].name == nameOfCharm)
            {
                rigModels[i].SetActive(true);
                fireworks[i].SetActive(true);
            }
            else
            {
                rigModels[i].SetActive(false);
                fireworks[i].SetActive(false);
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
                fireworks[i].SetActive(false);
            }
        }
    }
}
