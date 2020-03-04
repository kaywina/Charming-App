using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] chimeSounds;
    
    public void PlayChimeSound()
    {
        int randomIndex = Random.Range(0, chimeSounds.Length);
        //Debug.Log("Play sound " + chimeSounds[randomIndex].gameObject.name);
        chimeSounds[randomIndex].Play();
    }

}
