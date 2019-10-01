using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPButton : MonoBehaviour
{
    private UnityIAPController iapController;
    public string purchaseID;

    void OnEnable()
    {
        GameObject controllerObject = GameObject.FindGameObjectWithTag("IAPController");
        iapController = controllerObject.GetComponent<UnityIAPController>();
    }

    public void MakePurchase()
    {
        if (iapController == null)
        {
            Debug.LogWarning("IAP Controller is null; aborting purchase");
            return;
        }

        switch (purchaseID)
        {
            case "16":
                iapController.BuyConsumable16();
                break;
            case "32":
                iapController.BuyConsumable32();
                break;
            case "64":
                iapController.BuyConsumable64();
                break;
            case "128":
                iapController.BuyConsumable128();
                break;
            case "256":
                iapController.BuyConsumable256();
                break;
            default:
                Debug.LogWarning("ProductID not implemented in IAPButton class");
                break;
        }

        return;
    }

}
