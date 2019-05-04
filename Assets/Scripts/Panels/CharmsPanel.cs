using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmsPanel : MonoBehaviour
{
    public GameObject worldSpaceUI;
    public GameObject standIcons;
    public GameObject headerControls;
    public GameObject arrowControls;
    public bool returnToMain = true;
    public GameObject[] setActiveOnEnable;

    protected void OnEnable()
    {
        if (worldSpaceUI != null)
        {
            worldSpaceUI.SetActive(false);
        }

        if (standIcons != null)
        {
            standIcons.SetActive(false);
        }

        if (headerControls != null)
        {
            headerControls.SetActive(false);
        }

        if (arrowControls != null)
        {
            arrowControls.SetActive(false);
        }

        ActivateObjects();
    }

    protected void OnDisable()
    {
        if (!returnToMain) { return; }

        if (worldSpaceUI != null)
        {
            worldSpaceUI.SetActive(true);
        }

        if (standIcons != null)
        {
            standIcons.SetActive(true);
        }

        if (headerControls != null)
        {
            headerControls.SetActive(true);
        }

        if (arrowControls != null)
        {
            arrowControls.SetActive(true);
        }

        DeactivateObjects();
        
    }

    public void ActivateObjects()
    {
        for (int i = 0; i < setActiveOnEnable.Length; i++)
        {
            setActiveOnEnable[i].SetActive(true);
        }
    }

    public void DeactivateObjects()
    {
        for (int i = 0; i < setActiveOnEnable.Length; i++)
        {
            setActiveOnEnable[i].SetActive(false);
        }
    }

    public void SetReturnToMain (bool toSet)
    {
        returnToMain = toSet;
    }
    // hmmm
    public void DisableCharmsPanel()
    {
        gameObject.SetActive(false);
    }
}
