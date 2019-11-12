using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreatheControl : MonoBehaviour
{
    private float breatheInOutSeconds = 2f;

    public float GetBreatheInOutSeconds()
    {
        return breatheInOutSeconds;
    }

    public void SetBreatheInOutSeconds(float seconds)
    {
        breatheInOutSeconds = seconds;
    }
}
