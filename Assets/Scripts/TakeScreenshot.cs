using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{

    public GameObject takeScreenshotButton;
#if UNITY_EDITOR
    private int shotCount = 0;
#endif

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        shotCount = 0;
#else
        takeScreenshotButton.SetActive(false);
#endif
    }

    public void TakeShot()
    {
#if UNITY_EDITOR
        Debug.Log("Take screenshot");

        // take screenshot and increment count so filenames don't overwrite
        ScreenCapture.CaptureScreenshot("screenshot" + shotCount + ".png", 1);
        shotCount++;
#endif
    }
}
