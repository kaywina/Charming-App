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
        optionsPanel.SetReturnToMain(true);
        returnToMain = true;
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


    public void OpenStore()
    {
        storePanel.SetFromUnlock(true);
        storePanel.gameObject.SetActive(true);
    }
}
