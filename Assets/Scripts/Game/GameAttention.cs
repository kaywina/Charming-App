using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAttention : MonoBehaviour
{
    private int level = 0;
    private int score = 0;
    public Text scoreText;

    public Text countdownText;
    private int countdown = 3;

    public PlayGame playGame;
    public PlayManager playManager;

    public GameObject[] levelButtons;
    public Image[] images;
    public static int[] indexes;
    public static GameAttentionIndexedObject[] indexedButtons;

    private static bool playingGame = false;

    private static int selectedCount = 0;

    private static GameAttention instance;

    private static int selectedIndex = 0;

    private void Awake()
    {
        instance = gameObject.GetComponent<GameAttention>();
    }

    private void OnEnable()
    {
        ResetGame();
        SetupLevel();
    }

    private void SetupLevel()
    {
        //Debug.Log("Setup the level for the attention game");


        // get an array of all button Images for this level
        images = levelButtons[level].GetComponentsInChildren<Image>();
        indexedButtons = levelButtons[level].GetComponentsInChildren<GameAttentionIndexedObject>();

        // set ordered indexes (we use these to track which button a user selects
        for (int n = 0; n < indexedButtons.Length; n++)
        {
            indexedButtons[n].SetOrderedIndex(n);
        }

        if (images.Length <= 0)
        {
            Debug.LogError("No button images found; are you missing the inspector hookup? Aborting level setup");
            return;
        }

        // fill up an array up with numbers from 0 to one less than it's length
        indexes = new int[images.Length];

        // build the array of indexes that will be used to shuffle buttons
        for (int i = 0; i < indexes.Length; i++)
        {
            indexes[i] = i;
        }

        Shuffle(indexes); // shuffle those numbers

        // check for error
        if (indexes.Length != images.Length)
        {
            Debug.LogError("Mismatched array lengths in Game Attention; aborting level setup");
            return;
        }

        
        string output = "";
        foreach (int i in indexes)
        {
            output += " " + i.ToString();
        }
        Debug.Log("contents of indexesToShuffle after shuffling =" + output);
        

        // this should apply the randomized pattern to the buttons
        for (int f = 0; f < indexes.Length; f++)
        {
            //Debug.Log("Set sprite for " + levelButtons[level][f].name + " to " + playManager.GetSpriteByIndex(indexesToShuffle[f]).name);
            images[f].sprite = playManager.GetSpriteByIndex(indexes[f]);
            indexedButtons[f].SetShuffledIndex(indexes[f]); // also set the index on the button, which we use to track which buttons the user has selected
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
            images[i].sprite = playManager.GetHideSprite(); // set all sprites to blank
        }
    }

    private void EndGame()
    {
        playingGame = false;
        levelButtons[level].SetActive(false);
        playGame.EndGame();
    }

    private void NextLevel()
    {
        playingGame = false;
        selectedCount = 0;
        levelButtons[level].SetActive(false);

        level++;

        if (level > levelButtons.Length)
        {
            Debug.Log("Completed all levels");
            instance.EndGame();
            return;
        }

        SetupLevel();
    }

    public void CountdownToPlay()
    {
        //Debug.Log("Start 3 2 1 countdown to play");
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
            playingGame = true;
        }
        else
        {
            countdownText.text = countdown.ToString();
        }
    }

    private void ResetCountdown()
    {
        CancelInvoke("CountdownByOne");
        //Debug.Log("Reset countdown");
        countdown = 3;
        countdownText.text = "";
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void SetImage(int index)
    {
        Debug.Log("index is " + index);
        Debug.Log("indexes[index] is " + indexes[index]);
        indexedButtons[selectedIndex].GetComponent<Image>().sprite = playManager.GetSpriteByIndex(index);
    }

    public static void CheckIndex(int shuffledIndex, int orderedIndex)
    {

        selectedIndex = orderedIndex;
        if (!playingGame)
        {
            return;
        }

        //Debug.Log("selectedCount = " + selectedCount.ToString());
        //Debug.Log("indexToCheck is " + indexToCheck.ToString());

        if (selectedCount != shuffledIndex)
        {
            Debug.Log("Incorrect");
            instance.EndGame();
        }
        else
        {
            Debug.Log("Correct");
            selectedCount++;
            instance.SetImage(shuffledIndex);
            instance.IncrementScore();  
        }

        if (selectedCount >= indexes.Length)
        {
            Debug.Log("Go to next level");
            instance.NextLevel();
        }
    }

    public void ResetGame()
    {
        playingGame = false;
        selectedCount = 0;
        level = 0;
        score = 0;
        scoreText.text = "";
        ResetCountdown();
    }

}

