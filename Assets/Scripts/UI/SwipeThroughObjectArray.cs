using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeThroughObjectArray : SwipeFunction
{
    public GameObject[] objects;
    int index = 0;

    public Text currentPageText;
    public Text totalPagesText;

    public bool usingButtons = true;
    public bool allowCycling = true;

    new void Start()
    {
        base.Start();

        // always enable the first object in the array by default and disable the rest

        if (usingButtons)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (i == index)
                {
                    objects[i].SetActive(true);
                }
                else
                {
                    objects[i].SetActive(false);
                }
            }
        }

        SetPageIndicatorText();
    }

    public override void SwipeLeft()
    {
        //Debug.Log("Swipe left");
        objects[index].SetActive(false);
        index++;
        if (allowCycling)
        {
            if (index >= objects.Length) { index = 0; }
        }
        else
        {
            if (index >= objects.Length) { index--; }
        }

        objects[index].SetActive(true);
        SetPageIndicatorText();
    }

    public override void SwipeRight()
    {
        //Debug.Log("Swipe right");
        objects[index].SetActive(false);
        index--;

        if (allowCycling)
        {
            if (index < 0) { index = objects.Length - 1; }
        }
        else
        {
            if (index < 0) { index = 0; }
        }

        if (index < 0) { index = objects.Length - 1; }
        objects[index].SetActive(true);
        SetPageIndicatorText();
    }

    private void SetPageIndicatorText()
    {
        if (currentPageText != null)
        {
            currentPageText.text = (index + 1).ToString();
        }

        if (totalPagesText != null)
        {
            totalPagesText.text = objects.Length.ToString();
        }
    }
}
