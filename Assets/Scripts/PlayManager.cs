using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public GameObject[] games;
    public GameObject menuCanvas;

    private int gameCost = 2;

    public void PlayGameByIndex(int index)
    {
        menuCanvas.SetActive(false);
        games[index].SetActive(true);
        CurrencyManager.WithdrawAmount(gameCost);
    }

    public void QuitGameByIndex(int index)
    {
        games[index].SetActive(false);
        menuCanvas.SetActive(true);
    }
}
