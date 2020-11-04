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


        if (PlayerPrefs.GetString(UnityIAPController.goldSubscriptionPlayerPref) == "true")
        {
            if (promo != null) { promo.SetActive(false); }
            if (success != null) { success.SetActive(true); }

        }
        else
        {
            if (promo != null) { promo.SetActive(true); }
            if (success != null) { success.SetActive(false); }
        }

        if (enableOnStartPurchase != null) { enableOnStartPurchase.SetActive(false); }
        if (enableOnFinishPurchase != null) { enableOnFinishPurchase.SetActive(false); }
        if (enableOnFailPurchase != null) { enableOnFailPurchase.SetActive(false); }

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
        if (enableOnStartPurchase != null) { enableOnStartPurchase.SetActive(true); }
        if (enableOnFinishPurchase != null) { enableOnFinishPurchase.SetActive(false); }
    }

    private void OnFinishPurchase()
    {
        if (enableOnStartPurchase != null) { enableOnStartPurchase.SetActive(true); }
        if (enableOnFinishPurchase != null) { enableOnFinishPurchase.SetActive(false); }
    }

    private void OnFailPurchase()
    {
        if (enableOnStartPurchase != null) { enableOnStartPurchase.SetActive(false); }
        ShowFailIndicator();
    }

    public void MakePurchase()
    {
        // always deactivate fail indicator as part of iap setup
        if (enableOnFailPurchase != null) { enableOnFailPurchase.SetActive(false); }

        switch (purchaseID)
        {
            case "Gold":
                UnityIAPController.BuyGoldSubscription();
                break;
            // these have been disabled following change from consumables store to gold subscription
            case "16":
                UnityIAPController.BuyConsumable16();
                break;
            case "32":
                UnityIAPController.BuyConsumable32();
                break;
            case "64":
                UnityIAPController.BuyConsumable64();
                break;
            case "128":
                UnityIAPController.BuyConsumable128();
                break;
            case "256":
                UnityIAPController.BuyConsumable256();
                break;
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
