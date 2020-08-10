using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    public enum VolumeSliderType { Music, SFX };
    public VolumeSliderType sliderType;

    public Slider slider;
    public SoundManager soundManager;

    private void OnEnable()
    {
        switch (sliderType) {
            case VolumeSliderType.Music:
                slider.value = soundManager.GetMusicVolumeMultiplier();
                break;
            case VolumeSliderType.SFX:
                slider.value = soundManager.GetSoundVolumeMultiplier();
                break;
            default:
                Debug.Log("Not a valid volume slider type");
                break;
        }
    }

    public void SetVolumeFromSlider()
    {
        
        switch (sliderType)
        {
            case VolumeSliderType.Music:
                //Debug.Log("Set music volume from slider");
                soundManager.SetMusicVolumeMultiplier(slider.value);
                break;
            case VolumeSliderType.SFX:
                //Debug.Log("Set sfx volume from slider");
                soundManager.SetSoundVolumeMultiplier(slider.value);
                break;
            default:
                Debug.Log("Not a valid volume slider type");
                break;
        }    
    }

    public void ResetVolumeMultiplier()
    {
        slider.value = soundManager.GetDefaultVolumeMultiplier();
        SetVolumeFromSlider();
    }
}
