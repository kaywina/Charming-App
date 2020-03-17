using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFromPlayerPrefToggle : MonoBehaviour
{
    public SetPlayerPrefFromToggle togglePrefab;
    public GameObject[] enableIfFalse;
    public GameObject[] enableIfTrue;

    public void UpdateReferencedObjects()
    {
        string playerPref = PlayerPrefs.GetString(togglePrefab.GetPlayerPrefName());
        Debug.Log("playerPref is " + playerPref);

        if (!PlayerPrefs.HasKey(togglePrefab.GetPlayerPrefName()) || playerPref == "false")
        {
            EnableDisableObjects(false);
            Debug.Log("No key or playerpref is false for " + togglePrefab.GetPlayerPrefName());
        }
        else
        {
            EnableDisableObjects(true);
            Debug.Log("Key exists and playerpref is true" + togglePrefab.GetPlayerPrefName());
        }
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log("Enable or disable objects for playerpref " + togglePrefab.GetPlayerPrefName());
        UpdateReferencedObjects();
        EventManager.StartListening(UnityIAPController.subscribeSuccessPlayerPref, UpdateReferencedObjects);
    }

    private void OnDisable()
    {
        EventManager.StopListening(UnityIAPController.subscribeSuccessPlayerPref, UpdateReferencedObjects);
    }

    private void EnableDisableObjects(bool isTrue)
    {
        for (int i = 0; i < enableIfFalse.Length; i++)
        {
            enableIfFalse[i].SetActive(!isTrue);
        }

        for (int i = 0; i < enableIfTrue.Length; i++)
        {
            enableIfTrue[i].SetActive(isTrue);
        }
    }

}
