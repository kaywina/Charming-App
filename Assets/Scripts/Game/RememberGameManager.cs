using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RememberGameManager : MonoBehaviour
{
    private string hasSelectedButtonsPlayerPref = "HasSelectedButtons";
    private string difficultyPlayerPref = "RememberGameDifficultyIndex";
    private string shuffledIndexPlayerPrefPrefix = "ShuffledIndex_";
    public RememberGame rememberGame;
    public GameObject rememberComeBack;
    public GameObject instructions;

    private void OnEnable()
    {
        //Debug.Log("On Enable for RememberGameManager");
        if (PlayerPrefs.GetString(hasSelectedButtonsPlayerPref) == "True")
        {
            if (TimeManager.IsNewDay(TimeManager.TimeType.RememberGame))
            {
                Debug.Log("It's a new day, input your numbers!");
                instructions.SetActive(false);
                Debug.Log("Saved difficulty index is " + GetSavedDifficultIndex());
                //GetSavedShuffledIndexes();

            }
            else
            {
                Debug.Log("It's not a new day, come back tomorrow");
                rememberGame.gameObject.SetActive(false);
                rememberComeBack.SetActive(true);
                instructions.SetActive(false);
            }
            
        }
        else
        {
            instructions.SetActive(true);
            rememberComeBack.SetActive(false);
        }
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
        Debug.Log("Save the difficulty index of " + difficultyIndex);
        PlayerPrefs.SetInt(difficultyPlayerPref, difficultyIndex);
    }

    public int GetSavedDifficultIndex()
    {
        int difficultyIndex = PlayerPrefs.GetInt(difficultyPlayerPref);
        Debug.Log("The saved difficulty index is " + difficultyIndex);
        return difficultyIndex;
    }

    public void SetSavedShuffledIndexes(int[] shuffledIndexes)
    {
        for (int i = 0; i < shuffledIndexes.Length; i++)
        {
            PlayerPrefs.SetInt(shuffledIndexPlayerPrefPrefix + i, shuffledIndexes[i]);
            Debug.Log("Saved player pref for shuffled index = " + shuffledIndexes[i]);
        }
    }

    public int[] GetSavedShuffledIndexes()
    {
        int numberOfButtonsAtIndexZero = 3;
        int[] shuffledIndexes = new int[PlayerPrefs.GetInt(difficultyPlayerPref) + numberOfButtonsAtIndexZero];

        for (int i = 0; i < shuffledIndexes.Length; i++)
        {
            shuffledIndexes[i] = PlayerPrefs.GetInt(shuffledIndexPlayerPrefPrefix + i);
            Debug.Log("Load shuffled index = " + shuffledIndexes[i]);
        }

        return shuffledIndexes;
    }


}
