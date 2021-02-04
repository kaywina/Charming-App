using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttentionGameSlider : MonoBehaviour
{
    public Slider slider;
    public AttentionGame attentionGame;

    private void OnEnable()
    {
        slider.value = attentionGame.GetSecondsToCoundDown();
    }

    public void SetSecondsFromSlider()
    {
        attentionGame.SetSecondsToCountdown((int)slider.value); // slider should be set to whole numbers only in inspector
    }
}
