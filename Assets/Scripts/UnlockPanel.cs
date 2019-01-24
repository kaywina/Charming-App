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
    private int cost;
    private GameObject toUnlock;
    private GameObject unlockButton;

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
        base.OnDisable();
        cost = 0;
        toUnlock = null;
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }

    public void SetUnlockCost(int toSet)
    {
        cost = toSet;
        costText.text = cost.ToString();
    }

    public void SetObjectToUnlock(GameObject toSet, GameObject button)
    {
        toUnlock = toSet;
        unlockButton = button;
    }

    public void UnlockObject()
    {
        CurrencyManager.WithdrawAmount(cost);
        PlayerPrefs.SetString(toUnlock.name, "unlocked");
        toUnlock.SetActive(true);
        unlockButton.SetActive(false);
        charms.SetCharm(toUnlock.name);
        HidePanel();
    }
}
