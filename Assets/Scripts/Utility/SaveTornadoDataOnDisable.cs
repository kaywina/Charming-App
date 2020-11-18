using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTornadoDataOnDisable : MonoBehaviour
{
    public BackgroundParticles bgParticles;

    void OnDisable()
    {
        bgParticles.SaveTornadoTransformValues();
    }
}
