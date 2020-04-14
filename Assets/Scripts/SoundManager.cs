using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] chimeSounds;
    public AudioSource wheelPointerSound;
    public AudioSource music;
    public AudioSource woosh;

    private static int chimeIndex = 0;
    private bool goingUpScale = true;

    private string soundsPlayerPref = "EnableSounds"; // don't change this in production
    private string musicPlayerPref = "EnableMusic"; // don't change this in production

    private void OnEnable()
    {
        SetMuteSoundFromPlayerPref();
        SetPlayMusicFromPlayerPref();
    }

    public void PlayRandomChimeSound()
    {
        int randomIndex = Random.Range(0, chimeSounds.Length);
        //Debug.Log("Play sound " + chimeSounds[randomIndex].gameObject.name);
        chimeSounds[randomIndex].Play();
    }

    public void PlayChimeNoteInScale()
    {
        chimeSounds[chimeIndex].Play();

        if (goingUpScale) { chimeIndex++; }
        else { chimeIndex--; }

        if (chimeIndex >= chimeSounds.Length)
        {
            goingUpScale = false;
            chimeIndex = chimeIndex - 2;
        }

        else if (chimeIndex < 0)
        {
            goingUpScale = true;
            chimeIndex = chimeIndex + 2;
        }
    }

    public void StopAllChimeNotes()
    {
        for (int i = 0; i < chimeSounds.Length; i++)
        {
            chimeSounds[i].Stop();
        }
    }

    public void SetMuteSoundFromPlayerPref()
    {
        if (PlayerPrefs.GetString(soundsPlayerPref) == "true")
        {
            //Debug.Log("Unmute all sounds");
            SetMuteOnSounds(false);
        }
        else
        {
            //Debug.Log("Mute all sounds");
            SetMuteOnSounds(true);
        }
    }

    public void SetPlayMusicFromPlayerPref()
    {
        if (PlayerPrefs.GetString(musicPlayerPref) == "true")
        {
            //Debug.Log("Play music");
            SetPlayOnMusic(false);
        }
        else
        {
            //Debug.Log("Stop music");
            SetPlayOnMusic(true);
        }
    }

    private void SetMuteOnSounds(bool mute)
    {
        for (int i = 0; i < chimeSounds.Length; i++)
        {
            chimeSounds[i].mute = mute;
        }

        // Add mute function for other sounds below
        wheelPointerSound.mute = mute;
        woosh.mute = mute;

    }

    private void SetPlayOnMusic(bool doNotPlay)
    {
        if (doNotPlay == true)
        {
            music.Stop();
        }
        else
        {
            music.Play();
        }
    }

    public void PlayChimeSoundByIndex (int i)
    {
        if (i < 0 || i >= chimeSounds.Length)
        {
            Debug.LogError("Invalid index for chime sounds array");
            return;
        }

        chimeSounds[i].Play();
    }

    public void PlayWheelPointerSound()
    {
        wheelPointerSound.Play();
    }

    public void PlayWooshSound()
    {
        woosh.Play();
    }
}
