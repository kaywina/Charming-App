using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class UnityRewardedAdsButton : MonoBehaviour
{

    public string placementId = "rewardedVideo";
    private Button adButton;
    public BonusPanel bonusPanel;
    private bool watched;

#if UNITY_IOS
    private string gameId = "1234567";
#elif UNITY_ANDROID
    private string gameId = "7654321";
#endif

    void Start()
    {
        adButton = GetComponent<Button>();
        if (adButton)
        {
            adButton.onClick.AddListener(ShowRewardedAd);
        }

        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, true);
        }
    }

    private void OnEnable()
    {
        watched = false; // can only use button once during bonus wheel session
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
                bonusPanel.DoubleBonus();
                watched = true;
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