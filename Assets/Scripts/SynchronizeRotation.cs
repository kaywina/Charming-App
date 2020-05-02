using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizeRotation : MonoBehaviour
{
    public Transform targetTransform;

    private void OnEnable()
    {
        // get rotation from target object
        transform.rotation = targetTransform.rotation;
    }

    private void OnDisable()
    {
        // assign rotation to target object
        targetTransform.rotation = transform.rotation;
    }
}
