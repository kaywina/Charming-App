using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateForSeconds : MonoBehaviour
{
    public float secondsToDeactivation = 3f;
    public GameObject objectToDeactivate;

    public void DeactivateForXSeconds()
    {
        objectToDeactivate.SetActive(false);
        Invoke("Reactivate", secondsToDeactivation);
    }

    private void Reactivate()
    {
        objectToDeactivate.SetActive(true);
    }
}
