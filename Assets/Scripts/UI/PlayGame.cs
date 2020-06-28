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
    public GameObject newHighScore;
    public GameObject niceTry;
    public GameObject playButton;

    bool newHighScoreFlag = false;

    private string highScoreDataTag = "_highscore"; // this is used to define the player pref name; so don't change it in production

    void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        gameControls.SetActive(false);
        instructions.SetActive(true);
        playButton.SetActive(true);
        scoreIndicator.SetActive(false);
        highScoreDisplay.SetActive(false);
        newHighScore.SetActive(false);
        niceTry.SetActive(false);
        newHighScoreFlag = false;
    }

    public void Play()
    {
        highScoreDisplay.SetActive(false);
        newHighScore.SetActive(false);
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
        if (score > PlayerPrefs.GetInt(gameName + highScoreDataTag))
        {
            newHighScoreFlag = true;
            return true;
        }
        else { return false; }
    }

    public void ShowHighScoreDisplay()
    {
        highScoreDisplay.SetActive(true);
    }

    public void ShowNewHighScore()
    {
        newHighScore.SetActive(true);
    }

    public void EndGame()
    {
        Debug.Log("That's the end of the game");
        gameControls.SetActive(false);
        highScoreDisplay.SetActive(true);
        playButton.SetActive(true);

        // show different UI depending on if user got a personal high score or not
        if (newHighScoreFlag)
        {
            newHighScore.SetActive(true);
            niceTry.SetActive(false);
        }
        else
        {
            newHighScore.SetActive(false);
            niceTry.SetActive(true);
        }

        ShowHighScoreDisplay();
    }
}
