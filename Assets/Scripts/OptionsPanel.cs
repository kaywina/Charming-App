using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanel : CharmsPanel
{

    new private void OnEnable()
    {
        base.OnEnable();
    }

    new private void OnDisable()
    {
        base.OnDisable();
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
