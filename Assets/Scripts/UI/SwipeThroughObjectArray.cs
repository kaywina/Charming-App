using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeThroughObjectArray : SwipeFunction
{
    public GameObject[] objects;
    static int index = 0;

    new void Start()
    {
        base.Start();

        // always enable the first object in the array by default and disable the rest
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

    public override void SwipeLeft()
    {
        objects[index].SetActive(false);
        index--;
        if (index < 0) { index = objects.Length - 1; }
        objects[index].SetActive(true);
    }

    public override void SwipeRight()
    {
        objects[index].SetActive(false);
        index++;
        if (index >= objects.Length) { index = 0; }
        objects[index].SetActive(true);
    }
}
