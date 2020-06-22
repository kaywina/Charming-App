using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLoveOnEnable : MonoBehaviour
{
    public LoveManager loveManager;

    private void OnEnable()
    {
        loveManager.Initialize();
    }
}
