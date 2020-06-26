using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    public Text scoreText;
    public GameObject instructions;
    public GameObject gameControls;

    public Text countdownText;
    int countdown = 3;

    void OnEnable()
    {
        Reset();
    }

    void OnDisable()
    {

    }

    public void Reset()
    {
        gameControls.SetActive(false);
        countdown = 3;
        countdownText.text = countdown.ToString();
        instructions.SetActive(true);
        CountdownToPlay();
    }

    private void CountdownToPlay()
    {
        InvokeRepeating("CountDownByOne", 1f, 1f);
    }

    private void CountDownByOne()
    {
        countdown--;
        if (countdown <= 0)
        {
            StartGame();
        }
        else
        {
            countdownText.text = countdown.ToString();
        }
    }

    private void StartGame()
    {
        instructions.SetActive(false);
        gameControls.SetActive(true);
        CancelInvoke("CountDownByOne");
    }
}
