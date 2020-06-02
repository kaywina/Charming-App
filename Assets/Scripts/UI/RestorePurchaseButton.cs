using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestorePurchaseButton : MonoBehaviour
{
    public void RestorePurchase()
    {
        GameObject controlObject = GameObject.FindGameObjectWithTag("IAPController");
        controlObject.GetComponent<UnityIAPController>().RestorePurchases();
    }
}
