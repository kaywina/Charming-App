using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmsPanel : MonoBehaviour
{
    public GameObject worldSpaceUI;

    protected void OnEnable()
    {
        worldSpaceUI.SetActive(false);
    }

    protected void OnDisable()
    {
        worldSpaceUI.SetActive(true);
    }
}
