using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttentionGameSlider : MonoBehaviour
{
    public Slider slider;
    public AttentionGame attentionGame;
    public Text secondsText;

    private void OnEnable()
    {
        int seconds = attentionGame.GetSecondsToCoundDown();
        slider.value = seconds;
        secondsText.text = seconds.ToString();
    }

    public void SetSecondsFromSlider()
    {
        int seconds = (int)slider.value;
        attentionGame.SetSecondsToCountdown(seconds); // slider should be set to whole numbers only in inspector
        secondsText.text = seconds.ToString();
    }
}
