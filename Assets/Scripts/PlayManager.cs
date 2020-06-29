using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public GameObject menuCanvas;

    public PlayGame playGameAttention;

    public Sprite[] romanNumeralSprites;
    public Sprite hideSprite;

    public ParticleSystem fireworks;

    private int gameCostNoGold = 2;
    private int gameCostGold = 1;

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
        if (CheckGameCost())
        {
            playGameAttention.Play();
        } 
    }

    private bool CheckGameCost()
    {
        if (PlayerPrefs.GetString(UnityIAPController.goldSubscriptionPlayerPref) == "true")
        {
            if (CurrencyManager.WithdrawAmount(gameCostGold))
            {
                return true;
            }
        }
        else
        {
            if (CurrencyManager.WithdrawAmount(gameCostNoGold))
            {
                return true;
            }
        }
        return false;
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
