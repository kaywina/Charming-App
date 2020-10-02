using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRemember : MonoBehaviour
{
    private static GameRemember instance;

    public GameObject[] levelButtons;
    public PlayManager playManager;
    public RememberGameSlider difficultySlider;
    public GameObject rememberComeBack;

    private int difficultyIndex = 2;
    private Image[] images;
    private static int[] indexes;
    private static GameIndexedObject[] indexedButtons;
    private static bool playingGame = false;
    private static int selectedIndex = 0;
    private static int selectedCount = 0;

    private bool hasSelectedButtons = false;
    private string hasSelectedButtonsPlayerPref = "HasSelectedButtons";

    private void Awake()
    {
        instance = gameObject.GetComponent<GameRemember>();
    }

    private void OnEnable()
    {
        if (PlayerPrefs.GetString(hasSelectedButtonsPlayerPref) == "True")
        {
            difficultySlider.gameObject.SetActive(false);
            DisableAllButtons();
            rememberComeBack.SetActive(true);
        }
        else
        {
            ResetButtonSelect();
        }
    }

    private void DisableAllButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].SetActive(false);
        }
    }

    private void ResetButtonSelect()
    {
        Debug.Log("Reset buttons");
        difficultySlider.gameObject.SetActive(true);
        rememberComeBack.SetActive(false);
        SetupButtons();
    }

    private void EndButtonSelectStage()
    {
        Debug.Log("Finish Today's Round of Remember");
        difficultySlider.gameObject.SetActive(false);
        rememberComeBack.SetActive(true);
        PlayerPrefs.SetString(hasSelectedButtonsPlayerPref, "True");
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

    public void SetupButtons()
    {
        //Debug.Log("Setup the level for the attention game");

        // get an array of all button Images for this level

        difficultyIndex = difficultySlider.GetValue();

        images = levelButtons[difficultyIndex].GetComponentsInChildren<Image>();
        indexedButtons = levelButtons[difficultyIndex].GetComponentsInChildren<GameIndexedObject>();

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

        // activate only the correct button set for the difficulty
        for (int n = 0; n < levelButtons.Length; n++)
        {
            if (n == difficultyIndex)
            {
                levelButtons[n].SetActive(true);
            }
            else
            {
                levelButtons[n].SetActive(false);
            }
        }

        playingGame = true;
    }

    public void EnableButtonsByIndex(int index)
    {
        //Debug.Log("Enable buttons for index " + index);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i == index)
            {
                levelButtons[i].SetActive(true);
            }
            else
            {
                levelButtons[i].SetActive(false);
            }
        }
    }

    public void SetImage(int index)
    {
        //Debug.Log("index is " + index);
        //Debug.Log("indexes[index] is " + indexes[index]);
        indexedButtons[selectedIndex].GetComponent<Image>().sprite = playManager.GetSpriteByIndex(index);
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
            //Debug.Log("Incorrect"); // do nothing in this case
        }
        else
        {
            //Debug.Log("Correct");
            selectedCount++;
            indexedButtons[selectedIndex].gameObject.SetActive(false);
        }

        // has selected all the buttons
        if (selectedCount >= indexes.Length)
        {
            instance.EndButtonSelectStage();
        }

        return true;
    }
}
