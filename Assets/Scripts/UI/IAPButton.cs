using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAPButton : MonoBehaviour
{
    public string purchaseID;
    public Button button;

    // these are used to handle UI change on subscription panel
    public GameObject promo;
    public GameObject success;

    // events shared between all iap buttons
    public GameObject enableOnStartPurchase;
    public GameObject enableOnFailPurchase;
    public GameObject enableOnSuccessfulPurchase;

    // currency indicator is used on store panel
    public CurrencyIndicator currencyIndicator;

    public enum BUTTON_TYPE { Subscribe, Consumable };
    public BUTTON_TYPE buttonType = BUTTON_TYPE.Consumable;

    private void OnEnable()
    {
        button.interactable = true; // edge case - button will be interactable if user goes out of store panel while transaction in progress then returns after

        if (promo != null) { promo.SetActive(true); }
        if (success != null) { success.SetActive(false); }

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
        EventManager.StartListening(UnityIAPController.onPurchaseStart, onStartPurchase);
        EventManager.StartListening(UnityIAPController.onPurchaseComplete, OnPurchaseComplete);
        EventManager.StartListening(UnityIAPController.onPurchaseFail, OnFailPurchase);

        if (enableOnStartPurchase != null) { enableOnStartPurchase.SetActive(false); }
        if (enableOnFailPurchase != null) { enableOnFailPurchase.SetActive(false); }
        if (enableOnSuccessfulPurchase != null) { enableOnSuccessfulPurchase.SetActive(false); }
    }

    private void OnDisable()
    {
        // different code paths for success on subscription vs consumable products; so EnableFromPlayerPrefToggle is only updated in case of subscription purchase not consumable
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

        EventManager.StopListening(UnityIAPController.onPurchaseStart, onStartPurchase);
        EventManager.StopListening(UnityIAPController.onPurchaseComplete, OnPurchaseComplete);
        EventManager.StopListening(UnityIAPController.onPurchaseFail, OnFailPurchase);
    }

    private void onStartPurchase()
    {
        button.interactable = false;
        if (enableOnStartPurchase != null) { enableOnStartPurchase.SetActive(true); }
    }

    private void OnFailPurchase()
    {
        button.interactable = false;
        if (enableOnFailPurchase != null) { enableOnFailPurchase.SetActive(true); }
    }

    public void OnSuccess()
    {
        //Debug.Log("IAP Button receives event message that purchase is successful");
        if (promo != null) { promo.SetActive(false); }
        if (success != null) { success.SetActive(true); }
        if (currencyIndicator != null) { currencyIndicator.UpdateIndicator(); }
        if (enableOnSuccessfulPurchase != null) { enableOnSuccessfulPurchase.SetActive(true); }
        Debug.Log("Success");
    }

    // this fires after OnSuccess
    private void OnPurchaseComplete()
    {
        if (enableOnStartPurchase != null) { enableOnStartPurchase.SetActive(false); }
        button.interactable = true;
    }

    public void MakePurchase()
    {
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
}
