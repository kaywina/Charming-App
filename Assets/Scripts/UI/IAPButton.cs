using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPButton : MonoBehaviour
{
    public string purchaseID;
    public GameObject failIndicator;

    private void OnEnable()
    {
        EventManager.StartListening(UnityIAPController.failedToSubscribePlayerPref, ShowFailIndicator);
        EventManager.StartListening(UnityIAPController.subscribeSuccessPlayerPref, HideFailIndicator);
        HideFailIndicator();
    }

    private void OnDisable()
    {
        EventManager.StopListening(UnityIAPController.failedToSubscribePlayerPref, ShowFailIndicator);
        EventManager.StopListening(UnityIAPController.subscribeSuccessPlayerPref, HideFailIndicator);
    }

    public void MakePurchase()
    {
        // these have been disabled following change from consumables store to gold subscription
        switch (purchaseID)
        {
            case "Gold":
                UnityIAPController.BuyGoldSubscription();
                break;
            /*
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
            */
            default:
                Debug.LogWarning("ProductID not implemented in IAPButton class");
                break;
        }

        return;
    }

    public void ShowFailIndicator()
    {
        if (failIndicator != null) { failIndicator.SetActive(true); }
    }

    public void HideFailIndicator()
    {
        if (failIndicator != null) { failIndicator.SetActive(false); }
    }

}
