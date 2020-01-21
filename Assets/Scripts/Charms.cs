using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class Charms : MonoBehaviour {

	public Camera mainCamera;

	public LocalizationTextMesh charmNameLocText;
	public LocalizationTextMesh charmDescriptionLocText;

	// For Tap Interface
	private Ray ray;
	private RaycastHit hit;

    private string[] charmNames = {"Love", "Grace", "Patience", "Wisdom", "Joy", "Focus", "Will", "Guile", "Force",                 // set zero
                                    "Honor", "Faith", "Vision", "Balance", "Harmony", "Regard", "Insight", "Plenty", "Influence" }; // set one

	// For swappable icons on stand"

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

    private float swapSpeed = 0.5f;

    private bool loaded = false;

    public GameObject[] charmSets;
    public GameObject[] unlockButtonSets;
    private static string charmSetPrefName = "CharmSet";

	// Use this for initialization
	void Start () {
        // set the correct charm set depending on pref
        CheckCharmSet();

        // set a default so don't get loc error when data cleared on Android
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("Charm"))) {
            PlayerPrefs.SetString("Charm", "Love");
        }

        SetCharm(PlayerPrefs.GetString("Charm"));
        loaded = true;
    }
	
    public static string GetCharmSetPlayerPrefName()
    {
        return charmSetPrefName;
    }

    public static int GetCharmSet()
    {
        int charmSet = PlayerPrefs.GetInt(charmSetPrefName);
        return charmSet;
    }

    // enables/disables correct charm world ui button set and unlock buttons parent object based on playerpref
    void CheckCharmSet()
    {
        int charmSet = PlayerPrefs.GetInt(charmSetPrefName);
        if (charmSet < 0)
        {
            Debug.LogError("Error - stored charm set is invalid - less than zero");
        }
        if (charmSet >= charmSets.Length || charmSet >= unlockButtonSets.Length)
        {
            Debug.LogError("Error - stored charm set is invalid - longer than charmset and/or unlock buttons array");
        }
        for (int i = 0; i < charmSets.Length; i++)
        {
            if (i == charmSet)
            {
                charmSets[i].SetActive(true);
                unlockButtonSets[i].SetActive(true);
            }
            else
            {
                charmSets[i].SetActive(false);
                unlockButtonSets[i].SetActive(false);
            }
        }
    }

	public void SetCharm(string charmName) {

        // don't try to set charm if it hasn't changed
        if (loaded && charmName == PlayerPrefs.GetString("Charm"))
        {
            return;
        }

        /* Uncomment to add a cost to every time the player changes charms
        int costPerChange = 5;
        if (!onFirstLoad && !CurrencyManager.WithdrawAmount(costPerChange))
        {
            //Debug.Log("Not enough kisses");
            return;
        }
        */

        switch (charmName) {
		case "Love":
			mainCamera.transform.DOMoveX(love.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("LOVE_DESC");
			break;
        case "Grace":
            mainCamera.transform.DOMoveX(grace.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("GRACE_DESC");
            break;
        case "Patience":
            mainCamera.transform.DOMoveX(patience.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("PATIENCE_DESC");
            break;
        case "Wisdom":
            mainCamera.transform.DOMoveX(wisdom.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("WISDOM_DESC");
            break;
        case "Joy":
            mainCamera.transform.DOMoveX(joy.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("JOY_DESC");
            break;
        case "Focus":
            mainCamera.transform.DOMoveX(focus.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("FOCUS_DESC");
            break;
        case "Will":
            mainCamera.transform.DOMoveX(will.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("WILL_DESC");
            break;
        case "Guile":
            mainCamera.transform.DOMoveX(guile.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("GUILE_DESC");
            break;
        case "Force":
		    mainCamera.transform.DOMoveX(force.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("FORCE_DESC");
            break;
        case "Honor":
            mainCamera.transform.DOMoveX(honor.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("HONOR_DESC");
            break;
        case "Faith":
            mainCamera.transform.DOMoveX(faith.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("FAITH_DESC");
            break;
        case "Vision":
            mainCamera.transform.DOMoveX(vision.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("VISION_DESC");
            break;
        case "Balance":
            mainCamera.transform.DOMoveX(balance.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("BALANCE_DESC");
            break;
        case "Harmony":
            mainCamera.transform.DOMoveX(harmony.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("HARMONY_DESC");
            break;
        case "Regard":
            mainCamera.transform.DOMoveX(regard.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("REGARD_DESC");
            break;
        case "Insight":
            mainCamera.transform.DOMoveX(insight.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("INSIGHT_DESC");
            break;
        case "Plenty":
            mainCamera.transform.DOMoveX(plenty.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("PLENTY_DESC");
            break;
        case "Influence":
            mainCamera.transform.DOMoveX(influence.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("INFLUENCE_DESC");
            break;
        default:
			mainCamera.transform.DOMoveX(love.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("LOVE_DESC");
            break;
		}

        charmNameLocText.SetLocalizationKey(charmName.ToUpper());
        PlayerPrefs.SetString("Charm", charmName);
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
