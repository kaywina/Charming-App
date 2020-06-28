using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAttentionIndexedObject : MonoBehaviour
{
    private int shuffledIndex = 0;
    private int orderedIndex = 0;
    
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
        GameAttention.CheckIndex(shuffledIndex, orderedIndex);
    }
}
