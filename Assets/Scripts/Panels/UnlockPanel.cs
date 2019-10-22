using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPanel : CharmsPanel
{
    public Charms charms;
    public GameObject notEnoughText;
    public GameObject storeButton;
    public Button buyButton;
    public Text costText;
    public CongratsPanel congratsPanel;
    public OptionsPanel optionsPanel;
    private static int cost;
    private static GameObject toUnlock;
    private GameObject unlockButton;
    private bool isCharm;
    public GameObject silverKeyImage;
    public StorePanel storePanel;

    new void OnEnable()
    {
        base.OnEnable();
        bool canWithdraw = false;
        canWithdraw = CurrencyManager.CanWithdrawAmount(cost);
        silverKeyImage.SetActive(true);

        if (canWithdraw)
        {
            buyButton.interactable = true;
            storeButton.SetActive(false);
            notEnoughText.SetActive(false);
        }
        else
        {
            buyButton.interactable = false;
            storeButton.SetActive(true);
            notEnoughText.SetActive(true);
        }
    }

    new void OnDisable()
    {
        // do not call base.OnDisable here because we go to congrats panel after not back to main UI
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        optionsPanel.SetReturnToMain(true); // i think there was an edge case bug related to this; should've commented it at the time I fixed it oops
        returnToMain = true;
        base.OnDisable();
        gameObject.SetActive(false);
    }

    public void SetUnlockCost(int toSet)
    {
        cost = toSet;
        costText.text = cost.ToString();
    }

    public void SetObjectToUnlock(GameObject toSet, GameObject button, bool charm)
    {
        toUnlock = toSet;
        unlockButton = button;
        isCharm = charm;
    }

    public void UnlockObject()
    {
        CurrencyManager.WithdrawAmount(cost);
        PlayerPrefs.SetString(toUnlock.name, "unlocked");
        toUnlock.SetActive(true);
        unlockButton.SetActive(false);

        // only one UI option is currently supported; unlocking charm in main UI (won't work on options screen or other panels)
        if (isCharm)
        {
            charms.SetCharm(toUnlock.name);
            congratsPanel.SetUnlockedObject(toUnlock);
            congratsPanel.ShowPanel(isCharm);
        }
        
        optionsPanel.SetReturnToMain(true);
        gameObject.SetActive(false);
    }


    public void OpenStore()
    {
        storePanel.SetFromUnlock(true);
        storePanel.gameObject.SetActive(true);
    }
}
