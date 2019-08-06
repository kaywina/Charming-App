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
    private bool premiumCurrency;
    public GameObject goldKeyImage;
    public GameObject silverKeyImage;
    public StorePanel storePanel;
    private bool returnToOptions;

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
        if (returnToOptions)
        {
            optionsPanel.gameObject.SetActive(true);
            returnToMain = false;
        }
        else
        {
            optionsPanel.SetReturnToMain(true); // i think there was an edge case bug related to this; should've commented it at the time I fixed it oops
            returnToMain = true;
        }
        
        base.OnDisable();
        gameObject.SetActive(false);
    }

    public void SetUnlockCost(int toSet)
    {
        cost = toSet;
        costText.text = cost.ToString();
    }

    public void SetObjectToUnlock(GameObject toSet, GameObject button, bool charm, bool usePremiumCurrency, bool cameFromOptionsPanel)
    {
        toUnlock = toSet;
        unlockButton = button;
        isCharm = charm;
        premiumCurrency = usePremiumCurrency;
        returnToOptions = cameFromOptionsPanel;
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

        // only two UI options currently supported; either unlocking charm in main UI and go to congrats screen, or unlocking option with 2D button
        // other options will result in black UI since no panel is opened after closing the unlock panel
        if (isCharm)
        {
            charms.SetCharm(toUnlock.name);
            congratsPanel.SetUnlockedObject(toUnlock);
            congratsPanel.ShowPanel(isCharm);
        }
        
        else if (returnToOptions)
        {
            optionsPanel.ShowPanel();
        }
        else
        {
            optionsPanel.SetReturnToMain(true);
            optionsPanel.ShowPanel();
        }

        gameObject.SetActive(false);
    }


    public void OpenStore()
    {
        storePanel.SetFromUnlock(true);
        storePanel.gameObject.SetActive(true);
    }
}
