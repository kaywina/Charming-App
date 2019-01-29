using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RotateOnSwipeInCollider : MonoBehaviour
{
    private Vector3 lastPosition = Vector3.zero;
    public Collider2D boxCollider2D;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButton(0) && lastPosition != Vector3.zero)
        {
            Vector3 newPosition = Input.mousePosition;
            if (newPosition == lastPosition) { return; }
      
            Vector2 worldMousePos2D = (Vector2)mainCamera.ScreenToWorldPoint(newPosition);

            if (boxCollider2D.bounds.Contains(worldMousePos2D))
            {
                Vector3 mousePositionDelta = Input.mousePosition - lastPosition;
                gameObject.transform.Rotate(0f, mousePositionDelta.x, 0f);
            }

            lastPosition = newPosition;
        }
        lastPosition = Input.mousePosition;
#endif

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 1)
        {
            // GET TOUCH 0
            Touch touch0 = Input.GetTouch(0);
            Vector2 worldTouchPos2D = (Vector2)mainCamera.ScreenToWorldPoint(touch0.position);
             
            // APPLY ROTATION
            if (touch0.phase == TouchPhase.Moved && boxCollider2D.bounds.Contains(worldTouchPos2D))
            {
                gameObject.transform.Rotate(0f, touch0.deltaPosition.x, 0f);
            }
        }
#endif
    }
}
