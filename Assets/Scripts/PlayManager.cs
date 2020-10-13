using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public CurrencyManager currencyManager;
    public GameObject menuCanvasObject;

    public PlayGame playAttentionGame;
    public PlayGame playRememberGame;

    public AttentionGameManager attentionGameManager;
    public RememberGameManager rememberGameManager;

    public GameObject rewardedAdButton;
    public GameObject watchedRewardedAdText;
    public GameObject moreCurrencyText;

    public Sprite[] romanNumeralSprites;
    public Sprite hideSprite;

    public ParticleSystem fireworks;

    private static int gameCostNoGold = 4; // amount to play a game for free users
    private static int gameCostGold = 0; // amount to play a game for subscribers (set to free) - don't change this in production!

    private static int adReward = 8; // amount free users get for watching a rewarded ad

    private void Awake()
    {
        attentionGameManager.gameObject.SetActive(true); // always set game manager active in scene by default
    }

    public void SetupGameSelect()
    {
        rewardedAdButton.SetActive(false);
        watchedRewardedAdText.SetActive(false);
        menuCanvasObject.SetActive(true);
        moreCurrencyText.SetActive(false);

        playAttentionGame.gameObject.SetActive(false);
        playRememberGame.gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        SetupGameSelect();
    }

    public static int GetGameCost()
    {
        if (IsGold()) { return gameCostGold; }
        else { return gameCostNoGold;  }
    }

    public void ReturnToGameSelect()
    {
        watchedRewardedAdText.SetActive(false);
        if (UnityAdsController.GetAllowAds()) {
            moreCurrencyText.SetActive(false);
            rewardedAdButton.SetActive(true);
        }  // only show ad if user has opted-in and AllowAds is true
        else {
            moreCurrencyText.SetActive(true);
            rewardedAdButton.SetActive(false);
        }
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
        attentionGameManager.gameObject.SetActive(true);
        playAttentionGame.gameObject.SetActive(true);
    }

    public void OpenGameMemory()
    {
        CloseGameSelectMenu();
        rememberGameManager.gameObject.SetActive(true);
        playRememberGame.gameObject.SetActive(true);
    }

    public void PlayGameAttention()
    {
        if (CheckGameCost())
        {
            attentionGameManager.PlayGame();
        }
        else
        {
            playAttentionGame.gameObject.SetActive(false);
            ReturnToGameSelect();
        }
    }

    public void PlayGameRemember()
    {
        if (CheckGameCost())
        {
            rememberGameManager.PlayGame();
        }
        else
        {
            playRememberGame.gameObject.SetActive(false);
            ReturnToGameSelect();
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
