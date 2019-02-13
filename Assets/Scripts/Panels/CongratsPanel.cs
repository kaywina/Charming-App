using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CongratsPanel : CharmsPanel
{
    public LocalizationText unlockText;

    // this is janky, but make sure these lists match up
    public List<GameObject> rigModels;
    public List<GameObject> fireworks;

    private GameObject unlocked;

    new void OnDisable()
    {
        DisableRigModels();
        base.OnDisable();
    }

    public void ShowPanel(bool isCharm)
    {
        unlockText.SetLocalizationKey(unlocked.name.ToUpper());
        SetRig(unlocked.name);
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

    public void SetRig(string nameOfCharm)
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
