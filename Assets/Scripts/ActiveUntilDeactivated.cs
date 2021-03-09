using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUntilDeactivated : MonoBehaviour
{

    public string PlayerPrefName = "UniqueStringValue"; // replace with a unique player pref name in inspector
    public ActiveUntilDeactivated toReactivateOnDeactivate;
    private bool deactivated = false;


    void Start()
    {

        if (toReactivateOnDeactivate != null) { toReactivateOnDeactivate.gameObject.SetActive(false); }

        if (PlayerPrefs.GetInt(PlayerPrefName) != 0)
        {
            gameObject.SetActive(false);
            deactivated = true;
        }
    }

    public void Deactivate()
    {
        if (deactivated) { return; }

        PlayerPrefs.SetInt(PlayerPrefName, 1);
        gameObject.SetActive(false);
        deactivated = true;
        if (toReactivateOnDeactivate != null) { toReactivateOnDeactivate.Reactivate(); }
    }

    public void Reactivate()
    {
        //Debug.Log("Reactivate");
        PlayerPrefs.SetInt(PlayerPrefName, 0);
        gameObject.SetActive(true);
    }
}
