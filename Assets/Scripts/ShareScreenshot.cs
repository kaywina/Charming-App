using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class ShareScreenshot : MonoBehaviour
{

    public bool hideBannerAd = false;

    public bool useCustomFileName = false;
    public string customFileName = "";

    public bool useLocKeyForSubject = false;
    public string subjectLocKey = "";

    public Button shareButton;
    public Image shareButtonImage;
    private bool isFocus = false;

    private string shareSubject, shareMessage, shareLink;

#if UNITY_ANDROID || UNITY_IOS
    private bool isProcessing = false;

    public bool includeImage = true;
    public bool cropImage = false;

    public float cropAtXRelative = 0f;
    public float cropAtYRelative = 0f;
    public float cropWidthRelative = 1f;
    public float cropHeightRelative = 1f;

    private int cropAtX = 0;
    private int cropAtY = 0;
    private int cropWidth = 0;
    private int cropHeight = 0;

    private float sceneResetDelayInSeconds = 2f;

#endif

    private string screenshotName;

    public GameObject shareBonusIndicator;
    public ActiveUntilDeactivated shareArrow;
    public GameObject[] hideOnShare;
    public bool showAgainAfterShare = false;
    public GameObject url;
    public GameObject thanksObject;
    public GameObject doubleBonusButton;
    public GameObject doubleBonusText;
    public GameObject doubleBonusAmountText;
    public GameObject strikeout;
    public GameObject okButton;

    public Text rewardAmountText;
    public CurrencyManager currencyManager;
    public CurrencyIndicator currencyIndicator;

    public int baseBonusAmount = 8;
    private int giveBonusAmount = 0;
    private bool bonusGiven;

    private bool[] alreadyHiddenIndices;

    private void OnEnable()
    {
        bonusGiven = false;
        if (thanksObject != null) { thanksObject.SetActive(false); }
        if (url != null) { url.SetActive(false); }
        if (doubleBonusButton != null) { doubleBonusButton.SetActive(false); }
        if (doubleBonusText != null) { doubleBonusText.SetActive(false); }
        if (doubleBonusAmountText != null) { doubleBonusAmountText.SetActive(false); }
        if (rewardAmountText != null) { rewardAmountText.text = baseBonusAmount.ToString(); }
        if (strikeout != null) { strikeout.SetActive(false); }
        if (shareArrow != null) { shareArrow.Reactivate(); }
    }

    void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }

    public void OnShareButtonClick()
    {
        if (useCustomFileName)
        {
            screenshotName = customFileName;
        }
        else
        {
            screenshotName = string.Format("{0}.png", PlayerPrefs.GetString("Charm"));
        }
        
        string tempSubject = Localization.GetTranslationByKey(subjectLocKey);
        if (useLocKeyForSubject && !string.IsNullOrEmpty(tempSubject))
        {
            shareSubject = tempSubject;
        }
        else
        {
            shareSubject = PlayerPrefs.GetString("Charm"); // default to using the currently active charm name if no valid loc key is used or provided
        }

        //Debug.Log("Sharing with subject - " + shareSubject);
        shareLink = "www.charmingapp.com";
        shareMessage = string.Format("{0} - {1}", Application.productName, shareLink); // share message is always app name + url

        if (hideBannerAd) { GoogleMobileAdsController.HideBannerAd(); }
        ShareSS();
    }

    private void ShareSS()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (!isProcessing)
        {
            StartCoroutine(TakeSSAndShare());
            if (includeImage) { DelaySceneReset(sceneResetDelayInSeconds); }
        }

#else
        Debug.Log("No sharing set up for this platform.");
