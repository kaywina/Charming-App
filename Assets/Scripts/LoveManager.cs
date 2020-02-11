using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoveManager : MonoBehaviour
{

    public LocalizationText loveText;

    private const string PLAYER_PREF_NAME = "LoveNumber"; // don't change in production
    private const string PLAYER_PREF_NAME_MAX_UNLOCKED = "LoveNumberMaxUnlocked"; // don't change in production

    private int maxLocStrings = 69;
    private static bool unlockedThisSession = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (!unlockedThisSession)
        {
            if (PlayerPrefs.GetInt(PLAYER_PREF_NAME) > maxLocStrings)
            {
                PlayerPrefs.SetInt(PLAYER_PREF_NAME, 1);
            }
            else
            {
                int newIndex = PlayerPrefs.GetInt(PLAYER_PREF_NAME) + 1;

                PlayerPrefs.SetInt(PLAYER_PREF_NAME, newIndex);

                if (newIndex > PlayerPrefs.GetInt(PLAYER_PREF_NAME_MAX_UNLOCKED))
                {
                    PlayerPrefs.SetInt(PLAYER_PREF_NAME_MAX_UNLOCKED, newIndex);
                }
            }
            unlockedThisSession = true;
        }

        string locKey = "LOVE_" + PlayerPrefs.GetInt(PLAYER_PREF_NAME).ToString();
        loveText.SetLocalizationKey(locKey);
    }
}
