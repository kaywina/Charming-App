using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSourceManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        IronSource.Agent.validateIntegration();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
