using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    public GameObject[] rankTextObjects;
    public Text daysToNextRankText;
    public static string daysPlayerPref = "RankDayCount"; // don't change this in production!
    public static string maxRankLocKey = "REACHED_MAX_RANK"; // don't change this in production!

    private int newDays = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(daysPlayerPref))
        {
            newDays = PlayerPrefs.GetInt(daysPlayerPref);
        }
        else
        {
            PlayerPrefs.SetInt(daysPlayerPref, 0);
            IncrementDayCount();
        }

        if (TimeManager.IsNewDay(TimeManager.TimeType.Rank))
        {
            IncrementDayCount();
        }

        SetRank(newDays);
    }

    private void IncrementDayCount()
    {
        newDays++;
        PlayerPrefs.SetInt(daysPlayerPref, newDays);
        //Debug.Log("Total days checked in = " + newDays);
        TimeManager.SetPrefsForRank();
    }

    private void SetRank(int days)
    {
        DisableAllRankTextObjects();
        //Debug.Log("Set a rank based on number of unique daily spin days");

        int rankIndex = 0;
        int daysToNextRank = 0;

        // match number of rank cases to length of rankTextObjects array

        int firstRankDays = 3; // hack to make zero indexing work to display 2 days until first rank
        int secondRankDays = 4 + firstRankDays;
        int thirdRankDays = 8 + secondRankDays;
        int fourthRankDays = 16 + thirdRankDays;
        int fifthRankDays = 32 + fourthRankDays;


        if (days >= fifthRankDays)
        {
            rankIndex = 5; // archon
            UnityAnalyticsController.SendAchievedFifthRankEvent();
            // this is the maximum rank
        }
        else if (days >= fourthRankDays)
        {
            rankIndex = 4; // acolyte
            daysToNextRank = fifthRankDays - days;
            UnityAnalyticsController.SendAchievedFourthRankEvent();
        }
        else if (days >= thirdRankDays)
        {
            rankIndex = 3; // adept
            daysToNextRank = fourthRankDays - days;
            UnityAnalyticsController.SendAchievedThirdRankEvent();
        }
        else if (days >= secondRankDays)
        {
            rankIndex = 2; // apprentice
            daysToNextRank = thirdRankDays - days;
            UnityAnalyticsController.SendAchievedSecondRankEvent();
        }
        else if (days >= firstRankDays)
        {
            rankIndex = 1; // amateur
            daysToNextRank = secondRankDays - days;
            UnityAnalyticsController.SendAchievedFirstRankEvent();
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
        //Debug.Log("user has achieved rank " + rankIndex);
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
