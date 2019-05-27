using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditatePanel : CharmsPanel
{
    // world space models for Meditation screen
    public GameObject love;
    public GameObject grace;
    public GameObject patience;
    public GameObject wisdom;
    public GameObject joy;
    public GameObject focus;
    public GameObject will;
    public GameObject guile;
    public GameObject force;
    public GameObject honor;
    public GameObject faith;
    public GameObject vision;
    public GameObject balance;
    public GameObject harmony;
    public GameObject regard;
    public GameObject insight;
    public GameObject plenty;
    public GameObject influence;

    new void OnEnable()
    {
        SetCharmModel(true);
        base.OnEnable();
        charmText.SetActive(true); // charm text stays active on meditate screen
    }

    new void OnDisable()
    {
        SetCharmModel(false);
        base.OnDisable();
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

    void EnableCharm (string charmName, bool enable)
    {
        switch (charmName)
        {
            case "Love":
                love.SetActive(enable);
                break;
            case "Grace":
                grace.SetActive(enable);
                break;
            case "Patience":
                patience.SetActive(enable);
                break;
            case "Wisdom":
                wisdom.SetActive(enable);
                break;
            case "Joy":
                joy.SetActive(enable);
                break;
            case "Focus":
                focus.SetActive(enable);
                break;
            case "Will":
                will.SetActive(enable);
                break;
            case "Guile":
                guile.SetActive(enable);
                break;
            case "Force":
                force.SetActive(enable);
                break;
            case "Honor":
                honor.SetActive(enable);
                break;
            case "Faith":
                faith.SetActive(enable);
                break;
            case "Vision":
                vision.SetActive(enable);
                break;
            case "Balance":
                balance.SetActive(enable);
                break;
            case "Harmony":
                harmony.SetActive(enable);
                break;
            case "Regard":
                regard.SetActive(enable);
                break;
            case "Insight":
                insight.SetActive(enable);
                break;
            case "Plenty":
                plenty.SetActive(enable);
                break;
            case "Influence":
                influence.SetActive(enable);
                break;
            default:
                Debug.Log("This is not the case you are looking for");
                break;
        }
    }
}
