using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticlesOnEnable : MonoBehaviour
{
    public ParticleSystem particles;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (particles != null) { particles.Play(); }
    }

    // Update is called once per frame
    void OnDisable()
    {
        if (particles != null) { particles.Stop(); }
    }
}
