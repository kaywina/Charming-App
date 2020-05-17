using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public UnlockButton[] unlockObjects;

    private static string saveFileName = "charmProgessData.txt"; // do not change this in production!
    private string persistentData;

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
        string[] perObjectData = persistentData.Split('*');

        for (int n = 0; n < perObjectData.Length; n++)
        {
            string[] playerPrefData = perObjectData[n].Split(' ');
            string ppName = playerPrefData[0];
            string ppValue = playerPrefData[1];

            // special case for int player prefs
            if (ppName == RankManager.daysPlayerPref)
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

        saveData += "*" + RankManager.daysPlayerPref + " " + PlayerPrefs.GetInt(RankManager.daysPlayerPref).ToString();

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

        Debug.Log("Contents of save file are: " + allText);
        return allText;
    }
}
