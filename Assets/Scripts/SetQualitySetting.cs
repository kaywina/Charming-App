using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SetQualitySetting : MonoBehaviour
{
    private Slider slider;
    public Text sliderText;

    private void Start()
    {
        QualitySettings.SetQualityLevel(5); // max quality by default
    }

    private void OnEnable()
    {
        if (slider == null)
        {
            slider = gameObject.GetComponent<Slider>();
        }
        slider.value = QualitySettings.GetQualityLevel();
        sliderText.text = slider.value.ToString();
    }

    private void OnDisable()
    {

    }

    public void SetQualityLevelFromSlider()
    {
        QualitySettings.SetQualityLevel((int)slider.value, true);
        sliderText.text = slider.value.ToString();
    }
}
