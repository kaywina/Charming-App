using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource chimeSound;
    
    public void PlayChimeSound()
    {
        chimeSound.Play();
    }

}
