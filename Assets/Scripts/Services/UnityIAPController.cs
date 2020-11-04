using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class UnityIAPController : MonoBehaviour, IStoreListener
{
    public static string goldSubscriptionPlayerPref = "Gold";
    public SetPlayerPrefFromToggle goldTogglePrefab;

    public static string failedToSubscribePlayerPref = "GoldFail";
    public static string subscribeSuccessPlayerPref = "GoldSuccess";

    public static string onStartPurchaseName = "onStartPurchase";
    public static string onFinishPurchaseEventName = "onFinishPurchase";
    public static string onPurchaseFailName = "onPurchaseFail";

    public static string onStartRestoreEventName = "onStartRestore";
    public static string onFinishRestoreEventName = "onFinishRestore";
    public static string onFailRestoreEventName = "onFailedRestore";


    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    private ITransactionHistoryExtensions m_TransactionHistoryExtensions;
#if UNITY_ANDROID
    private IGooglePlayStoreExtensions m_GooglePlayStoreExtensions;
#elif UNITY_IOS
    private IAppleExtensions m_AppleExtensions;
#endif

    private SubscriptionInfo info;
    private static bool m_PurchaseInProgress;

    private static string localizedPricePlayerPrefName = "GoldPrice";

    // Product identifiers for all products capable of being purchased: 
    // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
    // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
    // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

    // General product identifiers for the consumable, non-consumable, and subscription products.
    // Use these handles in the code to reference which product to purchase. Also use these values 
    // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
    // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
    // specific mapping to Unity Purchasing's AddProduct, below.
#if UNITY_ANDROID
    public static string consumable16 = "16";
    public static string consumable32 = "32";
    public static string consumable64 = "64";
    public static string consumable128 = "128";
    public static string consumable256 = "256";
#elif UNITY_IOS
    public static string consumable16 = "16_keys";
    public static string consumable32 = "32_keys";
    public static string consumable64 = "64_keys";
    public static string consumable128 = "128_keys";
    public static string consumable256 = "256_keys";
#endif

    //public static string kProductIDNonConsumable = "nonconsumable";

    public static string kProductIDSubscription = "Gold";

    // Apple App Store-specific product identifier for the subscription product.
    private static string kProductNameAppleSubscription = "com.charmingape.charmingapp.goldios";

    // Google Play Store-specific product identifier subscription product.
    private static string kProductNameGooglePlaySubscription = "com.charmingape.charmingapp.gold";

    void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }

        goldTogglePrefab.SetPlayerPrefName(goldSubscriptionPlayerPref); //to-do: clean up this UI dependency
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.
        builder.AddProduct(consumable16, ProductType.Consumable);
        builder.AddProduct(consumable32, ProductType.Consumable);
        builder.AddProduct(consumable64, ProductType.Consumable);
        builder.AddProduct(consumable128, ProductType.Consumable);
        builder.AddProduct(consumable256, ProductType.Consumable);

        // Continue adding the non-consumable product.
        //builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);

        // And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
        // if the Product ID was configured differently between Apple and Google stores. Also note that
        // one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
        // must only be referenced here. 

        builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
            { kProductNameAppleSubscription, AppleAppStore.Name },
            { kProductNameGooglePlaySubscription, GooglePlay.Name },
        });

        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }

    private static bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    /*
     * Methods called by store buttons
     */
    public static void BuyConsumable16()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(consumable16);
    }
    public static void BuyConsumable32()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(consumable32);
    }
    public static void BuyConsumable64()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(consumable64);
    }
    public static void BuyConsumable128()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(consumable128);
    }
    public static void BuyConsumable256()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(consumable256);
    }

    /*
     * Additional button methods for non-consumable products
     */

    /*
    public void BuyNonConsumable()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(kProductIDNonConsumable);
    }
    */

    /*
     * Additional button methods for subscription products
     */

    public static void BuyGoldSubscription()
    {
        // Buy the subscription product using its the general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        // Notice how we use the general product identifier in spite of this ID being mapped to
        // custom store-specific identifiers above.
        BuyProductID(kProductIDSubscription);
        //BuyProductID("this_should_fail");
    }

    static void BuyProductID(string productId)
    {
        if (m_PurchaseInProgress == true)
        {
            Debug.Log("Purchase is already in progress. Do not proceed with buying a new product");
            return;
        }

        // If Purchasing has not been initialized ...
        if (!IsInitialized())
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
            EventManager.TriggerEvent(failedToSubscribePlayerPref);
            return;
        }

        EventManager.TriggerEvent(onStartPurchaseName);

        // ... look up the Product reference with the general product identifier and the Purchasing 
        // system's products collection.
        Product product = m_StoreController.products.WithID(productId);

        // If the look up found a product for this device's store and that product is ready to be sold ... 
        if (product != null && product.availableToPurchase)
        {
            Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
            // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
            // asynchronously.
            m_StoreController.InitiatePurchase(product);
        }
        // Otherwise ...
        else
        {
            // ... report the product look-up failure situation  
            Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            EventManager.TriggerEvent(failedToSubscribePlayerPref);
            return;
        }

        EventManager.TriggerEvent(onFinishPurchaseEventName);
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        EventManager.TriggerEvent(onStartRestoreEventName);

