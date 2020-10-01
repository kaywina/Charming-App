using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRemember : MonoBehaviour
{

    public GameObject[] levelButtons;
    public PlayManager playManager;

    private int level = 5;
    private Image[] images;
    private static int[] indexes;
    private static GameAttentionIndexedObject[] indexedButtons;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableButtonsByIndex(int index)
    {
        Debug.Log("Enable buttons for index " + index);
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

        //CountdownToPlay(); // this is not used in Remember game; come back tomorrow instead
    }
}
