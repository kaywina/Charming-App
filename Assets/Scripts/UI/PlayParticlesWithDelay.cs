using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticlesWithDelay: MonoBehaviour
{
    public ParticleSystem particles;
    public float delaySeconds = 1f;
    public bool invokeOnEnable = true;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (invokeOnEnable)
        {
            Invoke("PlayParticles", delaySeconds);
        }
    }

    // Update is called once per frame
    void OnDisable()
    {
        StopParticles();
    }

    private void PlayParticles()
    {
        if (particles != null) { particles.Play(); }
    }

    private void StopParticles()
    {
        CancelInvoke("PlayParticles");
        if (particles != null) { particles.Stop(); }
    }
}

