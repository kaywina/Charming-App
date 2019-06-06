using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleMaterialColorOnTap : MonoBehaviour
{
    public Material material;

    // white should be first color for all palettes
    private string[] hexStrings = { "#ffffff", "#ff00bf", "#9500ff", "#4800ff", "#00b7ff", "#00fffb" }; // cyan to magenta palette

    public int defaultIndex = 0;
    private int index = 0;
    private BoxCollider2D boxCollider2D;
    private Camera mainCamera;
    private string playerPrefName;

    void OnEnable()
    {
        playerPrefName = gameObject.name + "-Color";
        mainCamera = Camera.main;

        if (defaultIndex >= hexStrings.Length)
        {
            defaultIndex = 0;
        }

        if (string.IsNullOrEmpty(PlayerPrefs.GetString(playerPrefName)))
        {
            index = defaultIndex;
        }
        else
        {
            index = int.Parse(PlayerPrefs.GetString(playerPrefName));
        }

        boxCollider2D = GetComponent<BoxCollider2D>();
        SetMaterialColor();
    }

    private void OnDisable()
    {
        boxCollider2D = null;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldMousePos2D = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            if (boxCollider2D.bounds.Contains(worldMousePos2D))
            {
                NextMaterialColor();
            }
        }
#endif

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 worldTouchPos2D = (Vector2)mainCamera.ScreenToWorldPoint(touch.position);

                if (boxCollider2D.bounds.Contains(worldTouchPos2D))
                {
                    NextMaterialColor();
                }
            }
        }
#endif
    }

    void SetMaterialColor()
    {
        Color newColor = new Color();
        ColorUtility.TryParseHtmlString(hexStrings[index], out newColor);
        material.color = newColor;
        PlayerPrefs.SetString(playerPrefName, index.ToString());
    }

    void NextMaterialColor()
    {
        index++;
        if (index > hexStrings.Length - 1) { index = 0; }
        SetMaterialColor();
    }
}
