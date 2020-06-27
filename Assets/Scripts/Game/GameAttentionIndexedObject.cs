using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAttentionIndexedObject : MonoBehaviour
{
    private int index = 0;
    
    public void SetIndex (int i)
    {
        index = i;
    }

    public int GetIndex ()
    {
        return index;
    }

    public void CheckIndex()
    {
        GameAttention.CheckIndex(index);
    }
}
