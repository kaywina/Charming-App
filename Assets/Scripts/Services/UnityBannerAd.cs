using System.Collections;
using UnityEngine;
//using UnityEngine.Advertisements;

public class UnityBannerAd : MonoBehaviour
{
    public string bannerPlacement = "bannerUnlock";
    private bool showing;

    void OnEnable()
    {
        //StartCoroutine(ShowBannerWhenReady());
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
