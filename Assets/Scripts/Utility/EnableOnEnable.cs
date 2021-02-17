using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnEnable : MonoBehaviour
{

    public GameObject[] toEnable;


    private void OnEnable()
    {
        for (int i = 0; i < toEnable.Length; i++)
        {
            toEnable[i].SetActive(true);
        }
    }
}
