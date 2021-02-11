using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeToughLove : SwipeFunction
{
    public LoveManager loveManager;
    public Text indexText;

    public override void OnEnable()
    {
        indexText.text = GetAdjustedSecondIndex().ToString();
        base.OnEnable();
    }

    public override void SwipeLeft()
    {
        //Debug.Log("Swile tough love left");
        loveManager.NextToughLove();
        indexText.text = GetAdjustedSecondIndex().ToString();
    }

    public override void SwipeRight()
    {
        //Debug.Log("Swile tough love right");
        loveManager.PreviousToughLove();
        indexText.text = GetAdjustedSecondIndex().ToString();
    }

    private int GetAdjustedSecondIndex()
    {
        int secondTempIndex = loveManager.GetSecondTempIndex();

        if (secondTempIndex < 0)
        {
            secondTempIndex = loveManager.GetMaxToughLoveIndex() + 1;
        }
        else
        {
            secondTempIndex++;
        }
        return secondTempIndex;
    }
}