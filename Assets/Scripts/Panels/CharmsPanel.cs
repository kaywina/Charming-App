using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharmsPanel : MonoBehaviour
{
    public GameObject worldUIMask;
    public GraphicRaycaster worldCanvasRaycaster;
    public GameObject charmText;
    public GameObject headerControls;
    public GameObject arrowControls;
    public bool returnToMain = true;
    public GameObject[] setActiveOnEnable;

    protected void OnEnable()
    {
        if (charmText != null)
        {
            charmText.SetActive(false);
        }

        if (worldUIMask != null)
        {
            worldUIMask.SetActive(true);
        }

        if (worldCanvasRaycaster != null)
        {
            worldCanvasRaycaster.enabled = false;
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

        if (charmText != null)
        {
            charmText.SetActive(true);
        }

        if (worldUIMask != null)
        {
            worldUIMask.SetActive(false);
        }

        if (worldCanvasRaycaster != null)
        {
            worldCanvasRaycaster.enabled = true;
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
            if (setActiveOnEnable[i] != null) { setActiveOnEnable[i].SetActive(true);  }
        }
    }

    public void DeactivateObjects()
    {
        for (int i = 0; i < setActiveOnEnable.Length; i++)
        {
            if (setActiveOnEnable[i] != null) { setActiveOnEnable[i].SetActive(false);  }
        }
    }

    public void SetReturnToMain (bool toSet)
    {
        returnToMain = toSet;
    }

    public void DisableCharmsPanel()
    {
        gameObject.SetActive(false);
    }
}
