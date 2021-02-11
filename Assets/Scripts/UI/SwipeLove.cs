using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeLove : SwipeFunction
{
    public LoveManager loveManager;
    public Text indexText;

    public override void OnEnable()
    {
        // get the index and adjust because it has been incremented for next day

        indexText.text = GetAdjustedIndex().ToString();
        base.OnEnable();
    }

    public override void SwipeLeft()
    {
        //Debug.Log("Swile easy love left");
        loveManager.NextLove();
        indexText.text = GetAdjustedIndex().ToString();
    }

    public override void SwipeRight()
    {
        //Debug.Log("Swile easy love right");
        loveManager.PreviousLove();
        indexText.text = GetAdjustedIndex().ToString();
    }

    private int GetAdjustedIndex()
    {
        int tempIndex = loveManager.GetTempIndex();

        if (tempIndex < 0)
        {
            tempIndex = loveManager.GetMaxLoveIndex() + 1;
        }
        else
        {
            tempIndex++;
        }
        return tempIndex;
    }
}
