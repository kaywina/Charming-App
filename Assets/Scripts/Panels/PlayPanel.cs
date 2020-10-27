using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPanel : CharmsPanel
{
    public GameObject overlayControls; // in this case we need the canvasOverlay active to show the currency indicator, so that is whey we have this explicit reference to the Controls scene object
    public GameObject arrowControls;
    public GameObject gameSelectMenu;
    public PlayManager playManager;

    new void OnEnable()
    {
        base.OnEnable();
        overlayControls.SetActive(false);
        arrowControls.SetActive(false);
        canvasOverlay.SetActive(true); // inherited
        gameSelectMenu.SetActive(true);
        playManager.SetupGameSelect();
}

    new void OnDisable()
    {
        base.OnDisable();
        overlayControls.SetActive(true);
        arrowControls.SetActive(true);
        canvasOverlay.SetActive(true); // inherited
    }
}
