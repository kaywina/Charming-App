using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBonusPanelButton : MonoBehaviour
{
    public CurrencyManager currencyManager;
    public GameObject bonusPanel;
    public GameObject infoPanel;

    public void TryOpenBonusPanel()
    {
        bool canOpen = currencyManager.CanOpenBonusPanel();
        if (canOpen)
        {
            CharmsPanel charmsPanel = infoPanel.GetComponent<CharmsPanel>();
            charmsPanel.DeactivateObjects();
            bonusPanel.SetActive(true);
            infoPanel.SetActive(false);
        }
        else
        {
            CharmsPanel charmsPanel = infoPanel.GetComponent<CharmsPanel>();
            charmsPanel.SetReturnToMain(true);
            charmsPanel.DisableCharmsPanel();
        }
    }
}
