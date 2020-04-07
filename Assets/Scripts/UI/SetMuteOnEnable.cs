using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMuteOnEnable : MonoBehaviour
{
    public SoundManager soundManager;

    // Start is called before the first frame update
    private void OnEnable()
    {
        soundManager.SetMuteSoundFromPlayerPref();
    }
}