#if UNITY_ANDROID
        Debug.Log("Restore transactions on Google Play");
        m_GooglePlayStoreExtensions.RestoreTransactions(OnTransactionsRestored);

#elif UNITY_IOS
        Debug.Log("Restore transactions on the App Store");
        m_AppleExtensions.RestoreTransactions(OnTransactionsRestored);
#else
        Debug.Log("Restoring transactions is not supported on this platform.");
        return;
#endif

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            //Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
                if (result)
                {
                    //Debug.Log("Completed restore purchase.");
                    EventManager.TriggerEvent(onFinishRestoreEventName);
                }
            });
        }
        // Otherwise the restore process has failed
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            EventManager.TriggerEvent(onFailRestoreEventName);
            return;
        }

    }

    /// <summary>
    /// This will be called after a call to IAppleExtensions.RestoreTransactions().
    /// </summary>
    private void OnTransactionsRestored(bool success)
    {
        Debug.Log("Transactions restored. Success flag = " + success);

#if UNITY_ANDROID
        EventManager.TriggerEvent(onFinishRestoreEventName); // always just say finished on Android regardless of outcome
#endif
    }

    //  
    // --- IStoreListener
    //
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;

        m_TransactionHistoryExtensions = extensions.GetExtension<ITransactionHistoryExtensions>();

#if UNITY_ANDROID
        m_GooglePlayStoreExtensions = extensions.GetExtension<IGooglePlayStoreExtensions>();
#elif UNITY_IOS
        m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();
#endif

#if UNITY_ANDROID
        Dictionary<string, string> introductory_info_dict = m_GooglePlayStoreExtensions.GetProductJSONDictionary();
#elif UNITY_IOS
        Dictionary<string, string> introductory_info_dict = m_AppleExtensions.GetIntroductoryPriceDictionary();
