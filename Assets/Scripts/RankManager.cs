using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    public GameObject[] rankTextObjects;

    private string rankPlayerPref = "Rank";  // don't change this in production
    
    // Start is called before the first frame update
    void Start()
    {
        SetRank();

        // TODO:
        /*
         * RankManager needs to track the number of check-ins, store it in a player pref, and increase the rank accordingly
         * 
         * */
    }

    private void SetRank()
    {
        if (!PlayerPrefs.HasKey(rankPlayerPref))
        {
            PlayerPrefs.SetInt(rankPlayerPref, 0);
            DisableAllRankTextObjects();
            EnableRankTextObjectByIndex(0);
            Debug.Log("No rank set; starting at index 0 unranked");
            return;
        }

        SetRankFromPlayerPref(); // set the rank from data if we get this far
    }

    private void DisableAllRankTextObjects()
    {
        for (int i = 0; i < rankTextObjects.Length; i++)
        {
            rankTextObjects[i].SetActive(false);
        }
    }

    private void EnableRankTextObjectByIndex(int index)
    {
        rankTextObjects[index].SetActive(true);
    }

    private void SetRankFromPlayerPref()
    {
        int index = PlayerPrefs.GetInt(rankPlayerPref);
        if (index >= rankTextObjects.Length)
        {
            Debug.LogError("Error: rank index exceeds length of rank text objects array");
            return;
        }

        DisableAllRankTextObjects();
        EnableRankTextObjectByIndex(index);
        Debug.Log("Rank successfully set from data");
    }
}
