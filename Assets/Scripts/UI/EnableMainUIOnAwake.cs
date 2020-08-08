using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is intented as a fall-back for app intiialization, in case where main ui has been left deactivated in scene in a build (possibly as a reuslt of using Inspector Tools -> Disable Main UI, and not re-enabling it before a build)
 * */

public class EnableMainUIOnAwake : MonoBehaviour
{

    public GameObject coreObject;
    public GameObject overlayObject;

    private void Awake()
    {
        coreObject.SetActive(true);
        overlayObject.SetActive(true);
    }
}
