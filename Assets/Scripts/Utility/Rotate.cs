using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool yAxis = true;
    public bool xAxis = false;
    public bool zAxis = false;
    public float speed = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        if (yAxis) { transform.Rotate(Vector3.up * step); }
        if (xAxis) { transform.Rotate(Vector3.right * step); }
        if (zAxis) { transform.Rotate(Vector3.back * step); }
    }
}
