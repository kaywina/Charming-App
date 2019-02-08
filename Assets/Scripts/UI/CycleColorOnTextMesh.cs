using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class CycleColorOnTextMesh : MonoBehaviour
{
    private TextMesh textMesh;

    // white should be first color for all palettes
    private string[] hexStrings = { "#ffffff", "#ff00bf", "#9500ff", "#4800ff", "#00b7ff", "#00fffb" }; // cyan to magenta palette

    public int defaultIndex = 0;
    private int index = 0;
    private BoxCollider2D boxCollider2D;
    private Camera mainCamera;
    private string playerPrefName;

    // Update is called once per frame
    private void Start()
    {
        playerPrefName = gameObject.name + "-Color";
        mainCamera = Camera.main;
    }

    void OnEnable()
    {
        if (defaultIndex >= hexStrings.Length)
        {
            Debug.LogWarning("Default index in CycleColorOnTextMesh is invalid; using default 0");
            defaultIndex = 0;
        }

        if (string.IsNullOrEmpty(PlayerPrefs.GetString(playerPrefName)))
        {
            Debug.Log("no stored player pref, using default index");
            index = defaultIndex;
        }
        else
        {
            Debug.Log("there is a stored player pref for this color");
            index = int.Parse(PlayerPrefs.GetString(playerPrefName));
            Debug.Log(PlayerPrefs.GetString(playerPrefName));
        }
        
        textMesh = GetComponent<TextMesh>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        SetColor();
    }

    private void OnDisable()
    {
        textMesh = null;
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
                NextColor();
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
                    NextColor();
                }
            }
        }
#endif
    }

    private void SetColor()
    {
        Color newColor = new Color();
        ColorUtility.TryParseHtmlString(hexStrings[index], out newColor);
        textMesh.color = newColor;
        PlayerPrefs.SetString(playerPrefName, index.ToString());
        Debug.Log("set player pref to " + PlayerPrefs.GetString(playerPrefName));
        
    }

    public void NextColor()
    {
        if (textMesh == null) {
            Debug.LogWarning("Cannot cycle color; text mesh is null");
            return;
        }

        index++;
        if (index >= hexStrings.Length) { index = 0; }

        SetColor();       
    }


}
