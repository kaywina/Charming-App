using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSourceManager : MonoBehaviour
{
#if UNITY_IOS
    private string YOUR_APP_KEY = "b5bf7fe5";
#elif UNITY_ANDROID
    private string YOUR_APP_KEY = "b5bfb96d";
#else
    private const string YOUR_APP_KEY = "";
#endif

    private void Start()
    {
        IronSource.Agent.init(YOUR_APP_KEY); // this way auto-initializes the ad units specified in the IS dev portal
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    public void Validate()
    {
        IronSource.Agent.validateIntegration();
    }
}
