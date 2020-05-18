using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBackgroundParticleEffectOnEnable : MonoBehaviour
{

    public BackgroundParticles bgParticles;

    private int tempIndex;
    public int indexToActivate = 5; // index 5 is set up to refer to tornado particles in the scene

    // Start is called before the first frame update
    private void OnEnable()
    {
        tempIndex = bgParticles.GetIndex();
        bgParticles.SetIndex(indexToActivate, true);
    }

    private void OnDisable()
    {
        bgParticles.SetIndex(tempIndex, true);
    }
}
