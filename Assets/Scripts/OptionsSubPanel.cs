using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsSubPanel : MonoBehaviour
{
    public OptionsPanel optionsPanel;

    private bool oldRTM;

    void OnEnable()
    {
        oldRTM = optionsPanel.returnToMain;
        optionsPanel.SetReturnToMain(false);
        optionsPanel.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        optionsPanel.SetReturnToMain(oldRTM);
        optionsPanel.gameObject.SetActive(true);
    }
}
