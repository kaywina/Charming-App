using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    public Slider slider;
    public SoundManager soundManager;

    private void OnEnable()
    {
        slider.value = soundManager.GetMusicVolumeMultiplier();
    }

    public void SetVolumeFromSlider()
    {
        Debug.Log("Set volume from slider");
        soundManager.SetMusicVolumeMultiplier(slider.value);
    }

    public void ResetVolumeMultiplier()
    {
        slider.value = 1;
        SetVolumeFromSlider();
    }
}
