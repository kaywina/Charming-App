using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmsPanel : MonoBehaviour
{
    public GameObject worldSpaceUI;

    protected void OnEnable()
    {
        if (worldSpaceUI != null)
        {
            worldSpaceUI.SetActive(false);
        }
    }

    protected void OnDisable()
    {
        if (worldSpaceUI != null)
        {
            worldSpaceUI.SetActive(true);
        }
    }
}
