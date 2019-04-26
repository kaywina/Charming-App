using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CycleMaterialOnTap : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    // white should be first color for all palettes
    public Material[] materials;

    public int defaultIndex = 0;
    private int index = 0;
    private BoxCollider2D boxCollider2D;
    private Camera mainCamera;
    private string playerPrefName;

    void OnEnable()
    {
        playerPrefName = gameObject.name + "-Color";
        mainCamera = Camera.main;

        if (defaultIndex >= materials.Length)
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
        SetMaterial();
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
                NextMaterial();
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
                    NextMaterial();
                }
            }
        }
#endif
    }

    private void SetMaterial()
    {
        meshRenderer.material = materials[index];
        PlayerPrefs.SetString(playerPrefName, index.ToString());

    }

    public void NextMaterial()
    {
        if (meshRenderer == null)
        {
            Debug.LogWarning("Cannot cycle color; mesh renderer is null");
            return;
        }

        index++;
        if (index >= materials.Length) { index = 0; }

        SetMaterial();
    }


}
