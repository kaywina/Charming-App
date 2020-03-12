using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] chimeSounds;
    public SetPlayerPrefFromToggle meditateAudioToggle;

    private void OnEnable()
    {
        SetMuteFromPlayerPref();
    }

    public void PlayChimeSound()
    {
        int randomIndex = Random.Range(0, chimeSounds.Length);
        //Debug.Log("Play sound " + chimeSounds[randomIndex].gameObject.name);
        chimeSounds[randomIndex].Play();
    }

    public void SetMuteFromPlayerPref()
    {
        if (PlayerPrefs.GetString(meditateAudioToggle.GetPlayerPrefName()) == "false")
        {
            Debug.Log("Mute all sounds");
            SetMuteOnSounds(true);
        }
        else
        {
            Debug.Log("Unmute all sounds");
            SetMuteOnSounds(false);
        }
    } 

    private void SetMuteOnSounds(bool mute)
    {
        for (int i = 0; i < chimeSounds.Length; i++)
        {
            chimeSounds[i].mute = mute;
        }
    }
}
