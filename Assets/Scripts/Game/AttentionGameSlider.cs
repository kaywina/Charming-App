using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttentionGameSlider : MonoBehaviour
{
    public Slider slider;
    public AttentionGame attentionGame;
    public Text secondsText;
    public GameObject showIfLocked;
    public GameObject objecttoLock;

    private void OnEnable()
    {
        int seconds = attentionGame.GetSecondsToCoundDown();
        slider.value = seconds;
        secondsText.text = seconds.ToString();
        LockObjectsBasedOnRank();
    }

    public void SetSecondsFromSlider()
    {
        int seconds = (int)slider.value;
        attentionGame.SetSecondsToCountdown(seconds); // slider should be set to whole numbers only in inspector
        secondsText.text = seconds.ToString();
        LockObjectsBasedOnRank();
    }

    private void LockObjectsBasedOnRank()
    {
        // there is where we control whether to show the instructions and paly button
        int rank = RankManager.GetRank();

        switch (rank)
        {
            case 0:
                CheckValue(6);
                break;
            case 1:
                CheckValue(5);
                break;
            case 2:
                CheckValue(4);
                break;
            case 3:
                CheckValue(3);
                break;
            case 4:
                CheckValue(2);
                break;
            case 5:
                CheckValue(1);
                break;
            default:
                CheckValue(1);
                break;
        }
    }

    private void CheckValue(int limit)
    {
        if (slider.value < limit)
        {
            Lock();
        }
        else
        {
            Unlock();
        }
    }

    private void Lock()
    {
        showIfLocked.SetActive(true);
        objecttoLock.SetActive(false);
    }

    private void Unlock()
    {
        showIfLocked.SetActive(false);
        objecttoLock.SetActive(true);
    }
}
