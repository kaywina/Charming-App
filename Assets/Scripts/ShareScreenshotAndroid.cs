using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class ShareScreenshotAndroid : MonoBehaviour
{
    public Button shareButton;
    public Image shareButtonImage;
    private bool isFocus = false;

    private string shareSubject, shareMessage, shareLink;
    private bool isProcessing = false;
    private string screenshotName;

    public GameObject shareBonusIndicator;
    public GameObject[] hideOnShare;
    public bool showAgainAfterShare = false;
    public GameObject url;
    public GameObject thanksText;
    public GameObject doubleBonusButton;
    public GameObject doubleBonusText;
    public GameObject okButton;

    public Text rewardAmountText;


    public int baseBonusAmount = 8;
    private int givenBonusAmount = 0;
    private bool bonusGiven;

    private float sceneResetDelayInSeconds = 2f;

    private void OnEnable()
    {
        shareButton.onClick.AddListener(OnShareButtonClick);
        bonusGiven = false;
        if (thanksText != null) { thanksText.SetActive(false); }
        if (url != null) { url.SetActive(false); }
        if (doubleBonusButton != null) { doubleBonusButton.SetActive(false); }
        if (doubleBonusText != null) { doubleBonusText.SetActive(false); }
        if (rewardAmountText != null) { rewardAmountText.text = baseBonusAmount.ToString(); }     
    }

    void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }

    public void OnShareButtonClick()
    {
        screenshotName = string.Format("{0}.png", PlayerPrefs.GetString("Charm"));
        shareSubject = PlayerPrefs.GetString("Charm");
        shareLink = "www.charmingapp.com";
        shareMessage = string.Format("{0} - {1}", Application.productName, shareLink);
        ShareScreenshot();
    }

    private void ShareScreenshot()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (!isProcessing)
        {
            StartCoroutine(TakeSSAndShare());
            DelaySceneReset(sceneResetDelayInSeconds);
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
        for (int i = 0; i < hideOnShare.Length; i++)
        {
            hideOnShare[i].SetActive(false);
            //Debug.Log("hide " + hideOnShare[i].name);
        }

        // hide ok button if it's linked in inspector
        if (okButton != null) { okButton.SetActive(false); }

        // hide the share button and make it non-interactable
        Button shareButton = GetComponent<Button>();
        shareButtonImage.enabled = false;
        shareButton.interactable = false;

        // show the url
        if (url != null) { url.SetActive(true); }
    }

    private void ResetScene()
    {
        // show the share button again and make it interactable
        shareButtonImage.enabled = true;
        shareButton.interactable = true;

        // hide url object
        if (url != null) { url.SetActive(false); }

        // show the hidden objects again
        if (showAgainAfterShare)
        {
            for (int i = 0; i < hideOnShare.Length; i++)
            {
                hideOnShare[i].SetActive(true);
            }
        }

        // always say thank you and show ok button
        if (thanksText != null) { thanksText.SetActive(true); }
        if (okButton != null) { okButton.SetActive(true); }
    }

    private void GiveBonus()
    {
        // give share bonus kisses one time only
        if (!bonusGiven && baseBonusAmount > 0)
        {
            bonusGiven = true;
            givenBonusAmount = baseBonusAmount;
        }

        // givenBonusAmount will be less than or equal to base amount as long as user has not watched a rewarded video; this prevents the button from showing if the user chooses to share more than once
        if (givenBonusAmount <= baseBonusAmount && doubleBonusButton != null)
        {
            if (PlayerPrefs.GetString(UnityIAPController.goldSubscriptionPlayerPref) != "true")
            {
                doubleBonusButton.SetActive(true);
            }
            else
            {
                givenBonusAmount = givenBonusAmount * 2;
                //rewardAmountText.text = givenBonusAmount.ToString();
                doubleBonusText.SetActive(true);
            } 
        } 
    }

    private IEnumerator TakeSSAndShare()
    {
        isProcessing = true;

        SetUpScene();

        yield return new WaitForEndOfFrame();
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, screenshotName);
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        Destroy(ss);
        ResetScene();
        GiveBonus();

        new NativeShare().AddFile(filePath).SetSubject(shareSubject).SetText(shareMessage).Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).SetText( "Hello world!" ).SetTarget( "com.whatsapp" ).Share();
 
        isProcessing = false;
    }

    public bool GetBonusGive ()
    {
        return bonusGiven;
    }

    public int GetGivenBonusAmount()
    {
        return givenBonusAmount;
    }

    public void SetGivenBonusAmount(int amount)
    {
        givenBonusAmount = amount;
    }
}