using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    public GameObject[] rankTextObjects;

    private string daysPlayerPref = "RankDays";

    // Start is called before the first frame update
    void Start()
    {
        int newDays = 0;
        if (PlayerPrefs.HasKey(daysPlayerPref))
        {
            newDays = PlayerPrefs.GetInt(daysPlayerPref);
        }
        else
        {
            PlayerPrefs.SetInt(daysPlayerPref, 0);
        }

        // TODO:
        /*
         * RankManager needs to track the number of check-ins, store it in a player pref, and increase the rank accordingly
         * 
         * */
        if (TimeManager.IsNewDay(TimeManager.TimeType.DailySpin))
        {
            newDays++;
            PlayerPrefs.SetInt(daysPlayerPref, newDays);
            Debug.Log("Total days checked in = " + newDays);
        }

        SetRank(newDays);
    }
    private void SetRank(int days)
    {
        DisableAllRankTextObjects();
        Debug.Log("Set a rank based on number of unique daily spin days");

        int rankIndex = 0;
        
        // match number of rank cases to length of rankTextObjects array
        if (days >= 2)
        {
            rankIndex = 1; // amateur
        }
        if (days >= 4)
        {
            rankIndex = 2; // apprentice
        }
        if (days >= 8)
        {
            rankIndex = 3; // adept
        }
        if (days >= 16)
        {
            rankIndex = 4; // acolyte
        }
        if (days >= 32)
        {
            rankIndex = 5; // archon
        }

        EnableRankTextObjectByIndex(rankIndex);
        Debug.Log("user has achieved rank " + rankIndex);
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
}