#endif


        //Debug.Log("Available items:");
        foreach (Product item in controller.products.all)
        {
            if (item.availableToPurchase)
            {

#if UNITY_EDITOR
                string goldProductID = kProductIDSubscription;
#elif UNITY_ANDROID
                string goldProductID = kProductNameGooglePlaySubscription;
#elif UNITY_IOS
                string goldProductID = kProductNameAppleSubscription;
#else
                Debug.Log("No product ID for this platform");
                // no product id assigned should cause build error on unsupported platforms intentionally
#endif

                // The case for Charming App Gold Subscription
                if (item.definition.storeSpecificId == goldProductID)
                {
                    string localizedPrice = item.metadata.localizedPriceString;
                    //Debug.Log("localizedPrice for " + goldProductID + " is " + localizedPrice);
                    PlayerPrefs.SetString(localizedPricePlayerPrefName, item.metadata.localizedPriceString);
                }
                
                /*
                Debug.Log(string.Join(" - ",
                    new[]
                    {
                        item.metadata.localizedTitle,
                        item.metadata.localizedDescription,
                        item.metadata.isoCurrencyCode,
                        item.metadata.localizedPrice.ToString(),
                        item.metadata.localizedPriceString,
                        item.transactionID,
                        item.receipt
                    }));
                
                */
                // this is the usage of SubscriptionManager class
                if (item.receipt != null)
                {
                    if (item.definition.type == ProductType.Subscription)
                    {
                        if (checkIfProductIsAvailableForSubscriptionManager(item.receipt))
                        {
                            string intro_json = (introductory_info_dict == null || !introductory_info_dict.ContainsKey(item.definition.storeSpecificId)) ? null : introductory_info_dict[item.definition.storeSpecificId];
                            SubscriptionManager p = new SubscriptionManager(item, intro_json);
                            info = p.getSubscriptionInfo();
                            /*
                            Debug.Log("product id is: " + info.getProductId());
                            Debug.Log("purchase date is: " + info.getPurchaseDate());
                            Debug.Log("subscription next billing date is: " + info.getExpireDate());
                            Debug.Log("is subscribed? " + info.isSubscribed().ToString());
                            Debug.Log("is expired? " + info.isExpired().ToString());
                            Debug.Log("is cancelled? " + info.isCancelled());
                            Debug.Log("product is in free trial peroid? " + info.isFreeTrial());
                            Debug.Log("product is auto renewing? " + info.isAutoRenewing());
                            Debug.Log("subscription remaining valid time until next billing date is: " + info.getRemainingTime());
                            Debug.Log("is this product in introductory price period? " + info.isIntroductoryPricePeriod());
                            Debug.Log("the product introductory localized price is: " + info.getIntroductoryPrice());
                            Debug.Log("the product introductory price period is: " + info.getIntroductoryPricePeriod());
                            Debug.Log("the number of product introductory price period cycles is: " + info.getIntroductoryPricePeriodCycles());
                            */
                            ValidateSubscription(info);

                        }
                        else
                        {
                            Debug.Log("This product is not available for SubscriptionManager class, only products that are purchase by 1.19+ SDK can use this class.");
                        }
                    }
                    else
                    {
                        Debug.Log("the product is not a subscription product");
                    }
                }
                else
                {
                    //Debug.Log("the product should have a valid receipt");
                    CheckForCancelledSubscription(item);

                }
            }
        }
    }

    private void CheckForCancelledSubscription(Product product)
    {
        // disable gold if there is not valid receipt for the subscription
        // this should capture case where the subscription has been cancelled
        bool disableGold = false;
        string id = product.definition.storeSpecificId;
#if UNITY_ANDROID
        if (id == kProductNameGooglePlaySubscription) { disableGold = true; }
#elif UNITY_IOS
        if (id == kProductNameAppleSubscription) { disableGold = true; }
#endif
        if (disableGold) { PlayerPrefs.SetString(goldSubscriptionPlayerPref, "false"); }
    }

    // Validate the Charming App Gold Subscription
    private void ValidateSubscription(SubscriptionInfo i)
    {
        string productIdFromInfo;
        productIdFromInfo = i.getProductId();

        if (string.IsNullOrEmpty(productIdFromInfo))
        {
            Debug.Log("Product ID is null or void; exiting validation");
            return;
        }

        bool isValid = false;
#if UNITY_ANDROID
        if (i.getProductId() == kProductNameGooglePlaySubscription) { isValid = true; }
#elif UNITY_IOS
        if (i.getProductId() == kProductNameAppleSubscription) { isValid = true; }
#else
        Debug.Log("IAP is not supported for this platform, no validation to perform");
#endif

        if (isValid == false)
        {
            Debug.Log("Product ID exist but is not valid, exiting validation");
            return;
        }

        // set the player pref flag for gold toggle based on if the user is subscribed or not
        switch (i.isSubscribed())
        {
            case Result.True:
                Debug.Log("This user is subscribed to Charming App Gold");
                PlayerPrefs.SetString(goldSubscriptionPlayerPref, "true");
                break;
            case Result.False:
                Debug.Log("This user is not subscribed to Charming App Gold");
                PlayerPrefs.SetString(goldSubscriptionPlayerPref, "false");
                break;
            case Result.Unsupported:
                Debug.Log("Hit unsupported case in Charming App Gold validation check");
                PlayerPrefs.SetString(goldSubscriptionPlayerPref, "false");
                break;
            default:
                Debug.Log("Hit default case in subscription check; this message should not appear");
                PlayerPrefs.SetString(goldSubscriptionPlayerPref, "false");
                break;
        }
    }

    public static string GetLocalizedPricePlayerPrefName()
    {
        return localizedPricePlayerPrefName;
    }

    private bool checkIfProductIsAvailableForSubscriptionManager(string receipt) {
        var receipt_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(receipt);
        if (!receipt_wrapper.ContainsKey("Store") || !receipt_wrapper.ContainsKey("Payload")) {
            Debug.Log("The product receipt does not contain enough information");
            return false;
        }
        var store = (string)receipt_wrapper ["Store"];
        var payload = (string)receipt_wrapper ["Payload"];

        if (payload != null ) {
            switch (store) {
            case GooglePlay.Name:
                {
                    var payload_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(payload);
                    if (!payload_wrapper.ContainsKey("json")) {
                        Debug.Log("The product receipt does not contain enough information, the 'json' field is missing");
                        return false;
                    }
                    var original_json_payload_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode((string)payload_wrapper["json"]);
                    if (original_json_payload_wrapper == null || !original_json_payload_wrapper.ContainsKey("developerPayload")) {
                        Debug.Log("The product receipt does not contain enough information, the 'developerPayload' field is missing");
                        return false;
                    }
                    var developerPayloadJSON = (string)original_json_payload_wrapper["developerPayload"];
                    var developerPayload_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(developerPayloadJSON);
                    if (developerPayload_wrapper == null || !developerPayload_wrapper.ContainsKey("is_free_trial") || !developerPayload_wrapper.ContainsKey("has_introductory_price_trial")) {
                        Debug.Log("The product receipt does not contain enough information, the product is not purchased using 1.19 or later");
                        return false;
                    }
                    return true;
                }
            case AppleAppStore.Name:
                {
                    return true;
                }

            case AmazonApps.Name:
            case MacAppStore.Name:
                {
                    return true;
                }
            default:
                {
                    return false;
                }
            }
        }
        return false;
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // Or ... a subscription product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, kProductIDSubscription, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerPrefs.SetString(goldSubscriptionPlayerPref, "true"); // if the gold subscription has been restored set the player pref
            EventManager.TriggerEvent(subscribeSuccessPlayerPref);
        }
        // Cases where a consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, consumable16, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased
            CurrencyManager.Instance.GiveBonus(16, true);
        }
        // A consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, consumable32, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased
            CurrencyManager.Instance.GiveBonus(32, true);
        }
        // A consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, consumable64, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased
            CurrencyManager.Instance.GiveBonus(64, true);
        }
        // A consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, consumable128, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased
            CurrencyManager.Instance.GiveBonus(128, true);
        }
        // A consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, consumable256, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased
            CurrencyManager.Instance.GiveBonus(256, true);
        }
        /*
        // Or ... a non-consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
        }
        */

        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        // else if product not found
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// This will be called if an attempted purchase fails.
    /// </summary>
    public void OnPurchaseFailed(Product item, PurchaseFailureReason r)
    {
        Debug.Log("Purchase failed: " + item.definition.id);
        Debug.Log(r);

        // Detailed debugging information
        Debug.Log("Store specific error code: " + m_TransactionHistoryExtensions.GetLastStoreSpecificPurchaseErrorCode());
        if (m_TransactionHistoryExtensions.GetLastPurchaseFailureDescription() != null)
        {
            Debug.Log("Purchase failure description message: " +
                      m_TransactionHistoryExtensions.GetLastPurchaseFailureDescription().message);
        }

        m_PurchaseInProgress = false;

        EventManager.TriggerEvent(onPurchaseFailName);
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Billing failed to initialize!");
        switch (error)
        {
            case InitializationFailureReason.AppNotKnown:
                Debug.LogError("IAP initialization error AppNotKnown. Is the App correctly uploaded on the relevant publisher console?");
                break;
            case InitializationFailureReason.PurchasingUnavailable:
                // Ask the user if billing is disabled in device settings.
                Debug.Log("IAP initialization error PurchasingUnvailable. Is billing disabled in the user's device settings?");
                break;
            case InitializationFailureReason.NoProductsAvailable:
                // Developer configuration error; check product metadata.
                Debug.Log("IAP initialization error NoProductsAvailable. Have the IAP products been setup on the relevant publisher console?");
                break;
            default:
                Debug.Log("Hit default case in IAP OnInitializeFailed. This message should not be shown.");
                break;
        }
    }

    /// <summary>
    /// iOS Specific.
    /// This is called as part of Apple's 'Ask to buy' functionality,
    /// when a purchase is requested by a minor and referred to a parent
    /// for approval.
    ///
    /// When the purchase is approved or rejected, the normal purchase events
    /// will fire.
    /// </summary>
    /// <param name="item">Item.</param>
    private void OnDeferred(Product item)
    {
        Debug.Log("Purchase deferred: " + item.definition.id);
    }
}