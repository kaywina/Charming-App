using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public CurrencyManager currencyManager;
    public GameObject menuCanvas;

    public PlayGame playGameAttention;

    public Sprite[] romanNumeralSprites;
    public Sprite hideSprite;

    public ParticleSystem fireworks;

    private static int gameCostNoGold = 4;
    private static int gameCostGold = 0;

    public void OnEnable()
    {
        playGameAttention.gameObject.SetActive(false);
    }

    public static int GetGameCost()
    {
        if (IsGold()) { return gameCostGold; }
        else { return gameCostNoGold;  }
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

    private static bool IsGold()
    {
        if (PlayerPrefs.GetString(UnityIAPController.goldSubscriptionPlayerPref) == "true")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckGameCost()
    {
        if (IsGold())
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
