using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanel : CharmsPanel
{
    public GameObject feedbackForm;

    new private void OnEnable()
    {
        base.OnEnable();
        feedbackForm.SetActive(false);
    }

    new private void OnDisable()
    {
        if (returnToMain)
        {
            base.OnDisable(); // only call base class method when returning to main charms screen, not if going to bonus wheel scene
        }
        feedbackForm.SetActive(false);
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
