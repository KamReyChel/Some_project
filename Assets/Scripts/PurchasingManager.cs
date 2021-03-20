using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchasingManager : Singleton<PurchasingManager>, IStoreListener
{
    [SerializeField] private MainMenuController mainMenuController;

    private static IStoreController storeController;

    private static IExtensionProvider extensionProvider;

    private string removeAdsId = "remove_ads";

    // Start is called before the first frame update
    void Start()
    {
        InitializePurchasing();
        
    }

    private void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(removeAdsId, ProductType.NonConsumable);

        ///
        /// Inna metoda na wywołanie fejk'a? Inicjalizacja "słuchacza" oraz konfiguracji.
        /// https://docs.unity3d.com/2018.1/Documentation/ScriptReference/Purchasing.UnityPurchasing.Initialize.html
        ///
        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return storeController != null && extensionProvider != null;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        extensionProvider = extensions;
        Debug.Log("OnInitialized: PASS");
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializedFailed InitializationFailureReason: " + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogFormat("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {

        if(string.Equals(purchaseEvent.purchasedProduct.definition.id, removeAdsId, System.StringComparison.Ordinal))
        {
            Debug.LogFormat("ProcessPurchase: PASS. Product : {0}", purchaseEvent.purchasedProduct.definition.id);
            PlayerPrefs.SetInt("AdsRemoved", 1);
            mainMenuController.SetButtonRemoveAdsVisible(false);
        }
        else
        {
            Debug.LogFormat("ProcessPurchase: FAIL. Unrecognized product : {0}", purchaseEvent.purchasedProduct.definition.id);
        }

        return PurchaseProcessingResult.Complete;
    }

    private void PurchaseProduct(string id)
    {
        if (IsInitialized())
        {
            Product product = storeController.products.WithID(id);

            if (product != null && product.availableToPurchase)
            {
                Debug.LogFormat("Purchasing product asynchronously: {0}", product.definition.id);
                storeController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("PurchaseProduct FAIL. Product not found or not available for purchase.");
            }

        }
        else
        {
            Debug.Log("PurchaseProduct FAIL. Not initialized.");
        }

    }

    public void BuyRemoveAds()
    {
        PurchaseProduct(removeAdsId);
    }

}
