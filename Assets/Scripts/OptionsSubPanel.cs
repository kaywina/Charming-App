using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsSubPanel : MonoBehaviour
{
    public OptionsPanel optionsPanel;

    private bool oldRTM;

    private bool wentToSubscribe = false;

    void OnEnable()
    {
        if (!wentToSubscribe)
        {
            oldRTM = optionsPanel.returnToMain;
            optionsPanel.SetReturnToMain(false);
            optionsPanel.gameObject.SetActive(false);
        }
        wentToSubscribe = false;
    }

    void OnDisable()
    {
        if (!wentToSubscribe)
        {
            optionsPanel.SetReturnToMain(oldRTM);
            optionsPanel.gameObject.SetActive(true);
        }
    }

    public void SetWentToSubscribe(bool toSet)
    {
        wentToSubscribe = toSet;
    }
}
