using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RememberGameManager : MonoBehaviour
{
    private string hasSelectedButtonsPlayerPref = "HasSelectedButtons";
    private string difficultyPlayerPref = "RememberGameDifficultyIndex";
    public GameRemember gameRemember;
    public GameObject rememberComeBack;
    public GameObject instructions;

    private void OnEnable()
    {
        Debug.Log("On Enable for RememberGameManager");
        if (PlayerPrefs.GetString(hasSelectedButtonsPlayerPref) == "True")
        {
            if (TimeManager.IsNewDay(TimeManager.TimeType.RememberGame))
            {
                Debug.Log("It's a new day, input your numbers!");
                instructions.SetActive(false);
                Debug.Log("Saved difficulty index is " + GetSavedDifficultIndex());
            }
            else
            {
                Debug.Log("It's not a new day, come back tomorrow");
                gameRemember.DisableControls();
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

    public bool SetupButtonSelect()
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
        PlayerPrefs.SetInt(difficultyPlayerPref, difficultyIndex);
    }

    public int GetSavedDifficultIndex()
    {
        return PlayerPrefs.GetInt(difficultyPlayerPref);
    }


}
