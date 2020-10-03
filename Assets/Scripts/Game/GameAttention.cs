using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAttention : MonoBehaviour
{
    private int level = 0;
    private int score = 0;

    public Text countdownText;
    private int countdown = 3;
    public GameObject go;

    public PlayGame playGame;
    public PlayManager playManager;

    public GameObject[] levelButtons;
    private Image[] images;
    private static int[] indexes;
    private static GameIndexedObject[] indexedButtons;

    private static bool playingGame = false;

    private static int selectedCount = 0;

    private static GameAttention instance;

    private static int selectedIndex = 0;

    public GameObject incorrectIndicator;
    public GameObject instructions;

    private void Awake()
    {
        instance = gameObject.GetComponent<GameAttention>();
    }

    private void OnEnable()
    {
        instructions.SetActive(false);
        ResetGame();
        SetupLevel();
    }

    //for shuffle number from array
    void Shuffle(int[] array)
    {
        System.Random _random = new System.Random();
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(0, n);
            int t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }

    private void SetupLevel()
    {
        //Debug.Log("Setup the level for the attention game");


        // get an array of all button Images for this level
        images = levelButtons[level].GetComponentsInChildren<Image>();
        indexedButtons = levelButtons[level].GetComponentsInChildren<GameIndexedObject>();

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

        /*
        string output = "";
        foreach (int i in indexes)
        {
            output += " " + i.ToString();
        }
        Debug.Log("contents of indexesToShuffle after shuffling =" + output);
        */

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

    private void HideAll()
    {
        //Debug.Log("Hide all");
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
        indexedButtons[selectedIndex].gameObject.SetActive(true); // set the last clicked button back to active incase it was deactivated as an incorrect choice
        if (playGame.CheckScore(score))
        {
            playGame.SaveHighScore(score);
        }
        playGame.EndGame(score);
    }

    private void NextLevel()
    {
        playingGame = false;
        UnityAnalyticsController.SendAttentionGameLevelCompletedEvent(level);
        HideGo();
        selectedCount = 0;
        levelButtons[level].SetActive(false);

        level++;

        if (level >= levelButtons.Length)
        {
            //Debug.Log("Completed all levels");
            level--; //set this back so that corret level buttons get disabled in EndGame
            playGame.SetPerfectGameFlag(true);
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
            go.SetActive(true);
            playingGame = true;
            Invoke("HideGo", 1.5f);
        }
        else
        {
            countdownText.text = countdown.ToString();
        }
    }

    private void HideGo()
    {
        go.SetActive(false);
    }

    private void ResetCountdown()
    {
        CancelInvoke("CountdownByOne");
        //Debug.Log("Reset countdown");
        countdown = 3;
        countdownText.text = "";
        go.SetActive(false);

    }

    public void IncrementScore()
    {
        score++;
    }

    public void SetImage(int index)
    {
        //Debug.Log("index is " + index);
        //Debug.Log("indexes[index] is " + indexes[index]);
        indexedButtons[selectedIndex].GetComponent<Image>().sprite = playManager.GetSpriteByIndex(index);
    }

    public void ShowIncorrectIndicator()
    {
        indexedButtons[selectedIndex].gameObject.SetActive(false);
        incorrectIndicator.transform.position = indexedButtons[selectedIndex].transform.position;
        incorrectIndicator.SetActive(true);
    }

    public static bool CheckIndex(int shuffledIndex, int orderedIndex)
    {
        if (!playingGame)
        {
            return false;
        }

        selectedIndex = orderedIndex; // this needs to happen after the playingGame check, to avoid cases where user cicks a button between an incorrect guess and end of game, which results in buttons not showing up on subsequent plays
        
        //Debug.Log("selectedCount = " + selectedCount.ToString());
        //Debug.Log("indexToCheck is " + indexToCheck.ToString());

        if (selectedCount != shuffledIndex)
        {
            //Debug.Log("Incorrect");
            instance.ShowIncorrectIndicator();
            playingGame = false; // stop ability to press more game buttons
            instance.DelayedEndGame(1f);
        }
        else
        {
            //Debug.Log("Correct");
            selectedCount++;
            instance.SetImage(shuffledIndex);
            instance.IncrementScore();  
        }

        if (selectedCount >= indexes.Length)
        {
            //Debug.Log("Go to next level");
            instance.NextLevel();
        }

        return true;
    }

    public void DelayedEndGame(float secondsToDelay)
    {
        Invoke("EndGame", secondsToDelay);
    }

    public void ResetGame()
    {
        if (indexedButtons != null)
        {
            indexedButtons[selectedIndex].gameObject.SetActive(true); // set the last clicked button back to active incase it was deactivated as an incorrect choice
        }

        for (int n = 0; n < levelButtons.Length; n++)
        {
            levelButtons[n].SetActive(false);
        }
        incorrectIndicator.SetActive(false);
        playingGame = false;
        selectedCount = 0;
        level = 0;
        score = 0;
        ResetCountdown();
    }

}

