using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{

    private int shotCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        shotCount = 0;
    }

    public void TakeShot()
    {
        Debug.Log("Take screenshot");

        // take screenshot and increment count so filenames don't overwrite
        ScreenCapture.CaptureScreenshot("screenshot" + shotCount + ".png", 1);
        shotCount++;

    }
}
