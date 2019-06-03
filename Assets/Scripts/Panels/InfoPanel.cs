using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : CharmsPanel
{
    new void OnEnable()
    {
        if (PlayerPrefs.GetString("ShowInfo") == "false")
        {
            gameObject.SetActive(false);
            return;
        }
        base.OnEnable();
    }
}
