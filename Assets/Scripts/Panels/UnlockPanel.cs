using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPanel : CharmsPanel
{
    public Charms charms;

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
    public LocalizationText hintText;
    public GameObject needMoreKeysText;
    public GameObject goForItText;

    public GameObject confirmArrow;
    public GameObject storeArrow;

    // don't need these two variables if the store button is always shown
    //public GameObject notEnoughText;
    //public GameObject storeButton;

    new void OnEnable()
    {
        SetHintText(toUnlock.name);

        base.OnEnable();
        bool canWithdraw = false;
        canWithdraw = CurrencyManager.CanWithdrawAmount(cost);
        silverKeyImage.SetActive(true);

        if (canWithdraw)
        {
            buyButton.interactable = true;
            //if (storeButton != null) { storeButton.SetActive(false); }
            needMoreKeysText.SetActive(false);
            goForItText.SetActive(true);
            confirmArrow.SetActive(true);
            storeArrow.SetActive(false);
        }
        else
        {
            buyButton.interactable = false;
            //if (storeButton != null) { storeButton.SetActive(true); }
            goForItText.SetActive(false);
            needMoreKeysText.SetActive(true);
            confirmArrow.SetActive(false);
            storeArrow.SetActive(true);
        }

        
    }

    private void SetHintText(string charmName)
    {
        switch (charmName)
        {
            case "Grace":
                hintText.localizationKey = "HINT_GRACE";
                break;
            case "Patience":
                hintText.localizationKey = "HINT_PATIENCE";
                break;
            case "Wisdom":
                hintText.localizationKey = "HINT_WISDOM";
                break;
            case "Joy":
                hintText.localizationKey = "HINT_JOY";
                break;
            case "Focus":
                hintText.localizationKey = "HINT_FOCUS";
                break;
            case "Will":
                hintText.localizationKey = "HINT_WILL";
                break;
            case "Guile":
                hintText.localizationKey = "HINT_GUILE";
                break;
            case "Force":
                hintText.localizationKey = "HINT_FORCE";
                break;
            case "Honor":
                hintText.localizationKey = "HINT_HONOR";
                break;
            case "Faith":
                hintText.localizationKey = "HINT_FAITH";
                break;
            case "Vision":
                hintText.localizationKey = "HINT_VISION";
                break;
            case "Balance":
                hintText.localizationKey = "HINT_BALANCE";
                break;
            case "Harmony":
                hintText.localizationKey = "HINT_HARMONY";
                break;
            case "Regard":
                hintText.localizationKey = "HINT_REGARD";
                break;
            case "Insight":
                hintText.localizationKey = "HINT_INSIGHT";
                break;
            case "Plenty":
                hintText.localizationKey = "HINT_PLENTY";
                break;
            case "Influence":
                hintText.localizationKey = "HINT_INFLUENCE";
                break;
            default:
                hintText.localizationKey = null;
                Debug.LogWarning("Localization key for hint text not found");
                break;
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
