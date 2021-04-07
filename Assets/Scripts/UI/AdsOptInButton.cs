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
            Debug.Log("No longer imlemented following change from Unity Ads to Admob");
            //UnityAdsController.AllowAds();
        }
        else
        {
            Debug.Log("No longer imlemented following change from Unity Ads to Admob");
            //UnityAdsController.DisallowAds();
        }

        infoPanel.SetActive(true);
        optInPanel.SetActive(false);
    }
}
