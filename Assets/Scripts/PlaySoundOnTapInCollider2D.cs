using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlaySoundOnTapInCollider2D : MonoBehaviour
{
    private Collider2D boxCollider2D;
    private Camera mainCamera;
    public SoundManager soundManager;
    public int breathSoundIndex = 0;

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
        if (Input.GetMouseButtonUp(0))
        {

            Vector2 worldMousePos2D = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (boxCollider2D.bounds.Contains(worldMousePos2D))
            {
                soundManager.PlayBreathSoundByIndex(breathSoundIndex);
            }

        }
#endif

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 1)
        {
            // GET TOUCH 0
            Touch touch0 = Input.GetTouch(0);
            Vector2 worldTouchPos2D = (Vector2)mainCamera.ScreenToWorldPoint(touch0.position);

            // APPLY ROTATION
            if (touch0.phase == TouchPhase.Ended && boxCollider2D.bounds.Contains(worldTouchPos2D))
            {
                soundManager.PlayBreathSoundByIndex(breathSoundIndex);
            }
        }
#endif
    }
}
