using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeFunction : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 endPosition;

    private float maxVertical = 200f;
    private float minHorizontal = 30f;

    // do nothing unless swipe is between 7.5% and 62% of the way down the screen
    public float yLimitTop = 0.075f;
    public float yLimitBottom = 0.62f;

    protected void OnEnable()
    {
        startPosition = Vector3.zero;
        endPosition = Vector3.zero;
    }

    // Update is called once per frame
    private void Update()
    {
#if UNITY_STANDALONE
        SimulateSwipesInEditor();
#elif UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR
        DetectSwipesOnMobile();
#endif
    }

    private void DetectSwipesOnMobile()
    {
        //Debug.Log("Swipe on mobile");
        if (Input.touchCount == 1)
        {
            // GET TOUCH 0
            Touch touch0 = Input.GetTouch(0);

            // APPLY ROTATION
            if (touch0.phase == TouchPhase.Began)
            {
                startPosition = touch0.position;
            }

            else if (touch0.phase == TouchPhase.Ended)
            {
                endPosition = touch0.position;
                CheckForSwipe(startPosition, endPosition);
            }
        }
    }

    /* Deprecated as the Unity bug that caused the requirement for this appears to have been fixed
    private void SimulateSwipesInEditor()
    {
        Debug.Log("Swipe on editor");
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPosition = Input.mousePosition;
            CheckForSwipe(startPosition, endPosition);
        }
    }
    */

    private void CheckForSwipe(Vector3 startPos, Vector3 endPos)
    {
        // calculate if mouse movement was a swipe
        //Debug.Log("swipe from " + startPosition + " to " + endPosition);
        float startRelativePosY = 1 - (startPosition.y / Screen.height);

        // exit if not in swipable area (around charm and colum); i.e. can't swipe over charm buttons or header
        if (startRelativePosY < yLimitTop || startRelativePosY > yLimitBottom) { return; }

        // keep going if swipe is valid
        float xDifference = startPos.x - endPos.x;
        float yDifference = startPos.y - endPos.y;

        if (Mathf.Abs(yDifference) > maxVertical)
        {
            // Debug.Log("no swipe, too much vertical movement");
            // do nothing
        }
        else if (xDifference > minHorizontal)
        {
            SwipeLeft();
            //Debug.Log("swipe from right to left");
            
        }
        else if (xDifference < -minHorizontal)
        {
            SwipeRight();
            //Debug.Log("swipe from left to right");
        }
    }

    public virtual void SwipeLeft()
    {
    }

    public virtual void SwipeRight()
    {
    }
}
