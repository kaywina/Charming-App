using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugObject : MonoBehaviour
{
    void Awake()
    {
        Debug.Log(gameObject.name + " awaked");
    }

    void Start()
    {
        Debug.Log(gameObject.name + " started");
    }

    private void OnEnable()
    {
        Debug.Log(gameObject.name + " enabled");
    }

    private void OnDisable()
    {
        Debug.Log(gameObject.name + " disabled");
    }
}
