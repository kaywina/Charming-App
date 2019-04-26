using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCharmControls : MonoBehaviour
{

    public GameObject[] charmSets;
    public GameObject[] unlockSets;
    public GameObject[] arrowButtons;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private float maxVertical = 200f;
    private float minHorizontal = 30f;

    private int charmSet;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = Vector3.zero;
        endPosition = Vector3.zero;
        charmSet = 0;
    }

    private void OnEnable()
    {
        charmSet = Charms.GetCharmSet();
    }

    // Update is called once per frame
    void Update()
    {
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
        float xDifference = startPos.x - endPos.x;
        float yDifference = startPos.y - endPos.y;


        if (Mathf.Abs(yDifference) > maxVertical)
        {
            // Debug.Log("no swipe, too much vertical movement");
            // do nothing
        }
        else if (xDifference > minHorizontal)
        {
            // Debug.Log("swipe from right to left");
            // go to previous charm set if possible otherwise do nothing
            if (charmSet == 0)
            {
                DeactivateObjects(charmSet);
                charmSet = 1;
                ActivateObjects(charmSet);
            }
        }
        else if (xDifference < -minHorizontal)
        {
            // Debug.Log("swipe from left to right");
            // go to next charm set if possible otherwise do nothing
            if (charmSet == 1)
            {
                DeactivateObjects(charmSet);
                charmSet = 0;
                ActivateObjects(charmSet);
            }
        }
    }

    private void DeactivateObjects(int index)
    {
        charmSets[charmSet].SetActive(false);
        unlockSets[charmSet].SetActive(false);
        arrowButtons[charmSet].SetActive(false);
    }

    private void ActivateObjects(int index)
    {
        charmSets[charmSet].SetActive(true);
        unlockSets[charmSet].SetActive(true);
        arrowButtons[charmSet].SetActive(true);
    }
}