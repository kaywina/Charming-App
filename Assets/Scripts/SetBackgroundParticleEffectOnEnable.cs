using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBackgroundParticleEffectOnEnable : MonoBehaviour
{

    public BackgroundParticles bgParticles;

    private int tempIndex;
    public int indexToActivate = 5; // index 5 is set up to refer to tornado particles in the scene
    public GameObject checkmark;

    // Start is called before the first frame update
    private void OnEnable()
    {
        tempIndex = bgParticles.GetIndex();
        bgParticles.SetIndex(indexToActivate, true);

        if (checkmark != null)
        {
            checkmark.SetActive(false);
        }
    }

    private void OnDisable()
    {
        bgParticles.SetIndex(tempIndex, true);
    }

    public void SetParticleEffectAsBackground()
    {
        tempIndex = indexToActivate;
        if (checkmark != null)
        {
            checkmark.SetActive(true);
            Invoke("DisableCheckmark", 1f); // only show the checkmark for 2 seconds
        }
    }

    private void DisableCheckmark()
    {
        checkmark.SetActive(false);
    }
}
