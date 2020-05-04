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
    }

    private void SetRank()
    {
        if (!PlayerPrefs.HasKey(rankPlayerPref))
        {
            PlayerPrefs.SetInt(rankPlayerPref, 0);
            DisableAllRankTextObjects();
            EnableRankTextObjectByIndex(0);
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
            Debug.LogError("Error: rank index exceeds lenght of rank text objects array");
            return;
        }

        DisableAllRankTextObjects();
        EnableRankTextObjectByIndex(index);
        Debug.Log("Rank successfully set from data");
    }
}
