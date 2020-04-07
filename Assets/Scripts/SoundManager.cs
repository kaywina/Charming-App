using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] chimeSounds;
    public AudioSource wheelPointerSound;
    public SetPlayerPrefFromToggle meditateAudioToggle;

    private static int chimeIndex = 0;
    private bool goingUpScale = true;

    private string audioPlayerPref = "EnableSounds"; // don't change this in production

    private void OnEnable()
    {
        SetMuteFromPlayerPref();
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

    public void SetMuteFromPlayerPref()
    {
        if (PlayerPrefs.GetString(audioPlayerPref) == "true")
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

    private void SetMuteOnSounds(bool mute)
    {
        for (int i = 0; i < chimeSounds.Length; i++)
        {
            chimeSounds[i].mute = mute;
        }

        // Add mute function for other sounds below
        wheelPointerSound.mute = mute;

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




}
