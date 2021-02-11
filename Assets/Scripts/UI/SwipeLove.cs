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
        indexText.text = loveManager.GetTempIndex().ToString();
        base.OnEnable();
    }

    public override void SwipeLeft()
    {
        //Debug.Log("Swile easy love left");
        loveManager.NextLove();
        indexText.text = loveManager.GetTempIndex().ToString();
    }

    public override void SwipeRight()
    {
        //Debug.Log("Swile easy love right");
        loveManager.PreviousLove();
        indexText.text = loveManager.GetTempIndex().ToString();
    }
}
