using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPButton : MonoBehaviour
{
    public string purchaseID;

    public GameObject promo;
    public GameObject success;

    public GameObject enableOnStartPurchase;
    public GameObject enableOnFinishPurchase;
    public GameObject enableOnFailPurchase;

    private void OnEnable()
    {
        EventManager.StartListening(UnityIAPController.failedToSubscribePlayerPref, ShowFailIndicator);
        EventManager.StartListening(UnityIAPController.subscribeSuccessPlayerPref, OnSuccess);
        EventManager.StartListening(UnityIAPController.onStartPurchaseName, OnStartPurchase);
        EventManager.StartListening(UnityIAPController.onFinishPurchaseEventName, OnFinishPurchase);
        EventManager.StartListening(UnityIAPController.onPurchaseFailName, OnFailPurchase);

        enableOnFailPurchase.SetActive(false);

        if (PlayerPrefs.GetString(UnityIAPController.goldSubscriptionPlayerPref) == "true")
        {
            promo.SetActive(false);
            success.SetActive(true);

        }
        else
        {
            promo.SetActive(true);
            success.SetActive(false);
        }

        enableOnStartPurchase.SetActive(false);
        enableOnFinishPurchase.SetActive(false);
        
    }

    private void OnDisable()
    {
        EventManager.StopListening(UnityIAPController.failedToSubscribePlayerPref, ShowFailIndicator);
        EventManager.StopListening(UnityIAPController.subscribeSuccessPlayerPref, OnSuccess);
        EventManager.StopListening(UnityIAPController.onStartPurchaseName, OnStartPurchase);
        EventManager.StopListening(UnityIAPController.onFinishPurchaseEventName, OnFinishPurchase);
        EventManager.StopListening(UnityIAPController.onPurchaseFailName, OnFailPurchase);
    }

    private void OnStartPurchase()
    {
        enableOnStartPurchase.SetActive(true);
        enableOnFinishPurchase.SetActive(false);
    }

    private void OnFinishPurchase()
    {
        enableOnStartPurchase.SetActive(true);
        enableOnFinishPurchase.SetActive(false);
    }

    private void OnFailPurchase()
    {
        enableOnStartPurchase.SetActive(false);
        ShowFailIndicator();
    }

    public void MakePurchase()
    {
        // always deactivate fail indicator as part of iap setup
        enableOnFailPurchase.SetActive(false);

        switch (purchaseID)
        {
            case "Gold":
                UnityIAPController.BuyGoldSubscription();
                break;
            /* // these have been disabled following change from consumables store to gold subscription
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
        Debug.Log("IAP Button receives event message that purchase has failed");
        if (enableOnFailPurchase != null) { enableOnFailPurchase.SetActive(true); }
    }

    public void OnSuccess()
    {
        Debug.Log("IAP Button receives event message that purchase is successfull");
        if (enableOnFailPurchase != null) { enableOnFailPurchase.SetActive(false); }
        if (promo != null) { promo.SetActive(false); }
        if (success != null) { success.SetActive(true); }
    }

}
