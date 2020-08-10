using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource wheelPointerSound;
    public AudioSource[] breathSounds;
    public AudioSource[] musicClips;

    private float wheelPointerSoundStartVolume;
    private float[] breathSoundsStartVolumes;
    private float[] musicClipsStartVolumes;

    private float defaultVolumeMultipleir = 0.1f;

    private static int musicIndex = 0;
    private static int chimeIndex = 0;
    private bool goingUpScale = true;

    private string soundsPlayerPref = "EnableSounds"; // don't change this in production
    private string musicPlayerPref = "EnableMusic"; // don't change this in production

    public bool enabledByDefault = true;

    private string musicIndexPlayerPref = "MusicIndex";

    // float values for these player prefs are a multiplier, between 0 and 2 
    private string musicVolumePlayerPref = "MusicVolumeMultiplier"; // don't change in production
    private string soundVolumePlayerPref = "SoundVolumeMultiplier"; // don't change in production

    public Toggle musicToggle;

    private void Awake()
    {
        if (enabledByDefault)
        {
            if (!PlayerPrefs.HasKey(soundsPlayerPref))
            {
                PlayerPrefs.SetString(soundsPlayerPref, "true");
            }
            if (!PlayerPrefs.HasKey(musicPlayerPref))
            {
                PlayerPrefs.SetString(musicPlayerPref, "true");
            }
        }


        // get starting relative volumes for all audio clips
        wheelPointerSoundStartVolume = wheelPointerSound.volume;
        breathSoundsStartVolumes = new float[breathSounds.Length];
        musicClipsStartVolumes = new float[musicClips.Length];

        // fill the sfx and music clip arrays
        for (int i = 0; i < breathSounds.Length; i++)
        {
            breathSoundsStartVolumes[i] = breathSounds[i].volume;
        }
        for (int i = 0; i < musicClips.Length; i++)
        {
            musicClipsStartVolumes[i] = musicClips[i].volume;
        }

        // 1 is default value for in-app volume control
        if (!PlayerPrefs.HasKey(musicVolumePlayerPref))
        {
            PlayerPrefs.SetFloat(musicVolumePlayerPref, defaultVolumeMultipleir);
            //Debug.Log("Set default music volume multiplier");
        }
        // otherwise set the volume multiplier
        else
        {
            SetMusicVolumeMultiplier(PlayerPrefs.GetFloat(musicVolumePlayerPref));
            //Debug.Log("Music volume multiplier set from player pref");
        }

        // do the same thing for sfx
        if (!PlayerPrefs.HasKey(soundVolumePlayerPref))
        {
            PlayerPrefs.SetFloat(soundVolumePlayerPref, defaultVolumeMultipleir);
            //Debug.Log("Set default sfx volume multiplier");
        }
        else
        {
            SetSoundVolumeMultiplier(PlayerPrefs.GetFloat(soundVolumePlayerPref));
            //Debug.Log("Sfx volume multiplier set from player pref");
        }
    }

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

    public void SetMusicVolumeMultiplier(float volumeMultiplier)
    {
        if (!CheckVolume(volumeMultiplier)) { return; }

        float tempNewVolume = 0;

        for (int i = 0; i < musicClips.Length; i++)
        {
            tempNewVolume = musicClipsStartVolumes[i] * volumeMultiplier;
            musicClips[i].volume = tempNewVolume;
        }
        PlayerPrefs.SetFloat(musicVolumePlayerPref, volumeMultiplier);

        //Debug.Log("Music volume multiplier has been set to " + volumeMultiplier.ToString());
    }

    public void SetSoundVolumeMultiplier(float volumeMultiplier)
    {
        if (!CheckVolume(volumeMultiplier)) { return; }

        wheelPointerSound.volume = wheelPointerSoundStartVolume * volumeMultiplier;

        float tempNewVolume = 0;
        for (int i = 0; i < breathSounds.Length; i++)
        {
            tempNewVolume = breathSoundsStartVolumes[i] * volumeMultiplier;
            breathSounds[i].volume = tempNewVolume;
        }

        PlayerPrefs.SetFloat(soundVolumePlayerPref, volumeMultiplier);

        //Debug.Log("Sound volume multiplier has been set to " + volumeMultiplier.ToString());
    }

    private bool CheckVolume (float v)
    {
        if (v < 0 || v > 1)
        {
            Debug.Log(v.ToString() + " is not a valid value for volume");
            return false;
        }
        return true;
    }

    public float GetMusicVolumeMultiplier()
    {
        return PlayerPrefs.GetFloat(musicVolumePlayerPref);
    }

    public float GetSoundVolumeMultiplier()
    {
        return PlayerPrefs.GetFloat(soundVolumePlayerPref);
    }

    public float GetDefaultVolumeMultiplier()
    {
        return defaultVolumeMultipleir;
    }
}
