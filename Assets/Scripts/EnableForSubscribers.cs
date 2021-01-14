using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableForSubscribers : MonoBehaviour
{
    public GameObject[] enableIfFalse;
    public GameObject[] enableIfTrue;

    public void UpdateReferencedObjects()
    {

        if (UnityIAPController.IsGold())
        {
            EnableDisableObjects(true);
        }
        else
        {
            EnableDisableObjects(false);
        }
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        UpdateReferencedObjects();
        EventManager.StartListening(UnityIAPController.subscriptionPurchaseSuccess, UpdateReferencedObjects);
    }

    private void OnDisable()
    {
        EventManager.StopListening(UnityIAPController.subscriptionPurchaseSuccess, UpdateReferencedObjects);
    }

    private void EnableDisableObjects(bool isTrue)
    {
        for (int i = 0; i < enableIfFalse.Length; i++)
        {
            enableIfFalse[i].SetActive(!isTrue);
        }

        for (int i = 0; i < enableIfTrue.Length; i++)
        {
            enableIfTrue[i].SetActive(isTrue);
        }
    }
}
