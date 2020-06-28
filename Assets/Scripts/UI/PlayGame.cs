using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    public string gameName;
    public PlayManager playManager;
    public Text scoreText;
    public Text highScoreText;
    public Text previousHighScoreText;
    public GameObject perfectIndicator;
    public GameObject scoreIndicator;
    public GameObject instructions;
    public GameObject gameControls;
    public GameObject highScoreDisplay;
    public GameObject newHighScore;
    public GameObject niceTry;
    public GameObject playButton;

    bool newHighScoreFlag = false;

    private string highScoreDataTag = "_highscore"; // this is used to define the player pref name; so don't change it in production
    private string previousHighScoreDataTag = "_previous_highscore"; // this is used to define the player pref name; so don't change it in production

    void OnEnable()
    {
        Reset();
    }

    private void OnDisable()
    {
        playManager.StopFireworks();
    }

    public void Reset()
    {
        playManager.StopFireworks();
        gameControls.SetActive(false);
        instructions.SetActive(true);
        playButton.SetActive(true);
        scoreIndicator.SetActive(false);
        highScoreDisplay.SetActive(false);
        newHighScore.SetActive(false);
        niceTry.SetActive(false);
        perfectIndicator.SetActive(false);
        newHighScoreFlag = false;
    }

    public void Play()
    {
        Reset();
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

    public void SavePreviousHighScore(int score)
    {
        PlayerPrefs.SetInt(gameName + previousHighScoreDataTag, score);
    }

    public int LoadPreviousHighScore()
    {
        return PlayerPrefs.GetInt(gameName + previousHighScoreDataTag);
    }

    // returns true if there is a new high score
    public bool CheckScore(int score)
    {
        if (!PlayerPrefs.HasKey(gameName + highScoreDataTag) 
                || score > PlayerPrefs.GetInt(gameName + highScoreDataTag))
        {
            newHighScoreFlag = true;
            return true;
        }
        else { return false; }
    }

    public void ShowHighScoreDisplay()
    {
        highScoreText.text = PlayerPrefs.GetInt(gameName + highScoreDataTag).ToString();
        highScoreDisplay.SetActive(true);
    }

    public void ShowNewHighScore()
    {
        newHighScore.SetActive(true);
    }

    public void ShowPerfectIndicator()
    {
        perfectIndicator.SetActive(true);
    }

    public void EndGame()
    {
        Debug.Log("That's the end of the game");
        
        gameControls.SetActive(false);

        // show different UI depending on if user got a personal high score or not
        if (newHighScoreFlag)
        {
            playManager.StartFireworks();
            newHighScore.SetActive(true);
            niceTry.SetActive(false);
        }
        else
        {
            newHighScore.SetActive(false);
            niceTry.SetActive(true);
        }

        previousHighScoreText.text = LoadPreviousHighScore().ToString();
        SavePreviousHighScore(PlayerPrefs.GetInt(gameName + highScoreDataTag)); // need to do this after updating text
        ShowHighScoreDisplay();
        playButton.SetActive(true);
    }
}
