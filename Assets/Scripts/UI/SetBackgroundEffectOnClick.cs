using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is used on the Yes button in the Unlock screen, to set the background particle effect automatically when one of those is unlocked
 * */

public class SetBackgroundEffectOnClick : MonoBehaviour
{
    public bool setEnabled = false;
    public int effectIndex = 0; // change this in inspector to set a different background effect
    public BackgroundParticles backgroundParticles;

    public void SetBackgroundEffect()
    {
        if (!setEnabled) { return; } // need to enable this in inspector for it to work

        backgroundParticles.EnableGameObjectByIndex(effectIndex);

        Reset();
    }

    // this is also called by the No button to reset if confirmation is cancelled
    public void Reset()
    {
        setEnabled = false;
        effectIndex = 0;
    }

    public void SetEnabled(bool toSet)
    {
        setEnabled = toSet;
    }

    public void SetEffectIndex (int toSet)
    {
        effectIndex = toSet;
    }
}
