using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreatheAnimation : MonoBehaviour
{

    public BreatheControl breatheControl;

    private int fps = 60;

    private float frameTime;
    private float increment;

    private float minScaleValue = 0.2f;
    private float maxScaleValue = 0.5f;
    

    private void OnEnable()
    {
        BreatheControl.OnSliderChanged += UpdateFrameTime;
        transform.localScale = new Vector3(minScaleValue, minScaleValue, minScaleValue);
        frameTime = breatheControl.GetBreatheInOutSeconds() / fps;
        increment = (maxScaleValue - minScaleValue) / fps;

        InvokeRepeating("NextFrame", 0f, frameTime);
    }

    private void OnDisable()
    {
        CancelInvoke("NextFrame");
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
        if (breatheControl.GetBreatheInOutFlag()) // get bigger if breathing in
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
            breatheControl.SetBreatheInOutFlag(false);
        }
        else if (transform.localScale.x <= minScaleValue)
        {
            breatheControl.SetBreatheInOutFlag(true);
        }
    }


}
