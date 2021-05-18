using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public CurrencyManager currencyManager;
    public GameObject menuCanvasObject;

    public PlayPanel playPanel;

    public PlayGame playAttentionGame;
    public PlayGame playRememberGame;

    public AttentionGameManager attentionGameManager;
    public RememberGameManager rememberGameManager;

    public GameObject rewardedAdButton;
    public GameObject watchedRewardedAdText;

    public Sprite[] romanNumeralSprites;
    public Sprite hideSprite;

    public ParticleSystem fireworks;

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
    }

    public void OnEnable()
    {
        SetupGameSelect();
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
        attentionGameManager.PlayGame();
    }

    public void PlayGameRemember(bool onInstructions = false)
    {
        rememberGameManager.PlayGame(onInstructions);
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
