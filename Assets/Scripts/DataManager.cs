using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public UnlockButton[] unlockObjects;

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

            // special case for int player prefs
            if (ppName == RankManager.daysPlayerPref || ppName == CurrencyManager.currencyPlayerPref)
            {
                PlayerPrefs.SetInt(ppName, int.Parse(ppValue));
            }

            // otherwise everything else is a string
            else
            {
                PlayerPrefs.SetString(ppName, ppValue);
            } 
        }
    }

    private void OnApplicationQuit()
    {
        WritePersistentSaveData();
    }

    private void WritePersistentSaveData()
    {
        string saveData = "";

        for (int i = 0; i < unlockObjects.Length; i++)
        {
            if (i != 0)
            {
                // add a delimiter for each entry after first
                saveData += "*";
            }
            saveData = saveData + unlockObjects[i].objectToEnable.name + " " + PlayerPrefs.GetString(unlockObjects[i].objectToEnable.name);
        }

        saveData += pairSeparator + RankManager.daysPlayerPref + nameValueSeparator + PlayerPrefs.GetInt(RankManager.daysPlayerPref).ToString();
        saveData += pairSeparator + CurrencyManager.currencyPlayerPref + nameValueSeparator + PlayerPrefs.GetInt(CurrencyManager.currencyPlayerPref).ToString();

        File.WriteAllText(Application.persistentDataPath + "/" + saveFileName, saveData);
    }

    // Get save data; return null if not saved data is found
    private string ReadPersistentSaveData()
    {
        string allText;

        try
        {
            Debug.Log("Attempting to read text from save file");
            allText = File.ReadAllText(Application.persistentDataPath + "/" + saveFileName);
        }
        catch
        {
            Debug.Log("Save file has not yet been created");
            allText = null;
        }

        //Debug.Log("Contents of save file are: " + allText); // don't leave this uncommented in production!
        return allText;
    }
}
