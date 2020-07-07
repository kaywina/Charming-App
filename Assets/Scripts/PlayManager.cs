using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public CurrencyManager currencyManager;
    public GameObject menuCanvasObject;

    public PlayGame playGameAttention;

    public GameObject rewardedAdButton;
    public GameObject watchedRewardedAdText;

    public Sprite[] romanNumeralSprites;
    public Sprite hideSprite;

    public ParticleSystem fireworks;

    private static int gameCostNoGold = 4; // amount to play a game for free users
    private static int gameCostGold = 0; // amount to play a game for subscribers (set to free) - don't change this in production!

    private static int adReward = 8; // amount free users get for watching a rewarded ad

    public void OnEnable()
    {
        rewardedAdButton.SetActive(false);
        watchedRewardedAdText.SetActive(false);
        playGameAttention.gameObject.SetActive(false);
        menuCanvasObject.SetActive(true);
    }

    public static int GetGameCost()
    {
        if (IsGold()) { return gameCostGold; }
        else { return gameCostNoGold;  }
    }

    public void OpenGameSelectMenu(bool showAdButton)
    {
        watchedRewardedAdText.SetActive(false);
        if (showAdButton) { rewardedAdButton.SetActive(true); }
        menuCanvasObject.SetActive(true);
    }

    public void CloseGameSelectMenu()
    {
        rewardedAdButton.SetActive(false);
        watchedRewardedAdText.SetActive(false);
        menuCanvasObject.SetActive(false);
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
        else
        {
            playGameAttention.gameObject.SetActive(false);
            OpenGameSelectMenu(true);
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

    public void ShowRewardedAdButton()
    {
        rewardedAdButton.SetActive(true);
    }

    public static int GetAdReward()
    {
        return adReward;
    }
}
