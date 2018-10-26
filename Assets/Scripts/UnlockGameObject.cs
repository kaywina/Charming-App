using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockGameObject : MonoBehaviour {

    public GameObject go;
    public int cost;
    public bool lockOnPlay = false;
    
	// Use this for initialization
	void Start () {

        if (lockOnPlay)
        {
            PlayerPrefs.SetString(go.name, "locked");
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
