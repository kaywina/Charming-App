using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretsPanel : CharmsPanel
{
    public GameObject secrets;
    public GameObject menu;

    new void OnEnable()
    {
        secrets.SetActive(false);
        menu.SetActive(true);
        base.OnEnable();
    }

    new void OnDisable()
    {
        base.OnDisable();
    }
}
