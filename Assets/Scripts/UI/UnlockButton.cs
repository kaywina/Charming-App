using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockButton : MonoBehaviour {

    public GameObject go;
    public int cost;
    private bool lockOnPlay = false;
    public Text priceText;
    public UnlockPanel unlockPanel;
    public CharmsPanel optionsPanel;
    public bool usePremiumCurrency = false;

    public MeshRenderer meshRenderer;
    public Material silverMat;
    public Material goldMat;

    void Start () {

        if (usePremiumCurrency)
        {
            if (meshRenderer != null) { meshRenderer.material = goldMat; }
        }
        else
        {
            if (meshRenderer != null) { meshRenderer.material = silverMat; }
        }

        priceText.text = cost.ToString();

        if (lockOnPlay)
        {
            PlayerPrefs.SetString(go.name, "locked");
            Debug.LogWarning("Warning! lockOnPlay is true");
        }

        if (PlayerPrefs.GetString(go.name) == "unlocked")
        {
            go.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            go.SetActive(false);
        }
	}

    public void GoToConfirmation(bool isCharm = true)
    {
        unlockPanel.SetObjectToUnlock(go, gameObject, isCharm, usePremiumCurrency); // pass in the object to unlock, and the current button object (so can disable it if unlock is confirmed)
        unlockPanel.SetUnlockCost(cost);
        unlockPanel.ShowPanel();

        if (optionsPanel != null)
        {
            optionsPanel.SetReturnToMain(false);
            optionsPanel.gameObject.SetActive(false);
        }
    }
}
