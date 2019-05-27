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
                if (love != null) { love.SetActive(enable); }
                break;
            case "Grace":
                if (grace != null) { grace.SetActive(enable); }
                break;
            case "Patience":
                if (patience != null) { patience.SetActive(enable); }
                break;
            case "Wisdom":
                if (wisdom != null) { wisdom.SetActive(enable); }
                break;
            case "Joy":
                if (joy != null) { joy.SetActive(enable); }
                break;
            case "Focus":
                if (focus != null) { focus.SetActive(enable); }
                break;
            case "Will":
                if (will != null) { will.SetActive(enable); }
                break;
            case "Guile":
                if (guile != null) { guile.SetActive(enable); }
                break;
            case "Force":
                if (force != null) { force.SetActive(enable); }
                break;
            case "Honor":
                if (honor != null) { honor.SetActive(enable); }
                break;
            case "Faith":
                if (faith != null) { faith.SetActive(enable); }
                break;
            case "Vision":
                if (vision != null) { vision.SetActive(enable); }
                break;
            case "Balance":
                if (balance != null) { balance.SetActive(enable); }
                break;
            case "Harmony":
                if (harmony != null) { harmony.SetActive(enable); }
                break;
            case "Regard":
                if (regard != null) { regard.SetActive(enable); }
                break;
            case "Insight":
                if (insight != null) { insight.SetActive(enable); }
                break;
            case "Plenty":
                if (plenty != null) { plenty.SetActive(enable); }
                break;
            case "Influence":
                if (influence != null) { influence.SetActive(enable); }
                break;
            default:
                Debug.Log("This is not the case you are looking for");
                break;
        }
    }
}
