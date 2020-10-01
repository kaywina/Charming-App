using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAttentionIndexedObject : MonoBehaviour
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
            isPlaying = GameAttention.CheckIndex(shuffledIndex, orderedIndex);
        }
        else if (gameType == GameType.Remember)
        {
            isPlaying = GameRemember.CheckIndex(shuffledIndex, orderedIndex);
        }

        //Debug.Log("isPlaying = " + isPlaying);
        if (isPlaying) { button.interactable = false; }
    }
}
