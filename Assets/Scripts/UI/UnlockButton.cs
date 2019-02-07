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
    public GameObject options;

    void Start () {

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
        unlockPanel.SetObjectToUnlock(go, gameObject, isCharm); // pass in the object to unlock, and the current button object (so can disable it if unlock is confirmed)
        unlockPanel.SetUnlockCost(cost);
        unlockPanel.ShowPanel();

        if (options != null)
        {
            options.SetActive(false);
        }
    }
}
