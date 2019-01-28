using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SetQualitySetting : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        QualitySettings.SetQualityLevel(5); // max quality by default
    }

    private void OnEnable()
    {
        if (slider == null)
        {
            Debug.Log("Get slider");
            slider = gameObject.GetComponent<Slider>();
        }
        slider.value = QualitySettings.GetQualityLevel();
        
    }

    private void OnDisable()
    {

    }

    public void SetQualityLevelFromSlider()
    {
        QualitySettings.SetQualityLevel((int)slider.value, true);
        Debug.Log("quality level = " + QualitySettings.GetQualityLevel());
        Debug.Log("soft particles = " + QualitySettings.softParticles);
    }
}
