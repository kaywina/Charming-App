using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUntilDeactivated : MonoBehaviour
{

    public string PlayerPrefName = "UniqueStringValue"; // replace with a unique player pref name in inspector


    void Start()
    {
        if (PlayerPrefs.GetInt(PlayerPrefName) != 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Deactivate()
    {
        PlayerPrefs.SetInt(PlayerPrefName, 1);
        gameObject.SetActive(false);
    }
}
