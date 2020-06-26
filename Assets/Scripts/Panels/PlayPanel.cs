using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPanel : CharmsPanel
{
    public GameObject headerControls;
    public GameObject arrowControls;

    new void OnEnable()
    {
        base.OnEnable();
        headerControls.SetActive(false);
        arrowControls.SetActive(false);
        canvasOverlay.SetActive(true); // inherited
}

    new void OnDisable()
    {
        base.OnDisable();
        headerControls.SetActive(true);
        arrowControls.SetActive(true);
        canvasOverlay.SetActive(true); // inherited
    }
}
