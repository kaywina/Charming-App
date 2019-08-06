using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockButton : MonoBehaviour {

    public GameObject objectToEnable;
    public int cost;
    private bool lockOnPlay = false;
    public Text priceText;
    public UnlockPanel unlockPanel;
    public CharmsPanel optionsPanel;
    public bool usePremiumCurrency = false;
    public bool cameFromOptionsPanel = false;

    // leave these three variables unassigned in inspector if button is 2D with no 3D model (i.e. these are only used for 3D UI buttons only)
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
            PlayerPrefs.SetString(objectToEnable.name, "locked");
            Debug.LogWarning("Warning! lockOnPlay is true");
        }

        if (PlayerPrefs.GetString(objectToEnable.name) == "unlocked")
        {
            objectToEnable.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            objectToEnable.SetActive(false);
        }
	}

    public void GoToConfirmation(bool isCharm = true)
    {
        unlockPanel.SetObjectToUnlock(objectToEnable, gameObject, isCharm, usePremiumCurrency, cameFromOptionsPanel); // pass in the object to unlock, and the current button object (so can disable it if unlock is confirmed)
        unlockPanel.SetUnlockCost(cost);
        unlockPanel.ShowPanel();

        if (optionsPanel != null)
        {
            optionsPanel.SetReturnToMain(false);
            optionsPanel.gameObject.SetActive(false);
        }
    }
}
