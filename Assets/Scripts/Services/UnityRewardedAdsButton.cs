using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class UnityRewardedAdsButton : MonoBehaviour
{

    public string placementId = "rewardedVideo";
    private Button adButton;
    public bool buttonIsOnBonusPanel = true;
    public BonusPanel bonusPanel;
    public bool buttonIsOnCongratsPanel = false;
    public ShareScreenshotAndroid shareScreenshotAndroid;
    private bool watched;
    public GameObject watchedRewardedAdText;
    public Text rewardAmountText;

    private void OnEnable()
    {
        adButton = GetComponent<Button>();
        adButton.onClick.AddListener(ShowRewardedAd);
        watched = false; // can only use button once during bonus wheel session
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
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(placementId, options);
        }
    }

    void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                if (buttonIsOnBonusPanel && bonusPanel != null) { bonusPanel.DoubleBonus(); }
                if (buttonIsOnCongratsPanel && shareScreenshotAndroid != null)
                {
                    shareScreenshotAndroid.givenBonusAmount = shareScreenshotAndroid.baseBonusAmount * 2;
                    shareScreenshotAndroid.rewardAmountText.text = shareScreenshotAndroid.givenBonusAmount.ToString();
                    if (rewardAmountText != null) { rewardAmountText.text = "16"; }
                }
                
                watched = true;
                watchedRewardedAdText.SetActive(true);
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
}