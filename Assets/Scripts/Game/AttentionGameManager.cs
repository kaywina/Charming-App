using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionGameManager : MonoBehaviour
{
    public GameObject instructions;
    public GameObject highScoreDisplay;
    public PlayGame attentionPlayGame;
    public AttentionGame attentionGameControls;

    private void OnEnable()
    {
        ResetToInstructions();
    }

    private void OnDisable()
    {
        ResetToInstructions();
    }

    public void PlayGame()
    {
        //Debug.Log("Play game Attention");
        HideInstructions();
        attentionPlayGame.Reset();
        attentionGameControls.gameObject.SetActive(true);
    }

    public void ShowInstructions()
    {
        instructions.SetActive(true);
    }

    public void HideInstructions()
    {
        instructions.SetActive(false);
    }

    public void ResetToInstructions()
    {
        highScoreDisplay.SetActive(false);
        attentionGameControls.gameObject.SetActive(false);
        instructions.SetActive(true);
    }
}
