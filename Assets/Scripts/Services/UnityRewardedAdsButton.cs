using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class UnityRewardedAdsButton : MonoBehaviour
{
    private Button adButton;
    public BonusPanel bonusPanel;
    public bool buttonIsOnBonusPanel = true;
    public bool buttonIsOnCongratsPanel = false;
    public bool buttonIsOnPlayPanel = false;
    public ShareScreenshotAndroid shareScreenshotAndroid;
    private bool watched;
    public GameObject watchedRewardedAdText;
    public Text doubleRewardAmountText;
    public GameObject strikeout;
    public ParticleSystem explosionParticles;

    private void OnEnable()
    {
        adButton = GetComponent<Button>();

        adButton.interactable = (!watched && AdmobController.IsRewardedAdReady()); // if ad isn't ready then button is non-interacteable

        adButton.onClick.AddListener(ShowRewardedAd);

        watched = false; // can only use button once during bonus wheel session
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

        if (explosionParticles != null)
        {
            explosionParticles.Play();
        }
    }

    private void OnDisable()
    {
        adButton.onClick.RemoveListener(ShowRewardedAd);
    }

    void ShowRewardedAd()
    {
        AdmobController.TryToShowRewardedAd();
    }


    private void HandleRewardEarned()
    {
        if (buttonIsOnBonusPanel && bonusPanel != null)
        {
            bonusPanel.DoubleBonus();
        }

        if (buttonIsOnCongratsPanel && shareScreenshotAndroid != null)
        {
            shareScreenshotAndroid.SetGivenBonusAmount(shareScreenshotAndroid.baseBonusAmount * 2);
            doubleRewardAmountText.text = shareScreenshotAndroid.GetGivenBonusAmount().ToString();
        }

        if (buttonIsOnPlayPanel)
        {
            CurrencyManager.Instance.GiveBonus(PlayManager.GetAdReward());
            CurrencyManager.Instance.ShowBonusIndicator(PlayManager.GetAdReward());
        }

        if (doubleRewardAmountText != null) { doubleRewardAmountText.gameObject.SetActive(true); }
        if (strikeout != null) { strikeout.SetActive(true); }

        watched = true;
        if (watchedRewardedAdText != null) { watchedRewardedAdText.SetActive(true); }

        gameObject.SetActive(false); // deactivate button after completion
    }


    /* Deprecated Unity Ads implementation
    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished:
                HandleRewardEarned();
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
    */
}