using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubscribePanel : CharmsPanel
{

    public bool fromOptions = false;
    public OptionsPanel optionsPanel;

    private bool optionsRTM;

    public void SetFromOptionsFlag (bool flag)
    {
        fromOptions = flag;
    }

    new void OnEnable()
    {
        base.OnEnable();
        if (fromOptions) {
            returnToMain = false;
            optionsRTM = optionsPanel.returnToMain;
            optionsPanel.SetReturnToMain(false);
            optionsPanel.gameObject.SetActive(false);
        }
        else
        {
            returnToMain = true;
        }
    }

    new void OnDisable()
    {
        base.OnDisable();
        if (fromOptions) {
            optionsPanel.SetReturnToMain(optionsRTM);
            optionsPanel.gameObject.SetActive(true);
        }
    }
}
