using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmsPanel : MonoBehaviour
{
    public GameObject worldSpaceUI;
    public GameObject standIcons;
    public GameObject headerControls;
    public GameObject footerControls;


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

        if (footerControls != null)
        {
            footerControls.SetActive(false);
        }

    }

    protected void OnDisable()
    {
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

        if (footerControls != null)
        {
            footerControls.SetActive(true);
        }
    }
}
