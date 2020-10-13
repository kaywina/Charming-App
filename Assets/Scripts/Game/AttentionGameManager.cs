using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionGameManager : MonoBehaviour
{
    public GameObject instructions;
    public PlayGame attentionPlayGame;
    public AttentionGame attentionGameControls;

    public void PlayGame()
    {
        Debug.Log("Play game Attention");
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
}
