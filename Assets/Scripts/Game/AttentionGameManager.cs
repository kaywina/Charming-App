using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionGameManager : MonoBehaviour
{
    public GameObject instructions;

    public void OnEnable()
    {
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
