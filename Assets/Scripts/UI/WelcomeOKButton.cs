using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeOKButton : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject optInPanel;
    public GameObject welcomePanel;

    public void OnOKButtonClick()
    {
        if (UnityAdsController.IsAllowAdsSet())
        {
            infoPanel.SetActive(true);
        }
        else
        {
            optInPanel.SetActive(true);
        }

        welcomePanel.SetActive(false);

    }
}
