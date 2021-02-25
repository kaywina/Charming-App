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
    private bool alreadyPlaying = false;
    private float noteRepeatRate = 0.5f;

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
        if (Input.GetMouseButton(0))
        {

            if (alreadyPlaying) { return; } // flag used to allow "sliding" from one note to another without lifting button

            Vector2 worldMousePos2D = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (boxCollider2D.bounds.Contains(worldMousePos2D))
            {
                PlaySound();
            }

        }
#endif

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount >= 1)
        {
            
            if (alreadyPlaying) { return; } // flag used to allow "sliding" from one note to another without lifting finger

            // GET TOUCH 0
            Touch touch0 = Input.GetTouch(0);
            Vector2 worldTouchPos2D = (Vector2)mainCamera.ScreenToWorldPoint(touch0.position);

            // APPLY ROTATION
            if (boxCollider2D.bounds.Contains(worldTouchPos2D))
            {
                PlaySound();
            }
        }
#endif
    }

    private void PlaySound()
    {
        soundManager.PlayBreathSoundByIndex(breathSoundIndex);
        alreadyPlaying = true;
        Invoke("SetNotAlreadyPlaying", noteRepeatRate);
    }

    private void SetNotAlreadyPlaying()
    {
        alreadyPlaying = false;
    }
}
