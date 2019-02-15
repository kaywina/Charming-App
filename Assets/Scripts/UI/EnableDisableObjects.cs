using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableObjects : MonoBehaviour
{
    public GameObject[] toEnable;
    public GameObject[] toDisable;

    public void EnableDisableGameObjects()
    {
        if (toEnable == null || toDisable == null)
        {
            Debug.LogError("Null array in EnableDisableObjects");
            return;
        }

        // enable objects
        for (int e = 0; e < toEnable.Length; e++)
        {
            if (toEnable[e] == null)
            {
                Debug.LogError("Cannot enable null object, in EnableDisableObjects");
                return;
            }

            toEnable[e].SetActive(true);
        }

        // disable objects
        for (int d = 0; d < toDisable.Length; d++)
        {
            if (toDisable[d] == null)
            {
                Debug.LogError("Cannot disable null object, in EnableDisableObjects");
                return;
            }

            toDisable[d].SetActive(false);
        }
    }
}
