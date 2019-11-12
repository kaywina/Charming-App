using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreatheAnimation : MonoBehaviour
{

    public BreatheControl breatheControl;

    private bool breatheIn = true;
    private float frameTime = 0f;
    private int fps = 30;

    private float minScaleValue = 0.2f;
    private float maxScaleValue = 0.5f;
    private float increment = 0.0f;

    private void OnEnable()
    {
        BreatheControl.OnSliderChanged += UpdateFrameTime;
        breatheIn = true;
        transform.localScale = new Vector3(minScaleValue, minScaleValue, minScaleValue);
        frameTime = breatheControl.GetBreatheInOutSeconds() / fps;
        increment = (maxScaleValue - minScaleValue) / fps;
        InvokeRepeating("NextFrame", 0f, frameTime);
    }

    void UpdateFrameTime()
    {
        frameTime = breatheControl.GetBreatheInOutSeconds() / fps;
        CancelInvoke("NextFrame");
        InvokeRepeating("NextFrame", 0f, frameTime);
        //Debug.Log("Update frame time");
    }

    private void NextFrame()
    {
        
        // do the animation
        if (breatheIn) // get bigger if breathing in
        {
            transform.localScale = new Vector3(transform.localScale.x + increment, transform.localScale.y + increment, transform.localScale.z + increment);
        }
        else // get smaller if breathing out
        {
            transform.localScale = new Vector3(transform.localScale.x - increment, transform.localScale.y - increment, transform.localScale.z - increment);
        }

        // check if finished breathing in or out
        if (transform.localScale.x >= maxScaleValue)
        {
            breatheIn = false;
        }
        else if (transform.localScale.x <= minScaleValue)
        {
            breatheIn = true;
        }
    }


}
