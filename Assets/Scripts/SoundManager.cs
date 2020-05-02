using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] breathSounds;
    public AudioSource wheelPointerSound;
    public AudioSource music;

    private static int chimeIndex = 0;
    private bool goingUpScale = true;

    private string soundsPlayerPref = "EnableSounds"; // don't change this in production
    private string musicPlayerPref = "EnableMusic"; // don't change this in production

    private void OnEnable()
    {
        SetMuteSoundFromPlayerPref();
        SetPlayMusicFromPlayerPref();
    }

    public void PlayRandomBreathSound()
    {
        int randomIndex = Random.Range(0, breathSounds.Length);
        //Debug.Log("Play sound " + chimeSounds[randomIndex].gameObject.name);
        breathSounds[randomIndex].Play();
    }

    public void PlayBreathNoteInScale()
    {
        breathSounds[chimeIndex].Play();

        if (goingUpScale) { chimeIndex++; }
        else { chimeIndex--; }

        if (chimeIndex >= breathSounds.Length)
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

    public void StopAllBreathNotes()
    {
        for (int i = 0; i < breathSounds.Length; i++)
        {
            breathSounds[i].Stop();
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
        for (int i = 0; i < breathSounds.Length; i++)
        {
            breathSounds[i].mute = mute;
        }

        // Add mute function for other sounds below
        wheelPointerSound.mute = mute;

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

    public void PlayBreathSoundByIndex (int i)
    {
        if (i < 0 || i >= breathSounds.Length)
        {
            Debug.LogError("Invalid index for breath sounds array");
            return;
        }

        breathSounds[i].Play();
    }

    public void PlayWheelPointerSound()
    {
        wheelPointerSound.Play();
    }
}
