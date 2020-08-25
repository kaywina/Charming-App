using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeToughLove : SwipeFunction
{
    public LoveManager loveManager;

    new void Start()
    {
        base.Start();
    }

    public override void SwipeLeft()
    {
        //Debug.Log("Swile tough love left");
        loveManager.NextToughLove();
    }

    public override void SwipeRight()
    {
        //Debug.Log("Swile tough love right");
        loveManager.PreviousToughLove();
    }

}