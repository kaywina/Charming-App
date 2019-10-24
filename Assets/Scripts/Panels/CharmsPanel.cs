using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharmsPanel : MonoBehaviour
{

    public GameObject worldUI;
    public GameObject standIcons;
    public GraphicRaycaster worldCanvasRaycaster;
    public BoxCollider pillarCollider;
    public GameObject charmText;
    public GameObject canvasOverlay;
    public bool returnToMain = true;
    public GameObject[] setActiveOnEnable;

    protected void OnEnable()
    {
        if (charmText != null)
        {
            charmText.SetActive(false);
        }
        if (worldUI != null)
        {
            worldUI.SetActive(false);
        }
        if (standIcons != null)
        {
            standIcons.SetActive(false);
        }
        if (worldCanvasRaycaster != null)
        {
            worldCanvasRaycaster.enabled = false;
        }

        if (pillarCollider != null)
        {
            pillarCollider.enabled = false;
        }

        if (canvasOverlay != null)
        {
            canvasOverlay.SetActive(false);
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

        if (worldUI != null)
        {
            worldUI.SetActive(true);
        }
        if (standIcons != null)
        {
            standIcons.SetActive(true);
        }

        if (worldCanvasRaycaster != null)
        {
            worldCanvasRaycaster.enabled = true;
        }

        if (pillarCollider != null)
        {
            pillarCollider.enabled = true;
        }

        if (canvasOverlay != null)
        {
            canvasOverlay.SetActive(true);
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
