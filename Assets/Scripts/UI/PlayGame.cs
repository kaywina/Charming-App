using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    public string gameName;
    public Text scoreText;
    public GameObject scoreIndicator;
    public GameObject instructions;
    public GameObject gameControls;
    public GameObject highScoreDisplay;
    public GameObject playButton;

    private string highScoreDataTag = "_highscore"; // this is used to define the player pref name; so don't change it in production

    void OnEnable()
    {
        Reset();
        scoreIndicator.SetActive(false);
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
        scoreIndicator.SetActive(true);
    }

    public void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt(gameName + highScoreDataTag, score);
    }

    public int LoadHighScore()
    {
        return PlayerPrefs.GetInt(gameName + highScoreDataTag);
    }

    // returns true if there is a new high score
    public bool CheckScore(int score)
    {
        if (score > PlayerPrefs.GetInt(gameName + highScoreDataTag)) { return true; }
        else { return false; }
    }

    public void EndGame()
    {
        Debug.Log("That's the end of the game");
        gameControls.SetActive(false);
        highScoreDisplay.SetActive(true);
        playButton.SetActive(true);
    }
}
