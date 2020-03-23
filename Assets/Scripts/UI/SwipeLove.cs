using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeLove : MonoBehaviour
{
    public LoveManager loveManager;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private float maxVertical = 200f;
    private float minHorizontal = 30f;

    // do nothing unless swipe is between 7.5% and 62% of the way down the screen
    private float yLimitTop = 0.075f;
    private float yLimitBottom = 0.62f;

    private bool checkForSwipe = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        startPosition = Vector3.zero;
        endPosition = Vector3.zero;

        // only enable swipe functionality if user has gold subscription
        if (PlayerPrefs.GetString(UnityIAPController.goldSubscriptionPlayerPref) == "true")
        {
            checkForSwipe = true;
        }
        else
        {
            checkForSwipe = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!checkForSwipe) { return; }

#if UNITY_EDITOR || UNITY_STANDALONE
        SimulateSwipesInEditor();
#endif
#if UNITY_IOS || UNITY_ANDROID
        DetectSwipesOnMobile();
#endif
    }

    private void DetectSwipesOnMobile()
    {
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

    private void SimulateSwipesInEditor()
    {
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
            //Debug.Log("swipe from right to left");
            loveManager.NextLove();
            
        }
        else if (xDifference < -minHorizontal)
        {
            //Debug.Log("swipe from left to right");
            loveManager.PreviousLove();
        }
    }
}
