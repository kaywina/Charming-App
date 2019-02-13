using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmsPanel : MonoBehaviour
{
    public GameObject worldSpaceUI;
    public GameObject standIcons;


    protected void OnEnable()
    {
        Debug.Log("On enable in CharmsPanel");

        if (worldSpaceUI != null)
        {
            worldSpaceUI.SetActive(false);
        }

        if (standIcons != null)
        {
            standIcons.SetActive(false);
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
    }
}
