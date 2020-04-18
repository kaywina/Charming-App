using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURLByPlatform : MonoBehaviour
{
    public string iosURL = "";
    public string androidURL = "";

    private string GetURL()
    {
#if UNITY_ANDROID
        return androidURL;
#elif UNITY_IOS
        return iOSURL
#else
        Debug.Log("Open URL functionality not defined for this platform;
        return "http://www.charmingapp.com/faq";
        return null;
#endif
    }

    public void OpenURL()
    {
        Application.OpenURL(GetURL());
    }
}
