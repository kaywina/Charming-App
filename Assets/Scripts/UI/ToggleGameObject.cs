using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    public GameObject objectToToggle;
    public bool enabledByDefault = false;

    public GameObject disableOnToggle;
    
    void OnEnable()
    {
        if (enabledByDefault)
        {
            objectToToggle.SetActive(true);
            if (disableOnToggle != null) { disableOnToggle.SetActive(false); }
        }
        else
        {
            objectToToggle.SetActive(false);
            if (disableOnToggle != null) { disableOnToggle.SetActive(true); }
        }
    }

    public void Toggle()
    {
        if (objectToToggle.activeSelf)
        {
            objectToToggle.SetActive(false);
            if (disableOnToggle != null) { disableOnToggle.SetActive(true); }
        }
        else
        {
            objectToToggle.SetActive(true);
            if (disableOnToggle != null) { disableOnToggle.SetActive(false); }
        }
    }
}
