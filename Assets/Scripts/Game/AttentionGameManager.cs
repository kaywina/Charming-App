using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionGameManager : MonoBehaviour
{
    public GameObject instructions;
    public PlayGame attentionPlayGame;

    public void OnEnable()
    {
        attentionPlayGame.Reset();
        ShowInstructions();
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
