using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableByPlatform : MonoBehaviour
{
    public GameObject gameObjectToEnable;
    public bool ios;
    public bool android;

    void OnEnable()
    {
        gameObjectToEnable.SetActive(false); // false by default

        // enable object depending on platform
#if UNITY_IOS
        if (ios) { gameObjectToEnable.SetActive(true); }
#elif UNITY_ANDROID
        if (android) { gameObjectToEnable.SetActive(true); }
#endif
    }
}
