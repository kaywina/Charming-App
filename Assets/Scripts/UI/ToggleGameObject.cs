using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    public GameObject objectToToggle;
    public bool enabledByDefault = false;
    
    void OnEnable()
    {
        if (enabledByDefault)
        {
            objectToToggle.SetActive(true);
        }
        else
        {
            objectToToggle.SetActive(false);
        }
    }

    public void Toggle()
    {
        if (objectToToggle.activeSelf)
        {
            objectToToggle.SetActive(false);
        }
        else
        {
            objectToToggle.SetActive(true);
        }
    }
}
