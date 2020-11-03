using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeLove : SwipeFunction
{
    public LoveManager loveManager;

    public override void SwipeLeft()
    {
        //Debug.Log("Swile easy love left");
        loveManager.NextLove();
    }

    public override void SwipeRight()
    {
        //Debug.Log("Swile easy love right");
        loveManager.PreviousLove();
    }
}
