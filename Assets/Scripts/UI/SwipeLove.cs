using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeLove : SwipeFunction
{
    public LoveManager loveManager;

    protected override void SwipeLeft()
    {
        loveManager.NextLove();
    }

    protected override void SwipeRight()
    {
        loveManager.PreviousLove();
    }
}
