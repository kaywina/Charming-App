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

    public CurrencyIndicator currencyIndicator;

    public enum BUTTON_TYPE { Subscribe, Consumable };
    public BUTTON_TYPE buttonType = BUTTON_TYPE.Consumable;

    private void OnEnable()
    {
        switch (buttonType) {
            case BUTTON_TYPE.Subscribe:
                EventManager.StartListening(UnityIAPController.subscriptionPurchaseSuccess, OnSuccess);
                break;
            case BUTTON_TYPE.Consumable:
                EventManager.StartListening(UnityIAPController.consumablePurchaseSuccess, OnSuccess);
                break;
            default:
                EventManager.StartListening(UnityIAPController.consumablePurchaseSuccess, OnSuccess);
                break;
        }
        EventManager.StartListening(UnityIAPController.onPurchaseStart, OnStartPurchase);
        EventManager.StartListening(UnityIAPController.onPurchaseFinish, OnFinishPurchase);
        EventManager.StartListening(UnityIAPController.onPurchaseFail, OnFailPurchase);

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
        // different code paths for success on subscription vs consumable products
        switch (buttonType)
        {
            case BUTTON_TYPE.Subscribe:
                EventManager.StopListening(UnityIAPController.subscriptionPurchaseSuccess, OnSuccess);
                break;
            case BUTTON_TYPE.Consumable:
                EventManager.StopListening(UnityIAPController.consumablePurchaseSuccess, OnSuccess);
                break;
            default:
                EventManager.StopListening(UnityIAPController.consumablePurchaseSuccess, OnSuccess);
                break;
        }

        EventManager.StopListening(UnityIAPController.onPurchaseStart, OnStartPurchase);
        EventManager.StopListening(UnityIAPController.onPurchaseFinish, OnFinishPurchase);
        EventManager.StopListening(UnityIAPController.onPurchaseFail, OnFailPurchase);
    }

    private void OnStartPurchase()
    {
        if (enableOnStartPurchase != null) { enableOnStartPurchase.SetActive(true); }
        if (enableOnFinishPurchase != null) { enableOnFinishPurchase.SetActive(false); }
    }

    private void OnFinishPurchase()
    {
        if (enableOnFailPurchase != null) { enableOnFailPurchase.SetActive(false); }
        if (enableOnStartPurchase != null) { enableOnStartPurchase.SetActive(false); }
        if (enableOnFinishPurchase != null) { enableOnFinishPurchase.SetActive(true); }
    }

    private void OnFailPurchase()
    {
        if (enableOnStartPurchase != null) { enableOnStartPurchase.SetActive(false); }
        if (enableOnFinishPurchase != null) { enableOnFinishPurchase.SetActive(false); }
        if (enableOnFailPurchase != null) { enableOnFailPurchase.SetActive(true); }
    }

    public void MakePurchase()
    {
        // always deactivate fail indicator as part of iap setup
        if (enableOnFailPurchase != null) { enableOnFailPurchase.SetActive(false); }

        switch (purchaseID)
        {
            // subscription
            case "Gold":
                UnityIAPController.BuyGoldSubscription();
                break;
            
            // consumables
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
            
            // this should not be reached
            default:
                Debug.LogWarning("ProductID not implemented in IAPButton class");
                break;
        }

        return;
    }

    public void OnSuccess()
    {
        //Debug.Log("IAP Button receives event message that purchase is successful");
        if (promo != null) { promo.SetActive(false); }
        if (success != null) { success.SetActive(true); }
        if (currencyIndicator != null) { currencyIndicator.UpdateIndicator(); }
    }

}
