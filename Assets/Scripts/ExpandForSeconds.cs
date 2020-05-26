using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandForSeconds : MonoBehaviour
{
    public GameObject toExpand;

    public bool x = true;
    public bool y = true;
    public bool z = false;

    public float secondsToExpand = 3f;

    public float sizeIncrement = 1.0f;

    private bool isExpanding = false;

    private Vector3 originalLocalScale;
    
    void OnEnable()
    {
        originalLocalScale = toExpand.transform.localScale;
    }

    private void OnDisable()
    {
        toExpand.transform.localScale = originalLocalScale;
    }

    void FixedUpdate()
    {
        if (isExpanding)
        {
            Vector3 tempScale = new Vector3();

            if (x) { tempScale.x = toExpand.transform.localScale.x + sizeIncrement; }
            if (y) { tempScale.y = toExpand.transform.localScale.y + sizeIncrement; }
            if (z) { tempScale.z = toExpand.transform.localScale.z + sizeIncrement; }

            toExpand.transform.localScale = tempScale;
        }
    }

    public void Expand()
    {
        isExpanding = true;
        Invoke("StopExpanding", secondsToExpand);
    }

    private void StopExpanding()
    {
        isExpanding = false;
        toExpand.transform.localScale = originalLocalScale;
    }
}
