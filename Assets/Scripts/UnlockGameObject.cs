using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockGameObject : MonoBehaviour {

    public GameObject go;
    public int cost;
    private bool lockOnPlay = false;
    public Text priceText;

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

    public void Unlock()
    {
        if (CurrencyManager.WithdrawAmount(cost))
        {
            PlayerPrefs.SetString(go.name, "unlocked");
            go.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
