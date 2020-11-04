using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPanel : CharmsPanel
{
    public GameObject overlayControls; // in this case we need the canvasOverlay active to show the currency indicator, so that is whey we have this explicit reference to the Controls scene object
    public GameObject arrowControls;
    public GameObject gameSelectMenu;
    public StorePanel storePanel;
    public PlayManager playManager;

    private bool backFromStore = false;

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

        // this is for handling the case when returning from store to play panel
        if (backFromStore) {
            backFromStore = false;
            return;
        }

        overlayControls.SetActive(true);
        arrowControls.SetActive(true);
        canvasOverlay.SetActive(true); // inherited
    }

    public void OpenStoreFromGameSelect()
    {
        returnToMain = false;
        backFromStore = true;
        storePanel.SetFromGameSelect(true);
        storePanel.gameObject.SetActive(true);
    }
}
