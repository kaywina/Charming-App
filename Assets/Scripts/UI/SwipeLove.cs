using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeLove : SwipeFunction
{
    public LoveManager loveManager;

    public override void SwipeLeft()
    {
        loveManager.NextLove();
    }

    public override void SwipeRight()
    {
        loveManager.PreviousLove();
    }

    public void SwipeToughLoveLeft()
    {
        loveManager.NextToughLove();
    }

    public void SwipeToughLoveRight()
    {
        loveManager.PreviousToughLove();
    }
}
