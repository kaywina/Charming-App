using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class UnityRewardedAdsButton : MonoBehaviour, IUnityAdsListener
{

    public string placementId = "rewardedVideo";
    private Button adButton;
    public BonusPanel bonusPanel;
    public bool buttonIsOnBonusPanel = true;
    public bool buttonIsOnCongratsPanel = false;
    public bool buttonIsOnPlayPanel = false;
    public ShareScreenshot shareScreenshotAndroid;
    private bool watched;
    public GameObject watchedRewardedAdText;
    public Text doubleRewardAmountText;
    public GameObject strikeout;
    public ParticleSystem explosionParticles;
    public CurrencyIndicator currencyIndicator;

    public GameObject adsHaveAudio;
    public GameObject waitingForAd;

    private void OnEnable()
    {
        adButton = GetComponent<Button>();
        adButton.onClick.AddListener(ShowRewardedAd);
        Advertisement.AddListener(this); // for handling callbacks

        watched = false; // can only use button once during bonus wheel session
        DisableRewardTextObjects();

        if (explosionParticles != null)
        {
            explosionParticles.Play();
        }

        CheckIfAdIsReadyAndEnableCorrectTextObject();
    }

    private void OnDisable()
    {
        adButton.onClick.RemoveListener(ShowRewardedAd);
    }

    private void DisableRewardTextObjects()
    {
        if (watchedRewardedAdText != null)
        {
            watchedRewardedAdText.gameObject.SetActive(false);
        }
        if (doubleRewardAmountText != null)
        {
            doubleRewardAmountText.gameObject.SetActive(false);
        }
        if (strikeout != null)
        {
            strikeout.SetActive(false);
        }
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
        //Debug.Log("Show rewarded ad");
        if (Advertisement.IsReady(placementId))
        {
            //var options = new ShowOptions { resultCallback = HandleShowResult }; // this is old deprecated method
            Advertisement.Show(placementId);
            UnityAnalyticsController.SendStartWatchingRewardedAdEvent();
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
                    bonusPanel.GiveDoubleBonus();
                }

                if (buttonIsOnCongratsPanel && shareScreenshotAndroid != null)
                {
                    doubleRewardAmountText.text = (shareScreenshotAndroid.baseBonusAmount * 2).ToString();
                    CurrencyManager.Instance.GiveBonus(shareScreenshotAndroid.GetGivenBonusAmount());
                }

                if (buttonIsOnPlayPanel)
                {
                    CurrencyManager.Instance.GiveBonus(PlayManager.GetAdReward());
                }

                if (doubleRewardAmountText != null) { doubleRewardAmountText.gameObject.SetActive(true); }
                if (strikeout != null) { strikeout.SetActive(true); }

                watched = true;
                if (watchedRewardedAdText != null) { watchedRewardedAdText.SetActive(true); }
     
                UnityAnalyticsController.SendCompleteWatchingRewardedAdEvent();
                if (currencyIndicator != null) {
                    currencyIndicator.UpdateIndicatorAnimated();
                }
                gameObject.SetActive(false); // deactivate button after completion
                //Debug.Log("The ad was successfully shown.");
                break;
            case ShowResult.Skipped:
                //Debug.Log("The ad was skipped before reaching the end.");
                UnityAnalyticsController.SendSkipWatchingRewardedAdEvent();
                break;
            case ShowResult.Failed:
                //Debug.LogError("The ad failed to be shown.");
                break;
        }
    }

    public void OnUnityAdsReady(string id)
    {
        CheckIfAdIsReadyAndEnableCorrectTextObject();
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }


    private void CheckIfAdIsReadyAndEnableCorrectTextObject()
    {
        // show different text depending on if button is ready or not
        bool adIsReady = Advertisement.IsReady(placementId);
        if (adsHaveAudio != null) { adsHaveAudio.SetActive(adIsReady); }
        if (waitingForAd != null) { waitingForAd.SetActive(!adIsReady); }
    }
}