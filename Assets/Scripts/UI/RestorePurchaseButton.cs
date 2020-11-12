using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestorePurchaseButton : MonoBehaviour
{

    public Button restorePurchaseButton;
    public GameObject enableWhileProcessing;
    public GameObject enableWhenFailed;
    public GameObject enableWhenSuccessful;

    public void RestorePurchase()
    {
        GameObject controlObject = GameObject.FindGameObjectWithTag("IAPController");
        controlObject.GetComponent<UnityIAPController>().RestorePurchases();
    }

    private void DisableAllDisplayObjects()
    {
        enableWhileProcessing.SetActive(false);
        enableWhenFailed.SetActive(false);
        enableWhenSuccessful.SetActive(false);
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        EventManager.StartListening(UnityIAPController.onRestoreStart, OnStartRestoreProcess);
        EventManager.StartListening(UnityIAPController.onRestoreFinish, OnFinishRestoreProcess);
        EventManager.StartListening(UnityIAPController.onRestoreFail, OnFailRestorePurchase);
        EventManager.StartListening(UnityIAPController.onRestoreSuccess, OnSuccessfulRestorePurchase);

        DisableAllDisplayObjects();
        restorePurchaseButton.interactable = true;
    }

    private void OnDisable()
    {
        EventManager.StopListening(UnityIAPController.onRestoreStart, OnStartRestoreProcess);
        EventManager.StopListening(UnityIAPController.onRestoreFinish, OnFinishRestoreProcess);
        EventManager.StopListening(UnityIAPController.onRestoreFail, OnFailRestorePurchase);
    }

    private void OnStartRestoreProcess()
    {
        restorePurchaseButton.interactable = false;

        enableWhenSuccessful.SetActive(false);
        enableWhenFailed.SetActive(false);
        enableWhileProcessing.SetActive(true);
    }

    private void OnFinishRestoreProcess()
    {
        restorePurchaseButton.interactable = true;
    }

    private void OnFailRestorePurchase()
    {
        restorePurchaseButton.interactable = true;

        enableWhileProcessing.SetActive(false);
        enableWhenSuccessful.SetActive(false);
        enableWhenFailed.SetActive(true);
    }

    private void OnSuccessfulRestorePurchase()
    {
        enableWhileProcessing.SetActive(false);
        enableWhenFailed.SetActive(false);
        enableWhenSuccessful.SetActive(true);
    }
}
