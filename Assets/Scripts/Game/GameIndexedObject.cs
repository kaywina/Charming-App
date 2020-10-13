using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameIndexedObject : MonoBehaviour
{
    private int shuffledIndex = 0;
    private int orderedIndex = 0;
    private Button button;
    public enum GameType { Attention, Remember };
    public GameType gameType = GameType.Attention;

    private void OnEnable()
    {
        button = gameObject.GetComponent<Button>();
        button.interactable = true;
    }

    public void SetOrderedIndex (int i)
    {
        orderedIndex = i;
    }

    public int GetOrderedIndex()
    {
        return orderedIndex;
    }

    public void SetShuffledIndex (int i)
    {
        shuffledIndex = i;
    }

    public int GetShuffledIndex ()
    {
        return shuffledIndex;
    }

    public void CheckIndex()
    {
        bool isPlaying = false;
        if (gameType == GameType.Attention)
        {
            isPlaying = AttentionGame.CheckIndex(shuffledIndex, orderedIndex);
            if (isPlaying) { button.interactable = false; }
        }
        else if (gameType == GameType.Remember)
        {
            isPlaying = RememberGame.CheckIndex(shuffledIndex, orderedIndex);
            // can continue playing if wrong button is clicked in Remember game
        }
    }
}