#endif
    }

    private void DelaySceneReset(float delay)
    {
        Invoke("ResetScene", delay);
    }

    private void SetUpScene()
    {
        // hide some objects while taking screenshot
        if (shareBonusIndicator != null) { shareBonusIndicator.SetActive(false); }

        alreadyHiddenIndices = new bool[hideOnShare.Length];
        for (int i = 0; i < hideOnShare.Length; i++)
        {
            alreadyHiddenIndices[i] = hideOnShare[i].activeSelf;
            hideOnShare[i].SetActive(false);
            //Debug.Log("hide " + hideOnShare[i].name);
        }

        // hide ok button and currency indicator if they are linked in inspector
        if (okButton != null) { okButton.SetActive(false); }
        if (currencyIndicator != null) { currencyIndicator.gameObject.SetActive(false); }

        // hide the share button and make it non-interactable
        Button shareButton = GetComponent<Button>();
        if (shareButtonImage != null) { shareButtonImage.enabled = false; }
        shareButton.interactable = false;

        // show the url
        if (url != null) { url.SetActive(true); }
    }

    private void ResetScene()
    {
        // show the share button again and make it interactable
        if (shareButtonImage != null) { shareButtonImage.enabled = true; }
        shareButton.interactable = true;

        // hide url object
        if (url != null) { url.SetActive(false); }

        // show the hidden objects again
        if (showAgainAfterShare)
        {
            for (int i = 0; i < hideOnShare.Length; i++)
            {
                if (alreadyHiddenIndices[i] == true)
                {
                    hideOnShare[i].SetActive(true);
                }
            }
        }

        // always say thank you and re-show ok button and currency indicator as required
        if (thanksObject != null) { thanksObject.SetActive(true); }
        if (okButton != null) { okButton.SetActive(true); }
        if (currencyIndicator != null) { currencyIndicator.gameObject.SetActive(true); }

        if (hideBannerAd) { GoogleMobileAdsController.ShowBannerAd(); }
    }

    private void GiveBonus()
    {
        //Debug.Log("Give bonus");

        bool giveBonus = false;

        // give share bonus kisses one time only
        if (!bonusGiven && baseBonusAmount > 0)
        {
            //Debug.Log("Only give the bonus once");
            bonusGiven = true;
            giveBonusAmount = baseBonusAmount;
            giveBonus = true;
        }

        // givenBonusAmount will be less than or equal to base amount as long as user has not watched a rewarded video; this prevents the button from showing if the user chooses to share more than once
        if (giveBonusAmount <= baseBonusAmount && doubleBonusButton != null)
        {
            //Debug.Log("User has not yet watched a rewarded video ad");
            // only show the rewarded ad double bonus button if not a gold subscriber and has opted-in to advertising

            /* Unity Ads have been disabled
            if (!UnityIAPController.IsGold() && UnityAdsController.GetAllowAds())
            {
                //Debug.Log("Activate rewarded ad double bonus button");
                doubleBonusButton.SetActive(true);
            }
            else if (UnityIAPController.IsGold()) // if a gold subscriber
            */

            /* Disabling the double share bonus for gold subscribers
            if (UnityIAPController.IsGold()) // if a gold subscriber
            {
                //Debug.Log("Give double bonus to gold subscribers");
                giveBonusAmount = giveBonusAmount * 2;
                //rewardAmountText.text = givenBonusAmount.ToString();
                doubleBonusText.SetActive(true);
                strikeout.SetActive(true);
                doubleBonusAmountText.SetActive(true);
                giveBonus = true;
            }
            */
            // just show the normal thanks (make sure strike-through and double bonus text are disabled in scene!    
        }
        if (giveBonus)
        {
            currencyManager.GiveBonus(giveBonusAmount); ;
            if (currencyIndicator != null) { currencyIndicator.UpdateIndicatorAnimated(); }
        }
    }

#if UNITY_ANDROID || UNITY_IOS
    private IEnumerator TakeSSAndShare()
    {
        isProcessing = true;

        if (!includeImage)
        {
            ShareMessage();
            UnityAnalyticsController.SendShareAnalyticsEvent(includeImage);
            isProcessing = false;
            yield break;
        }
        // if we are including the image the continue
        SetUpScene();

        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

        // if we crop the screenshot down then differnet path
        if (cropImage)
        {
            cropAtX = (int)(Screen.width * cropAtXRelative);
            cropAtY = (int)(Screen.height * cropAtYRelative);
            cropWidth = (int)(Screen.width * cropWidthRelative);
            cropHeight = (int)(Screen.height * cropHeightRelative);

            Color[] pix = ss.GetPixels(cropAtX, cropAtY, cropWidth, cropHeight);
            ss = new Texture2D(cropWidth, cropHeight);
            ss.SetPixels(pix);
        }

        ss.Apply();

        string filePath = SaveTexture2DAsFile(ss);
        ShareImageFile(filePath);

        //Debug.Log("Finished sharing image");

        UnityAnalyticsController.SendShareAnalyticsEvent(includeImage);

        isProcessing = false;
}
#endif

    public string SaveTexture2DAsFile(Texture2D tex)
    {
        string filePath = Path.Combine(Application.temporaryCachePath, screenshotName);
        File.WriteAllBytes(filePath, tex.EncodeToPNG());
        //Debug.Log("Screenshot texture saved at path " + filePath);
        Destroy(tex);
        ResetScene();
        GiveBonus();
        return filePath;
    }

    public void ShareImageFile(string filePath)
    {
        new NativeShare().AddFile(filePath).SetSubject(shareSubject).SetText(shareMessage).Share();
    }

    public void ShareMessage()
    {
        new NativeShare().SetSubject(shareSubject).SetText(shareMessage).Share();
    }

    public bool GetBonusGive ()
    {
        return bonusGiven;
    }

    public int GetGivenBonusAmount()
    {
        return giveBonusAmount;
    }

    public void SetGivenBonusAmount(int amount)
    {
        giveBonusAmount = amount;
    }
}