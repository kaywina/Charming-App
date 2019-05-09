using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnSwipe : MonoBehaviour
{
    private Vector3 lastPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButton(0) && lastPosition != Vector3.zero)
        {
            if (Input.mousePosition != lastPosition)
            {
                Vector3 mousePositionDelta = Input.mousePosition - lastPosition;
                gameObject.transform.Rotate(0f, mousePositionDelta.x, 0f);
            }
        }
        lastPosition = Input.mousePosition;
#endif
#if UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            // GET TOUCH 0
            Touch touch0 = Input.GetTouch(0);

            // APPLY ROTATION
            if (touch0.phase == TouchPhase.Moved)
            {
                gameObject.transform.Rotate(0f, touch0.deltaPosition.x, 0f);
            }
        }
#endif
    }
}
