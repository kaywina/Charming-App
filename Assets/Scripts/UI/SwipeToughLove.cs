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
        indexText.text = loveManager.GetSecondTempIndex().ToString();
        base.OnEnable();
    }

    public override void SwipeLeft()
    {
        //Debug.Log("Swile tough love left");
        loveManager.NextToughLove();
        indexText.text = loveManager.GetSecondTempIndex().ToString();
    }

    public override void SwipeRight()
    {
        //Debug.Log("Swile tough love right");
        loveManager.PreviousToughLove();
        indexText.text = loveManager.GetSecondTempIndex().ToString();
    }

}