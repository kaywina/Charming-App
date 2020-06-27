using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    public Text scoreText;
    public GameObject instructions;
    public GameObject gameControls;
    public GameObject highScoreDisplay;
    public GameObject playButton;

    void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        gameControls.SetActive(false);
        instructions.SetActive(true);
        playButton.SetActive(true);
        highScoreDisplay.SetActive(false);
    }

    public void Play()
    {
        highScoreDisplay.SetActive(false);
        instructions.SetActive(false);
        gameControls.SetActive(true);
        playButton.SetActive(false);
    }

    public void EndGame()
    {
        Debug.Log("That's the end of the game");
        gameControls.SetActive(false);
        highScoreDisplay.SetActive(true);
        playButton.SetActive(true);
    }
}
