using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    public GameObject[] rankTextObjects;
    public Text daysToNextRankText;
    private string daysPlayerPref = "RankDays"; // don't change this in production!
    private string maxRankLocKey = "REACHED_MAX_RANK"; // don't change this in production!

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
        int daysToNextRank = 0;

        // match number of rank cases to length of rankTextObjects array

        int firstRankDays = 3; // 3 instead 2 makes count start at 2 days until next rank (accounts for index starting at zero)
        int secondRankDays = 4 + firstRankDays;
        int thirdRankDays = 8 + secondRankDays;
        int fourthRankDays = 16 + thirdRankDays;
        int fifthRankDays = 32 + fourthRankDays;


        if (days >= fifthRankDays)
        {
            rankIndex = 5; // archon
            // this is the maximum rank
        }
        else if (days >= fourthRankDays)
        {
            rankIndex = 4; // acolyte
            daysToNextRank = fifthRankDays - days;
        }
        else if (days >= thirdRankDays)
        {
            rankIndex = 3; // adept
            daysToNextRank = fourthRankDays - days;
        }
        else if (days >= secondRankDays)
        {
            rankIndex = 2; // apprentice
            daysToNextRank = thirdRankDays - days;
        }
        else if (days >= firstRankDays)
        {
            rankIndex = 1; // amateur
            daysToNextRank = secondRankDays - days;
        }
        else
        {
            rankIndex = 0; // unranked
            daysToNextRank = firstRankDays - days;
        }


        // special loc case for having reached maximum rank
        if (rankIndex == 5)
        {
            daysToNextRankText.text = Localization.GetTranslationByKey(maxRankLocKey);
        }
        else
        {
            daysToNextRankText.text = daysToNextRank.ToString();
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
