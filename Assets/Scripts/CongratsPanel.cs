using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CongratsPanel : CharmsPanel
{
    public Text charmText; 
    public List<GameObject> rigModels;
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

    public void ShowPanel()
    {
        charmText.text = unlocked.name;
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
            }
            else
            {
                rigModels[i].SetActive(false);
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
            }
        }
    }
}
