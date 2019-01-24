using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPanel : MonoBehaviour
{
    public Charms charms;
    public GameObject notEnoughText;
    public Button yesButton;
    public Text costText;
    private int cost;
    private GameObject toUnlock;
    private GameObject unlockButton;

    private void OnEnable()
    {
        if (CurrencyManager.WithdrawAmount(cost))
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

    private void OnDisable()
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
        PlayerPrefs.SetString(toUnlock.name, "unlocked");
        charms.SetCharm(toUnlock.name);
        toUnlock.SetActive(true);
        unlockButton.SetActive(false);
        HidePanel();
    }
}
