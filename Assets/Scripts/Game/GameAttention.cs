using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAttention : MonoBehaviour
{
    private int level = 0;
    public PlayManager playManager;

    public GameObject[][] levelButtons;
    
    public GameObject[] buttonsLevel1;
    public GameObject[] buttonsLevel2;
    public GameObject[] buttonsLevel3;
    public GameObject[] buttonsLevel4;
    public GameObject[] buttonsLevel5;
    public GameObject[] buttonsLevel6;

    private void Awake()
    {
        levelButtons = new GameObject[][] { buttonsLevel1,
            buttonsLevel2, buttonsLevel2, buttonsLevel3,
            buttonsLevel4, buttonsLevel5, buttonsLevel6 };
    }

    private void OnEnable()
    {
        SetupLevel();
    }

    private void SetupLevel()
    {
        Debug.Log("Setup the level for the attention game");

        // fill array up with numbers from 0 to one less than it's length
        int[] indexesToShuffle = new int[levelButtons[level].Length];
        for (int i = 0; i < levelButtons[level].Length; i++)
        {
            indexesToShuffle[i] = i;
        }

        Shuffle(indexesToShuffle); // shuffle those numbers

        /*
        string output = "";
        foreach (int i in indexesToShuffle)
        {
            output += " " + i.ToString();
        }
        Debug.Log("contents of indexesToShuffle after shuffling =" + output);
        */



        HideAll();
    }

    //for shuffle number from array
    void Shuffle(int[] array)
    {
        System.Random _random = new System.Random();
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(1, n);
            int t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }

    private void HideAll()
    {
        Debug.Log("Hide all");
        for (int i = 0; i < levelButtons[level].Length; i++)
        {
            //Debug.Log("Set default sprite for object named - " + levelButtons[level][i].name);
            levelButtons[level][i].GetComponentInChildren<Image>().sprite = playManager.GetSpriteByIndex(0); // set all sprites to blank
        }
    }

    private void NextLevel()
    {
        level++;

        if (level > levelButtons.Length)
        {
            EndGame();
            return;
        }

        SetupLevel();
    }

    private void EndGame()
    {
        Debug.LogError("That's the end of the game; thanks for playing");
    }
}

