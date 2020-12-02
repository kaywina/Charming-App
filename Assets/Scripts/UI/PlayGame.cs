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

    // persistent data of these values are handled as a special case in DataManager
    private string highScoreDataTag = "_highscore"; // this is used to define the player pref name; so don't change it in production
    private string previousHighScoreDataTag = "_previous_highscore"; // this is used to define the player pref name; so don't change it in production
    private string nextRewardPlayerPrefTag = "_nextreward"; // don't change in production

    private int maxReward = 32;

    public string GetHighScorePlayerPrefName()
    {
        return gameName + highScoreDataTag;
    }

    public string GetPreviousHighScorePlayerPrefName()
    {
        return gameName + previousHighScoreDataTag;
    }

    public string GetNextRewardPlayerPrefName()
    {
        return gameName + nextRewardPlayerPrefTag;
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(GetNextRewardPlayerPrefName()))
        {
            PlayerPrefs.SetInt(GetNextRewardPlayerPrefName(), 2); // 2 is first reward
        }
    }

    private void OnDisable()
    {
        playManager.StopFireworks();
    }

    public void Reset()
    {
        //Debug.Log("Reset game via PlayGame script");
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

    public void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt(GetHighScorePlayerPrefName(), score);
    }

    public int LoadHighScore()
    {
        return PlayerPrefs.GetInt(GetHighScorePlayerPrefName());
    }

    public void SavePreviousHighScore(int score)
    {
        PlayerPrefs.SetInt(GetPreviousHighScorePlayerPrefName(), score);
    }

    public int LoadPreviousHighScore()
    {
        return PlayerPrefs.GetInt(GetPreviousHighScorePlayerPrefName());
    }

    // returns true if there is a new high score
    public bool CheckScore(int score)
    {
        if (!PlayerPrefs.HasKey(GetHighScorePlayerPrefName()) && score != 0 
                || score > PlayerPrefs.GetInt(GetHighScorePlayerPrefName()))
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

        // check if there is a new high score and save it if so
        if (CheckScore(score))
        {
            SaveHighScore(score);
        }

        // show the previous high score and save it
        previousHighScoreText.text = LoadPreviousHighScore().ToString();
        SavePreviousHighScore(PlayerPrefs.GetInt(GetHighScorePlayerPrefName())); // need to do this after updating text

        // show the high score display
        SetRewardUI(); // this shows different UI depending on if user got a new high score or not
        ShowYourScoreDisplay(score);
    }


    public int GetRewardAmount()
    {
        return PlayerPrefs.GetInt(GetNextRewardPlayerPrefName());
    }

    public void SetNextRewardAmount()
    {
        //Debug.Log("Set next reward amount");
        int reward = PlayerPrefs.GetInt(GetNextRewardPlayerPrefName());
        // only go to next reward size if not exceeded max reward size; otherwise reward just stays at max
        int doubleReward = reward * 2;
        if (doubleReward <= maxReward)
        {
            PlayerPrefs.SetInt(GetNextRewardPlayerPrefName(), doubleReward);
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
