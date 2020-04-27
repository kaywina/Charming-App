using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class UnityRewardedAdsButton : MonoBehaviour, IUnityAdsListener
{

    public string placementId = "rewardedVideo";
    private Button adButton;
    public bool buttonIsOnBonusPanel = true;
    public BonusPanel bonusPanel;
    public bool buttonIsOnCongratsPanel = false;
    public ShareScreenshotAndroid shareScreenshotAndroid;
    private bool watched;
    public GameObject watchedRewardedAdText;
    public Text doubleRewardAmountText;
    public GameObject strikeout;

    private void OnEnable()
    {
        adButton = GetComponent<Button>();
        adButton.onClick.AddListener(ShowRewardedAd);
        Advertisement.AddListener(this); // for handling callbacks

        watched = false; // can only use button once during bonus wheel session

        if (doubleRewardAmountText != null)
        {
            doubleRewardAmountText.gameObject.SetActive(false);
        }
        if (strikeout != null)
        {
            strikeout.SetActive(false);
        }
    }

    private void OnDisable()
    {
        adButton.onClick.RemoveListener(ShowRewardedAd);
    }

    void Update()
    {
        if (adButton)
        {
            adButton.interactable = (!watched && Advertisement.IsReady(placementId));
        }
    }

    void ShowRewardedAd()
    {

        if (Advertisement.IsReady(placementId))
        {
            //var options = new ShowOptions { resultCallback = HandleShowResult }; // this is old deprecated method
            Advertisement.Show(placementId);
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished:
                if (buttonIsOnBonusPanel && bonusPanel != null)
                {
                    bonusPanel.DoubleBonus();
                }

                if (buttonIsOnCongratsPanel && shareScreenshotAndroid != null)
                {
                    shareScreenshotAndroid.SetGivenBonusAmount(shareScreenshotAndroid.baseBonusAmount * 2);
                    doubleRewardAmountText.text = shareScreenshotAndroid.GetGivenBonusAmount().ToString();
                }

                if (doubleRewardAmountText != null) { doubleRewardAmountText.gameObject.SetActive(true); }
                if (strikeout != null) { strikeout.SetActive(true); }

                watched = true;
                if (watchedRewardedAdText != null) { watchedRewardedAdText.SetActive(true); }

                gameObject.SetActive(false); // deactivate button after completion

                //Debug.Log("The ad was successfully shown.");
                break;
            case ShowResult.Skipped:
                //Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                //Debug.LogError("The ad failed to be shown.");
                break;
        }
    }

    public void OnUnityAdsReady(string id)
    {
        // If the ready Placement is rewarded, show the ad:
        /*
        if (id == placementId)
        {
            Advertisement.Show(placementId);
        }
        */
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}