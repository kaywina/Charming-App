using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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


    public int bonusAmount = 8;
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
        shareSubject = PlayerPrefs.GetString("Charm");
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
        for (int i = 0; i < hideOnShare.Length; i++)
        {
            hideOnShare[i].SetActive(false);
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
            try
            {
                //Create intent for action send
                using (AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent"))
                {

                    AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
                    intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

                    // old way - get the Uri from parsing the file path string
                    //AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
                    //AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + screenShotPath); // replaced with File Provider

                    // new way compatible with Android 8 - use FileProvider

                    // add required flag to intent object
                    intentObject.Call<AndroidJavaObject>("addFlags", 0x00000001); // FLAG_GRANT_READ_URI_PERMISSION
                                                                                  //intentObject.Call<AndroidJavaObject>("addFlags", 0x00000002); // FLAG_GRANT_WRITE_URI_PERMISSION

                    using (AndroidJavaClass fileProviderClass = new AndroidJavaClass("android.support.v4.content.FileProvider")) // get the FileProvider class
                    {
                        // create the File object
                        AndroidJavaClass fileClass = new AndroidJavaClass("java.io.File");
                        AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", screenShotPath);

                        // get the context
                        AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                        AndroidJavaObject currentActivityObject = playerClass.GetStatic<AndroidJavaObject>("currentActivity"); // Context

                        // create the uriObject
                        string packageName = currentActivityObject.Call<string>("getPackageName");
                        string authority = packageName + ".provider";
                        AndroidJavaObject uriObject = fileProviderClass.CallStatic<AndroidJavaObject>("getUriForFile", currentActivityObject, authority, fileObject);

                        // add screenshot uri to the Intent
                        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
                    }


                    // add type and extra text to the Intent
                    intentObject.Call<AndroidJavaObject>("setType", "image/png");
                    intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), shareSubject);
                    intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

                    using (AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                    {
                        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
                        AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share your success!");
                        currentActivity.Call("startActivity", chooser);
                    }
                }
            }
            catch
            {
                Debug.LogError("Exception thrown while trying to share Android screenshot");
            }

            yield return new WaitUntil(() => isFocus);
        }

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

        // give share bonus kisses one time only
        if (!bonusGiven && bonusAmount > 0)
        {
            CurrencyManager.Instance.GivePremiumBonus(bonusAmount);
            bonusGiven = true;
        }

        if (thanksText != null) { thanksText.SetActive(true); }
        if (doubleBonusButton != null) { doubleBonusButton.SetActive(true); }
        isProcessing = false;
    }
#endif
}