using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{

#if UNITY_EDITOR
    private static int shotCount = 0;
#endif

    public static void TakeShot()
    {
#if UNITY_EDITOR
        Debug.Log("Take screenshot");

        // take screenshot and increment count so filenames don't overwrite
        ScreenCapture.CaptureScreenshot("screenshot" + shotCount + ".png", 1);
        shotCount++;
#endif
    }
}
