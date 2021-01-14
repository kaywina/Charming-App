using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public CurrencyManager currencyManager;
    public GameObject menuCanvasObject;

    public PlayPanel playPanel;

    public GameObject gameCostIndicator;

    public PlayGame playAttentionGame;
    public PlayGame playRememberGame;

    public AttentionGameManager attentionGameManager;
    public RememberGameManager rememberGameManager;

    public GameObject rewardedAdButton;
    public GameObject watchedRewardedAdText;

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

        playAttentionGame.gameObject.SetActive(false);
        playRememberGame.gameObject.SetActive(false);

        if (UnityIAPController.IsGold())
        {
            gameCostIndicator.SetActive(false);
        }
        else
        {
            gameCostIndicator.SetActive(true);
        }
    }

    public void OnEnable()
    {
        SetupGameSelect();
    }

    public static int GetGameCost()
    {
        if (UnityIAPController.IsGold()) { return gameCostGold; }
        else { return gameCostNoGold;  }
    }

    public void ReturnToGameSelect()
    {
        watchedRewardedAdText.SetActive(false);

        //Unity Ads have been disabled
        rewardedAdButton.SetActive(false);
        /* 
        if (UnityAdsController.GetAllowAds()) {
            rewardedAdButton.SetActive(true);
        }  // only show ad if user has opted-in and AllowAds is true
        else {
            rewardedAdButton.SetActive(false);
        }
        */

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
        if (CheckGameCost(false))
        {
            CloseGameSelectMenu();
            attentionGameManager.gameObject.SetActive(true);
            playAttentionGame.gameObject.SetActive(true);
        }
        else
        {
            //Debug.Log("Not enough keys");
            playPanel.OpenStoreFromGameSelect();
        }
    }

    public void OpenGameMemory()
    {
        if (CheckGameCost(false))
        {
            CloseGameSelectMenu();
            rememberGameManager.gameObject.SetActive(true);
            playRememberGame.gameObject.SetActive(true);
        }
        else
        {
            //Debug.Log("Not enough keys");
            playPanel.OpenStoreFromGameSelect();
        }
    }

    public void PlayGameAttention()
    {
        if (CheckGameCost(true))
        {
            attentionGameManager.PlayGame();
        }
        else
        {
            playAttentionGame.gameObject.SetActive(false);
            ReturnToGameSelect();
        }
    }

    public void PlayGameRemember(bool onInstructions = false)
    {
        if (CheckGameCost(true))
        {
            rememberGameManager.PlayGame(onInstructions);
        }
        else
        {
            playRememberGame.gameObject.SetActive(false);
            ReturnToGameSelect();
        }
    }

    private bool CheckGameCost(bool withdraw)
    {
        if (UnityIAPController.IsGold())
        {
            if (CurrencyManager.CanWithdrawAmount(gameCostGold))
            {
                if (withdraw) { CurrencyManager.WithdrawAmount(gameCostGold); }
                //Debug.Log("Can withdraw amount for gold subscriber");
                return true;
            }
            else
            {
                //Debug.Log("Not enough currency for gold cost");
            }
        }
        else
        {
            if (CurrencyManager.CanWithdrawAmount(gameCostNoGold))
            {
                if (withdraw) { CurrencyManager.WithdrawAmount(gameCostNoGold); }
                //Debug.Log("Can withdraw amount for non-gold subscriber");
                return true;
            }
            else
            {
                //Debug.Log("Not enough currency for non-gold cost");
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
