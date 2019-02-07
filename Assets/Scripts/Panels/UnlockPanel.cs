using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPanel : CharmsPanel
{
    public Charms charms;
    public GameObject notEnoughText;
    public Button yesButton;
    public Text costText;
    public CongratsPanel congratsPanel;
    private int cost;
    public GameObject toUnlock;
    private GameObject unlockButton;
    private bool isCharm;

    new void OnEnable()
    {
        base.OnEnable();
        if (CurrencyManager.CanWithdrawAmount(cost))
        {
            yesButton.interactable = true;
            notEnoughText.SetActive(false);
        }
        else
        {
            yesButton.interactable = false;
            notEnoughText.SetActive(true);
        }
    }

    new void OnDisable()
    {
        cost = 0;
        toUnlock = null;
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
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

        if (isCharm)
        {
            charms.SetCharm(toUnlock.name);
        }
        
        congratsPanel.SetUnlockedObject(toUnlock);
        congratsPanel.ShowPanel(isCharm);
        gameObject.SetActive(false);
    }
}
