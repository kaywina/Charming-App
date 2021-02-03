using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitchSlider : MonoBehaviour
{

    public SoundManager soundManager;
    public Slider slider;
    public Text valueText;

    private void OnEnable()
    {
        float pitch = soundManager.GetPitch();
        slider.value = pitch;
        valueText.text = pitch.ToString("F2");
    }

    public void SetPitchFromSliderValue()
    {
        soundManager.SetPitch(slider.value);
        valueText.text = slider.value.ToString("F2");
    }

    public void Reset()
    {
        float defaultPitch = soundManager.GetDefaultPitch();
        soundManager.ResetPitch();
        slider.value = defaultPitch;
        valueText.text = defaultPitch.ToString("F2");
    }
}
