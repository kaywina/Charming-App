using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour
{
    private Button adButton;
    public BonusPanel bonusPanel;
    public bool buttonIsOnBonusPanel = true;
    public bool buttonIsOnCongratsPanel = false;
    public bool buttonIsOnPlayPanel = false;
    public ShareScreenshotAndroid shareScreenshotAndroid;
    public GameObject watchedRewardedAdText;
    public Text doubleRewardAmountText;
    public GameObject strikeout;
    public ParticleSystem explosionParticles;

    private void OnEnable()
    {
        adButton = GetComponent<Button>();

        adButton.interactable = (AdmobController.IsRewardedAdReady()); // if ad isn't ready then button is non-interacteable

        adButton.onClick.AddListener(ShowRewardedAd);
        AdmobController.OnRewardedAdWatched += HandleRewardEarned;

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
        AdmobController.OnRewardedAdWatched -= HandleRewardEarned;
    }

    void ShowRewardedAd()
    {
        AdmobController.TryToShowRewardedAd();
    }


    private void HandleRewardEarned()
    {
        Debug.Log("Handle reward earned");

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
        if (watchedRewardedAdText != null) { watchedRewardedAdText.SetActive(true); }

        gameObject.SetActive(false); // deactivate button after completion
    }
}