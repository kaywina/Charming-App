using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] breathSounds;
    public AudioSource wheelPointerSound;
    public AudioSource[] musicClips;

    private static int musicIndex = 0;
    private static int chimeIndex = 0;
    private bool goingUpScale = true;

    private string soundsPlayerPref = "EnableSounds"; // don't change this in production
    private string musicPlayerPref = "EnableMusic"; // don't change this in production

    private string musicIndexPlayerPref = "MusicIndex";

    public Toggle musicToggle;

    private void OnEnable()
    {
        SetMuteSoundFromPlayerPref();
        SetMusicIndexFromPlayerPref();
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
            StopMusic();
        }
        else
        {
            PlayMusic();
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

    private void SetMusicIndexFromPlayerPref()
    {
        if (PlayerPrefs.HasKey(musicIndexPlayerPref))
        {
            musicIndex = PlayerPrefs.GetInt(musicIndexPlayerPref);
        }
    }

    private void PlayMusic()
    {
        CheckMusicIndex();
        musicClips[musicIndex].Play();
    }

    private void StopMusic()
    {
        CheckMusicIndex();
        musicClips[musicIndex].Stop();
    }

    private void CheckMusicIndex()
    {
        if (musicIndex < 0)
        {
            Debug.LogWarning("Music index is less than zero, resetting to zero default");
            musicIndex = 0;
        }

        else if (musicIndex >= musicClips.Length)
        {
            Debug.LogWarning("Music index is invalid, resetting to zero default");
            musicIndex = 0;
        }

        PlayerPrefs.SetInt(musicIndexPlayerPref, musicIndex);
    }

    public void SetMusicByIndex(int index)
    {
        StopMusic();

        // use a specific index to disable music from button
        if (index == -1)
        {
            DisableMusic();
            return;
        }

        musicIndex = index;
        CheckMusicIndex();
        PlayMusic();
        musicToggle.isOn = true;
        PlayerPrefs.SetString(musicPlayerPref, "true");
    }

    private void DisableMusic()
    {
        PlayerPrefs.SetString(musicPlayerPref, "false");
        PlayerPrefs.SetInt(musicIndexPlayerPref, 0);
        SetPlayMusicFromPlayerPref();
        musicToggle.isOn = false;
    }
}
