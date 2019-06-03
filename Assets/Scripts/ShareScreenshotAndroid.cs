using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShareScreenshotAndroid : MonoBehaviour
{
    public Button shareButton;
    public Image shareButtonImage;
    public Text charmsText;
    private bool isFocus = false;

    private string shareSubject, shareMessage, shareLink;
    private bool isProcessing = false;
    private string screenshotName;

    public GameObject shareBonusIndicator;
    public GameObject[] hideDuringShare;
    public GameObject url;
    public GameObject thanksText;

    private int bonusAmount = 8;
    private bool bonusGiven;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        shareButton.onClick.AddListener(OnShareButtonClick);
        bonusGiven = false;
        if (thanksText != null) { thanksText.SetActive(false); }
        if (url != null) { url.SetActive(false); }
    }

    void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }

    public void OnShareButtonClick()
    {

        screenshotName = string.Format("{0}.png", PlayerPrefs.GetString("Charm"));
        shareSubject = charmsText.text;
        shareLink = "www.charmingapp.com";

        shareMessage = string.Format("{0} - {1}", Application.productName, shareLink);
        ShareScreenshot();
    }


    private void ShareScreenshot()
    {

#if UNITY_ANDROID
        if (!isProcessing)
        {
            StartCoroutine(ShareScreenshotInAnroid());
        }

#else
		Debug.Log("No sharing set up for this platform.");
#endif
    }



#if UNITY_ANDROID
    public IEnumerator ShareScreenshotInAnroid()
    {

        isProcessing = true;

        // hide some objects while taking screenshot
        if (shareBonusIndicator != null) { shareBonusIndicator.SetActive(false); }
        for (int i = 0; i < hideDuringShare.Length; i++)
        {
            hideDuringShare[i].SetActive(false);
        }

        // hide the share button and make it non-interactable
        Button shareButton = GetComponent<Button>();
        shareButtonImage.enabled = false;
        shareButton.interactable = false;

        // show the url
        if (url != null) { url.SetActive(true); }

        // wait for graphics to render
        yield return new WaitForEndOfFrame();

        string screenShotPath = Application.persistentDataPath + "/" + screenshotName;
        ScreenCapture.CaptureScreenshot(screenshotName, 1);
        yield return new WaitForSeconds(0.5f);

        if (!Application.isEditor)
        {
            //Create intent for action send
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            //create image URI to add it to the intent
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + screenShotPath);

            //put image and string extra
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("setType", "image/png");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), shareSubject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share your success!");
            currentActivity.Call("startActivity", chooser);

            yield return new WaitUntil(() => isFocus);
        }

        // show the share button again and make it interactable
        shareButtonImage.enabled = true;
        shareButton.interactable = true;

        // hide url object
        if (url != null) { url.SetActive(false); }

        // show the hidden objects again
        for (int i = 0; i < hideDuringShare.Length; i++)
        {
            hideDuringShare[i].SetActive(true);
        }

        // give share bonus kisses one time only
        if (!bonusGiven)
        {
            CurrencyManager.Instance.GivePremiumBonus(bonusAmount);
            bonusGiven = true;
            if (thanksText != null) { thanksText.SetActive(true); }
        }
        isProcessing = false;
    }
#endif
}