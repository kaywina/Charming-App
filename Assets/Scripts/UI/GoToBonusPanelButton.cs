using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class GoToBonusPanelButton : MonoBehaviour
{
    public CurrencyManager currencyManager;
    public GameObject bonusPanel;
    public GameObject infoPanel;

    public void TryOpenBonusPanel()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        

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

        stopwatch.Stop();
        UnityEngine.Debug.Log("Time for TryOpenBonusPanel in milliseconds = " + stopwatch.ElapsedMilliseconds);

    }
}
