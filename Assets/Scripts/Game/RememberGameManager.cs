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
    public GameObject sliderHintObject;
    public GameObject starryTextForPerfectGame;

    public int daysToWin = 10;

    private void OnEnable()
    {
        PlayGame();
    }

    public void NextRound()
    {
        int numberOfDailyRounds = GetDailyRound();

        numberOfDailyRounds++;

        PlayerPrefs.SetInt(numberOfDailyRoundsPlayerPref, numberOfDailyRounds);

        if (numberOfDailyRounds > daysToWin)
        {
            //Debug.Log("Completed " + daysToWin + " rounds for a perfect game!");
            rememberPlayGame.SetPerfectGameFlag(true);
            rememberGameControls.EndGame();
        }
    }

    // reset data and UI
    public void EndGame()
    {
        //Debug.Log("End game");
        SetHasSelectedButtonsPlayerPref(false);
        rememberComeBack.SetActive(false);
        int score = CalculateScore();
        //Debug.Log("score is " + score);

        rememberPlayGame.EndGame(score);
        PlayerPrefs.SetInt(numberOfDailyRoundsPlayerPref, 0); // do this after calculating the score
    }

    private int CalculateScore()
    {
        int score = 0;
        score = (GetDailyRound() - 1) * (GetSavedDifficultyIndex() + 1);
        return score;
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

    public int GetSavedDifficultyIndex()
    {

        int difficultyIndex = PlayerPrefs.GetInt(difficultyPlayerPref);
        //Debug.Log("Get the saved difficulty index of " + difficultyIndex);
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
        int[] shuffledIndexes = new int[GetSavedDifficultyIndex() + numberOfButtonsAtIndexZero];


        string logOutput = "";

        for (int i = 0; i < shuffledIndexes.Length; i++)
        {
            shuffledIndexes[i] = PlayerPrefs.GetInt(shuffledIndexPlayerPrefPrefix + i);
            logOutput += shuffledIndexes[i] + " ";
        }

        //Debug.Log("Shuffled indexes loaded from data: " + logOutput);

        return shuffledIndexes;
    }

    public void PlayGame(bool onInstructions = false)
    {
        //Debug.Log("Play Remember Game");
        rememberPlayGame.Reset();
        rememberGameControls.ResetButtons();
        starryTextForPerfectGame.SetActive(false);

        if (HasSavedData())
        {
            if (TimeManager.IsNewDay(TimeManager.TimeType.RememberGame))
            {
                //Debug.Log("It's a new day, input your numbers!");
                sliderHintObject.SetActive(false);
                starryTextForPerfectGame.SetActive(true);
                instructions.SetActive(false);
                rememberGameControls.SetupButtonsFromData();
                rememberGameControls.DisableInstructionsUI();
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
            rememberComeBack.SetActive(false);
            if (!onInstructions)
            {
                //Debug.Log("Coming from the menu select screen, or high score screen");
                instructions.SetActive(true);
            }
            else
            {
                //Debug.Log("Playing from instructions screen; setup first daily round");
                instructions.SetActive(false);
                rememberGameControls.SetupButtons(false);
                rememberGameControls.gameObject.SetActive(true);
            }       
        }
    }
}
