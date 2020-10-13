using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RememberGameManager : MonoBehaviour
{
    private string hasSelectedButtonsPlayerPref = "HasSelectedButtons";
    private string difficultyPlayerPref = "RememberGameDifficultyIndex";
    private string shuffledIndexPlayerPrefPrefix = "ShuffledIndex_";
    private string numberOfDailyRoundsPlayerPref = "NumberOfDailyRounds";
    public PlayGame rememberPlayGame;
    public RememberGame rememberGameControls;
    public GameObject rememberComeBack;
    public GameObject instructions;

    public int daysToWin = 10;

    private int score = 0;

    private void OnEnable()
    {
        PlayGame();
    }

    public void NextRound()
    {
        int numberOfDailyRounds = GetDailyRound();

        numberOfDailyRounds++;

        if (numberOfDailyRounds > daysToWin)
        {
            EndGame();
        }
        else
        {
            PlayerPrefs.SetInt(numberOfDailyRoundsPlayerPref, numberOfDailyRounds);
        }
        Debug.Log("Number of daily rounds played so far = " + numberOfDailyRounds);
    }

    // reset data and UI
    public void EndGame()
    {
        Debug.Log("You win!");
        PlayerPrefs.SetInt(numberOfDailyRoundsPlayerPref, 0);
        SetHasSelectedButtonsPlayerPref(false);
        rememberComeBack.SetActive(false);
        rememberPlayGame.EndGame(score);
    }

    public int GetDailyRound()
    {
        return PlayerPrefs.GetInt(numberOfDailyRoundsPlayerPref);
    }

    public bool HasSavedData()
    {
        if (PlayerPrefs.GetString(hasSelectedButtonsPlayerPref) == "True")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetHasSelectedButtonsPlayerPref(bool set)
    {
        if (set)
        {
            PlayerPrefs.SetString(hasSelectedButtonsPlayerPref, "True");
            TimeManager.SetPrefsForRememberGame();
        }
        else
        {
            PlayerPrefs.SetString(hasSelectedButtonsPlayerPref, "False");
        }
    }

    public void SetSavedDifficultyIndex(int difficultyIndex)
    {
        //Debug.Log("Save the difficulty index of " + difficultyIndex);
        PlayerPrefs.SetInt(difficultyPlayerPref, difficultyIndex);
    }

    public int GetSavedDifficultIndex()
    {
        int difficultyIndex = PlayerPrefs.GetInt(difficultyPlayerPref);
        //Debug.Log("Saved difficulty index is " + difficultyIndex);
        return difficultyIndex;
    }

    public void SetSavedShuffledIndexes(int[] shuffledIndexes)
    {
        string logOutput = "";
        for (int i = 0; i < shuffledIndexes.Length; i++)
        {
            PlayerPrefs.SetInt(shuffledIndexPlayerPrefPrefix + i, shuffledIndexes[i]);
            logOutput += shuffledIndexes[i] + " ";
        }
        //Debug.Log("Saved player prefs for shuffled indexes = " + logOutput);
    }

    public int[] GetSavedShuffledIndexes()
    {
        int numberOfButtonsAtIndexZero = 3;
        int[] shuffledIndexes = new int[PlayerPrefs.GetInt(difficultyPlayerPref) + numberOfButtonsAtIndexZero];


        string logOutput = "";

        for (int i = 0; i < shuffledIndexes.Length; i++)
        {
            shuffledIndexes[i] = PlayerPrefs.GetInt(shuffledIndexPlayerPrefPrefix + i);
            logOutput += shuffledIndexes[i] + " ";
        }

        //Debug.Log("Shuffled indexes loaded from data: " + logOutput);

        return shuffledIndexes;
    }

    public void PlayGame()
    {
        Debug.Log("Play Remember Game");
        rememberPlayGame.Reset();
        if (PlayerPrefs.GetString(hasSelectedButtonsPlayerPref) == "True")
        {
            if (TimeManager.IsNewDay(TimeManager.TimeType.RememberGame))
            {
                //Debug.Log("It's a new day, input your numbers!");
                instructions.SetActive(false);
                rememberGameControls.SetupButtonsFromData();
                rememberGameControls.DisableDifficultySlider();
                rememberGameControls.gameObject.SetActive(true);
            }
            else
            {
                //Debug.Log("It's not a new day, come back tomorrow");
                rememberGameControls.gameObject.SetActive(false);
                rememberComeBack.SetActive(true);
                instructions.SetActive(false);
            }

        }
        else // there is no stored data so start fresh
        {
            instructions.SetActive(true);
            rememberGameControls.SetupButtons(false);
            rememberComeBack.SetActive(false);
        }
    }
}
