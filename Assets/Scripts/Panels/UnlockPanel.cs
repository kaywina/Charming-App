using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPanel : CharmsPanel
{
    public Charms charms;
    public GameObject notEnoughText;
    public Button buyButton;
    public Text costText;
    public CongratsPanel congratsPanel;
    private int cost;
    private GameObject toUnlock;
    private GameObject unlockButton;
    private bool isCharm;
    private bool premiumCurrency;
    public GameObject goldKeyImage;
    public GameObject silverKeyImage;

    new void OnEnable()
    {
        base.OnEnable();
        bool canWithdraw = false;
        if (premiumCurrency)
        {
            canWithdraw = CurrencyManager.CanWithdrawAmountGold(cost);
            goldKeyImage.SetActive(true);
            silverKeyImage.SetActive(false);
        }
        else
        {
            canWithdraw = CurrencyManager.CanWithdrawAmountSilver(cost);
            goldKeyImage.SetActive(false);
            silverKeyImage.SetActive(true);
        }
        
        if (canWithdraw)
        {
            buyButton.interactable = true;
            notEnoughText.SetActive(false);
        }
        else
        {
            buyButton.interactable = false;
            notEnoughText.SetActive(true);
        }
    }

    new void OnDisable()
    {
        cost = 0;
        toUnlock = null;

        // do not call base.OnDisable here because we go to congrats panel after not back to main UI
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

    public void SetObjectToUnlock(GameObject toSet, GameObject button, bool charm, bool usePremiumCurrency)
    {
        toUnlock = toSet;
        unlockButton = button;
        isCharm = charm;
        premiumCurrency = usePremiumCurrency;
    }

    public void UnlockObject()
    {
        if (premiumCurrency)
        {
            CurrencyManager.WithdrawAmountGold(cost);
        }
        else
        {
            CurrencyManager.WithdrawAmountSilver(cost);
        }
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
