using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public GameObject[] games;
    public GameObject menuCanvas;
    public GameObject scoreIndicator;

    public Sprite[] romanNumeralSprites;
    public Sprite hideSprite;

    private int gameCost = 2;

    public void OnEnable()
    {
        for (int i = 0; i < games.Length; i++)
        {
            games[i].SetActive(false);
        }
    }

    public void PlayGameByIndex(int index)
    {
        menuCanvas.SetActive(false);
        games[index].SetActive(true);
        CurrencyManager.WithdrawAmount(gameCost);
        scoreIndicator.SetActive(true);
    }

    public void QuitGameByIndex(int index)
    {
        games[index].SetActive(false);
        menuCanvas.SetActive(true);
    }

    public Sprite GetSpriteByIndex(int index)
    {
        return romanNumeralSprites[index];
    }

    public Sprite GetHideSprite()
    {
        return hideSprite;
    }

}
