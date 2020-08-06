using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsOptInButton : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject optInPanel;
    
    public void OptIn (bool optIn)
    {
        if (optIn)
        {
            UnityAdsController.AllowAds();
        }
        else
        {
            UnityAdsController.DisallowAds();
        }

        infoPanel.SetActive(true);
        optInPanel.SetActive(false);
    }
}
