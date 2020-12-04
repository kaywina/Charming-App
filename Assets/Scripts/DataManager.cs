using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public UnlockButton[] unlockObjects; // objects that store data for main ui unlockables i.e. charms
    public PlayGame[] playGames; // scripts that store data for the memory excercise games

    private static string saveFileName = "charmProgessData.txt"; // do not change this in production!
    private string persistentData;

    private char nameValueSeparator = ' '; // not recommended to change this in production
    private char pairSeparator = '*'; // not recommended to change this in production

    public static string GetSaveFileName()
    {
        return saveFileName;
    }

    public static void DeleteDataFile()
    {
        File.Delete(Application.persistentDataPath + "/" + saveFileName);
    }

    void Start()
    {
        LoadPersistentData();
    }

    public void LoadPersistentData()
    {
        Debug.Log("Attempt to load persistent data");
        persistentData = ReadPersistentSaveData();
        //Debug.Log("persistentData = " + persistentData);

        if (string.IsNullOrEmpty(persistentData))
        {
            Debug.Log("No persistent data was found; initialize normally");
            return;
        }
        else
        {
            Debug.Log("Persistent data was found; set player prefs from stored values");
            ParseSaveData();
        }
    }

    private void ParseSaveData()
    {
        string[] perObjectData = persistentData.Split(pairSeparator);

        for (int n = 0; n < perObjectData.Length; n++)
        {
            string[] playerPrefData = perObjectData[n].Split(nameValueSeparator);
            string ppName = playerPrefData[0];
            string ppValue = playerPrefData[1];

            // handle case for PlayGame scripts that are int data
            bool isDataForPlayGame = false;
            for (int i = 0; i < playGames.Length; i++)
            {
                if (ppName.Contains(playGames[i].gameName))
                {
                    isDataForPlayGame = true;
                }
            }

            // special case for non-string player prefs
            if (isDataForPlayGame
                || ppName == RankManager.daysPlayerPref 
                || ppName == CurrencyManager.currencyPlayerPref
                || ppName == LoveManager.PLAYER_PREF_NAME
                || ppName == LoveManager.PLAYER_PREF_NAME_MAX_UNLOCKED
                || ppName == LoveManager.SECOND_PLAYER_PREF_NAME
                || ppName == LoveManager.SECOND_PLAYER_PREF_NAME_MAX_UNLOCKED)
            {
                PlayerPrefs.SetInt(ppName, int.Parse(ppValue));
            }

            // otherwise everything else is a string
            else
            {
                PlayerPrefs.SetString(ppName, ppValue);
            }
            //Debug.Log("Parsed data for " + ppName + " is " + ppValue);
        }
    }

    private void OnApplicationQuit()
    {
        WritePersistentSaveData();
    }

    private void WritePersistentSaveData()
    {
        string saveData = ""; // start with an empty string

        // add save data for each of the unlock buttons (data for which charms are unlocked on main ui)
        for (int i = 0; i < unlockObjects.Length; i++)
        {
            // add a delimiter for each entry after very first
            if (i != 0)
            {
                saveData += pairSeparator;
            }
            saveData = saveData + unlockObjects[i].objectToEnable.name + nameValueSeparator + PlayerPrefs.GetString(unlockObjects[i].objectToEnable.name);
        }

        // data for memory excercise game high scores
        for (int i = 0; i < playGames.Length; i++)
        {
            saveData += pairSeparator + playGames[i].GetHighScorePlayerPrefName() + nameValueSeparator + PlayerPrefs.GetInt(playGames[i].GetHighScorePlayerPrefName());
            saveData += pairSeparator + playGames[i].GetNextRewardPlayerPrefName() + nameValueSeparator + PlayerPrefs.GetInt(playGames[i].GetNextRewardPlayerPrefName());
            saveData += pairSeparator + playGames[i].GetPreviousHighScorePlayerPrefName() + nameValueSeparator + PlayerPrefs.GetInt(playGames[i].GetPreviousHighScorePlayerPrefName());
        }

        // data for gold subscription
        saveData += pairSeparator + UnityIAPController.goldSubscriptionPlayerPref + nameValueSeparator + PlayerPrefs.GetString(UnityIAPController.goldSubscriptionPlayerPref);

        // data for Easy and Tough Love sayings
        saveData += pairSeparator + LoveManager.PLAYER_PREF_NAME + nameValueSeparator + PlayerPrefs.GetInt(LoveManager.PLAYER_PREF_NAME).ToString();
        saveData += pairSeparator + LoveManager.PLAYER_PREF_NAME_MAX_UNLOCKED + nameValueSeparator + PlayerPrefs.GetInt(LoveManager.PLAYER_PREF_NAME_MAX_UNLOCKED).ToString();
        saveData += pairSeparator + LoveManager.SECOND_PLAYER_PREF_NAME + nameValueSeparator + PlayerPrefs.GetInt(LoveManager.SECOND_PLAYER_PREF_NAME).ToString();
        saveData += pairSeparator + LoveManager.SECOND_PLAYER_PREF_NAME_MAX_UNLOCKED + nameValueSeparator + PlayerPrefs.GetInt(LoveManager.SECOND_PLAYER_PREF_NAME_MAX_UNLOCKED).ToString();

        // additional saved data
        saveData += pairSeparator + RankManager.daysPlayerPref + nameValueSeparator + PlayerPrefs.GetInt(RankManager.daysPlayerPref).ToString(); // data for achieved rank
        saveData += pairSeparator + CurrencyManager.currencyPlayerPref + nameValueSeparator + PlayerPrefs.GetInt(CurrencyManager.currencyPlayerPref).ToString(); // data for number of keys

        File.WriteAllText(Application.persistentDataPath + "/" + saveFileName, saveData);
    }

    // Get save data; return null if not saved data is found
    private string ReadPersistentSaveData()
    {
        string allText;

        try
        {
            //Debug.Log("Attempting to read text from save file");
            allText = File.ReadAllText(Application.persistentDataPath + "/" + saveFileName);
        }
        catch
        {
            //Debug.Log("Save file has not yet been created");
            allText = null;
        }

        //Debug.Log("Contents of save file are: " + allText); // don't leave this uncommented in production!
        return allText;
    }
}
