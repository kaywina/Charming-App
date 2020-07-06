using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorTools : MonoBehaviour
{

    private static GameObject[] taggedObjects;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void DeletePersistentData()
    {
        DataManager.DeleteDataFile();
    }

    public static void TakeScreenshotInEditor()
    {
        TakeScreenshot.TakeShot();
    }

    public static void EnableMainUI()
    {
        if (taggedObjects == null || taggedObjects.Length <= 0)
        {
            Debug.LogError("No game objects found to disable; has the Main UI been disabled?");
            return;
        }

        for (int i = 0; i < taggedObjects.Length; i++)
        {
            taggedObjects[i].SetActive(true);
        }
    }

    public static void DisableMainUI()
    {

        taggedObjects = GameObject.FindGameObjectsWithTag("MainUI");

        if (taggedObjects == null || taggedObjects.Length <= 0)
        {
            Debug.LogError("No game objects found to disable; is the Main UI not currently enabled?");
            return;
        }

        for (int i = 0; i < taggedObjects.Length; i++)
        {
            taggedObjects[i].SetActive(false);
        }
    }

}
