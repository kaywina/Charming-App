using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditatePanel : CharmsPanel
{
    public GameObject charmButtons;
    public GameObject subscribePanel;

    // world space models for Meditation screen
    public GameObject[] charms;

    public SetParticleColorFromCharm particleColor;

    new void OnEnable()
    {
        SetCharmModel(true);
        base.OnEnable();
        charmButtons.SetActive(false);
        worldUI.SetActive(true);
        Screen.sleepTimeout = SleepTimeout.NeverSleep; // prevent device from automatically going to sleep
    }

    new void OnDisable()
    {
        SetCharmModel(false);

        Screen.sleepTimeout = SleepTimeout.SystemSetting; // allow device to go to sleep again

        // gold subscribers return to main UI, non-subscribers are directed to subscribe panel
        if (UnityIAPController.IsGold())
        {
            if (charmButtons != null) { charmButtons.SetActive(true); }
            base.OnDisable();
        }
        else
        {
            DeactivateObjects();
            subscribePanel.SetActive(true);
        }
    }

    void SetCharmModel(bool enable)
    {
        string charmName = PlayerPrefs.GetString("Charm");

        if (string.IsNullOrEmpty(charmName))
        {
            Debug.Log("Charm name is null or empty when opening meditation panel");
            return;
        }

        EnableCharm(charmName, enable);
    }

    private void DisableAllCharms()
    {
        for (int i = 0; i < charms.Length; i++)
        {

        }
    }

    void EnableCharm (string charmName, bool enable)
    {
        for (int i = 0; i < charms.Length; i++)
        {
            if (charms[i] == null)
            {
                //Debug.Log("Charm is null; handle exception and do nothing on app shutdown");
                return;
            }

            if (charms[i].name == charmName)
            {
                charms[i].SetActive(true);
                particleColor.SetColor(charmName);
            }
            else
            {
                charms[i].SetActive(false);
            }
        }
    }
}
