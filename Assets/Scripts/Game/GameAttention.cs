using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAttention : MonoBehaviour
{
    private int level = 0;

    public Text countdownText;
    int countdown = 3;

    public PlayManager playManager;

    public GameObject[] levelButtons;
    private Image[] images;
    

    private void OnEnable()
    {
        ResetCountdown();
        SetupLevel();
    }

    private void SetupLevel()
    {
        Debug.Log("Setup the level for the attention game");


        // get an array of all button Images for this level
        images = levelButtons[level].GetComponentsInChildren<Image>();

        if (images.Length <= 0)
        {
            Debug.LogError("No button images found; are you missing the inspector hookup? Aborting level setup");
            return;
        }

        // fill up an array up with numbers from 0 to one less than it's length
        int[] indexesToShuffle = new int[images.Length];

        // build the array of indexes that will be used to shuffle buttons
        for (int i = 0; i < indexesToShuffle.Length; i++)
        {
            indexesToShuffle[i] = i + 1; // we want random indexes to start at 1 so we don't get the first sprite in the array, used when hiding the pattern
        }

        Shuffle(indexesToShuffle); // shuffle those numbers

        // check for error
        if (indexesToShuffle.Length != images.Length)
        {
            Debug.LogError("Mismatched array lengths in Game Attention; aborting level setup");
            return;
        }

        /*
        string output = "";
        foreach (int i in indexesToShuffle)
        {
            output += " " + i.ToString();
        }
        Debug.Log("contents of indexesToShuffle after shuffling =" + output);
        */

        // this should apply the randomized pattern to the buttons
        for (int f = 0; f < indexesToShuffle.Length; f++)
        {
            //Debug.Log("Set sprite for " + levelButtons[level][f].name + " to " + playManager.GetSpriteByIndex(indexesToShuffle[f]).name);
            images[f].sprite = playManager.GetSpriteByIndex(indexesToShuffle[f]);
        }

        levelButtons[level].SetActive(true);

        CountdownToPlay();
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
        for (int i = 0; i < images.Length; i++)
        {
            //Debug.Log("Set default sprite for object named - " + levelButtons[level][i].name);
            images[i].sprite = playManager.GetSpriteByIndex(0); // set all sprites to blank
        }
    }

    private void NextLevel()
    {

        levelButtons[level].SetActive(false);

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

    public void CountdownToPlay()
    {
        Debug.Log("Start 3 2 1 countdown to play");
        countdownText.text = countdown.ToString();
        InvokeRepeating("CountdownByOne", 1f, 1f);
    }

    private void CountdownByOne()
    {
        countdown--;
        if (countdown <= 0)
        {
            HideAll();         
            ResetCountdown();
        }
        else
        {
            countdownText.text = countdown.ToString();
        }
    }

    private void ResetCountdown()
    {
        CancelInvoke("CountdownByOne");
        Debug.Log("Reset countdown");
        countdown = 3;
        countdownText.text = "";
    }

}

