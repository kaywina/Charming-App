using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityBannerAd : MonoBehaviour
{
    public string bannerPlacement = "bannerUnlock";
    private bool showing;

    void OnEnable()
    {
        //StartCoroutine(ShowBannerWhenReady());
        showing = false;
    }

    private void Update()
    {
        if (!showing && Advertisement.IsReady(bannerPlacement))
        {
            Debug.Log("Show banner ad");
            Advertisement.Show(bannerPlacement);
            showing = true;
        }
    }
    /*
    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(bannerPlacement))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Show(bannerPlacement);
    }
    */
}
