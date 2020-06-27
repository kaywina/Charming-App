using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    public Text scoreText;
    public GameObject instructions;
    public GameObject gameControls;

    void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        gameControls.SetActive(false);
        instructions.SetActive(true);
    }

    public void Play()
    {
        instructions.SetActive(false);
        gameControls.SetActive(true);
    }
}
