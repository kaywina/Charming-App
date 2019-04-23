using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomePanel : CharmsPanel
{
    public GameObject bonusPanel;

    new private void OnEnable()
    {
        base.OnEnable();
    }

    new private void OnDisable()
    {
        bonusPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
