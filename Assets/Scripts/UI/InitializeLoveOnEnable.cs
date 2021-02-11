using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLoveOnEnable : MonoBehaviour
{
    public LoveManager loveManager;
    public GameObject easyLove;
    public GameObject toughLove;

    private void OnEnable()
    {
        loveManager.Initialize();

        easyLove.SetActive(true);
        toughLove.SetActive(false);
    }
}
