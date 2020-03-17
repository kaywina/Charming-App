using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubscribePanel : CharmsPanel
{

    public bool fromOptions = false;
    public bool fromLove = false;

    public OptionsPanel optionsPanel;
    public CharmsPanel lovePanel;

    private bool optionsRTM;
    private bool loveRTM;

    public void SetFromOptionsFlag (bool flag)
    {
        fromOptions = flag;
    }

    public void SetFromLoveFlag (bool flag)
    {
        fromLove = flag;
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
        else if (fromLove) {
            returnToMain = false;
            loveRTM = lovePanel.returnToMain;
            lovePanel.SetReturnToMain(false);
            lovePanel.gameObject.SetActive(false);
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
        else if (fromLove)
        {
            lovePanel.SetReturnToMain(loveRTM);
            lovePanel.gameObject.SetActive(true);
        }
    }
}
