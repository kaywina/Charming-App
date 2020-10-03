using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    public string gameName; // don't change these inspector values in production! (or else people will lose their high score data)
    public PlayManager playManager;
    public Text yourScoreText;
    public Text previousHighScoreText;
    public GameObject perfectIndicator;
    public GameObject awesomeIndicator;
    public GameObject gameControls;
    public GameObject highScoreDisplay;
    public GameObject newHighScore;
    public GameObject niceTry;

    public GameObject rewardObject;
    public Text rewardText;
    public GameObject nextRewardObject;
    public Text nextRewardText;
    public GameObject beatScoreText;

    bool newHighScoreFlag = false;
    private bool perfectGameFlag = false;

    private string highScoreDataTag = "_highscore"; // this is used to define the player pref name; so don't change it in production
    private string previousHighScoreDataTag = "_previous_highscore"; // this is used to define the player pref name; so don't change it in production
    private string nextRewardPlayerPrefTag = "_nextreward"; // don't change in production

    private int maxReward = 32;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(gameName + nextRewardPlayerPrefTag))
        {
            PlayerPrefs.SetInt(gameName + nextRewardPlayerPrefTag, 2); // 2 is first reward
        }
    }

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
        Debug.Log("Reset game");
        newHighScoreFlag = false;
        perfectGameFlag = false;
        playManager.StopFireworks();
        gameControls.SetActive(false);
        highScoreDisplay.SetActive(false);
        newHighScore.SetActive(false);
        niceTry.SetActive(false);
        perfectIndicator.SetActive(false);
        awesomeIndicator.SetActive(false);
        rewardObject.SetActive(false);
        nextRewardObject.SetActive(false);
        beatScoreText.gameObject.SetActive(false);
    }

    public void Play()
    {
        Reset();
        gameControls.SetActive(true);
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
        if (!PlayerPrefs.HasKey(gameName + highScoreDataTag) && score != 0 
                || score > PlayerPrefs.GetInt(gameName + highScoreDataTag))
        {
            newHighScoreFlag = true;
            return true;
        }
        else { return false; }
    }

    public void ShowYourScoreDisplay(int score)
    {
        yourScoreText.text = score.ToString();
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

    public void EndGame(int score)
    {
        //Debug.Log("That's the end of the game");
        gameControls.SetActive(false);

        SetRewardUI(); // this shows different UI depending on if user got a new high score or not

        previousHighScoreText.text = LoadPreviousHighScore().ToString();
        SavePreviousHighScore(PlayerPrefs.GetInt(gameName + highScoreDataTag)); // need to do this after updating text
        ShowYourScoreDisplay(score);
    }


    public int GetRewardAmount()
    {
        string rewardPlayerPrefName = gameName + nextRewardPlayerPrefTag;
        return PlayerPrefs.GetInt(rewardPlayerPrefName);
    }

    public void SetNextRewardAmount()
    {
        //Debug.Log("Set next reward amount");
        string rewardPlayerPrefName = gameName + nextRewardPlayerPrefTag;
        int reward = PlayerPrefs.GetInt(rewardPlayerPrefName);
        // only go to next reward size if not exceeded max reward size; otherwise reward just stays at max
        int doubleReward = reward * 2;
        if (doubleReward <= maxReward)
        {
            PlayerPrefs.SetInt(rewardPlayerPrefName, doubleReward);
        }
    }

    // this needs to be called after CheckScore() so that the flag has been updated
    public void SetRewardUI()
    {
        int rewardAmount = GetRewardAmount();

        if (newHighScoreFlag)
        {
            CurrencyManager.Instance.GiveBonus(rewardAmount);
            SetNextRewardAmount();
            rewardText.text = rewardAmount.ToString();
            rewardObject.SetActive(true);
            beatScoreText.gameObject.SetActive(false);
            playManager.StartFireworks();
            newHighScore.SetActive(true);
            niceTry.SetActive(false);
            nextRewardText.text = GetRewardAmount().ToString();
            if (perfectGameFlag)
            {
                perfectIndicator.SetActive(true);
            }
            else
            {
                awesomeIndicator.SetActive(true);
            }
        }
        else
        {
            rewardObject.SetActive(false);
            beatScoreText.gameObject.SetActive(true);
            newHighScore.SetActive(false);
            nextRewardText.text = (rewardAmount).ToString();
            if (perfectGameFlag)
            {
                perfectIndicator.SetActive(true);
            }
            else
            {
                niceTry.SetActive(true);
            }
        }

        nextRewardObject.SetActive(true);   
    }

    public void SetPerfectGameFlag(bool perfectGame)
    {
        perfectGameFlag = perfectGame;
    }
}
