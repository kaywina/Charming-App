using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public GameObject menuCanvas;

    public PlayGame playGameAttention;
    public GameAttention gameAttention;

    public Sprite[] romanNumeralSprites;
    public Sprite hideSprite;

    public ParticleSystem fireworks;

    private int gameCost = 2;

    public void OnEnable()
    {
        playGameAttention.gameObject.SetActive(false);
    }

    public void CloseGameSelectMenu()
    {
        menuCanvas.SetActive(false);
    }

    public void OpenGameAttention()
    {
        CloseGameSelectMenu();
        playGameAttention.gameObject.SetActive(true);
    }

    public void PlayGameAttention()
    {
        gameAttention.ResetGame();
        playGameAttention.Play();
        CurrencyManager.WithdrawAmount(gameCost);
    }

    public Sprite GetSpriteByIndex(int index)
    {
        return romanNumeralSprites[index];
    }

    public Sprite GetHideSprite()
    {
        return hideSprite;
    }

    public void StartFireworks()
    {
        fireworks.Play();
    }

    public void StopFireworks()
    {
        if (fireworks != null) { fireworks.Stop(); }
    }
}
