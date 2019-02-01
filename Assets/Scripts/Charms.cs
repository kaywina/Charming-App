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

    private string[] charmNames = {"Love", "Grace", "Patience", "Wisdom", "Joy", "Focus", "Will", "Guile", "Force" };

	// For swappable icons on stand
	public GameObject love;
    public GameObject grace;
    public GameObject patience;
    public GameObject wisdom;
    public GameObject joy;
    public GameObject focus;
    public GameObject will;
    public GameObject guile;
    public GameObject force;

	private float swapSpeed = 0.5f;

    private bool loaded = false;

    public bool clearPlayerPrefsOnPlayInEditor = false;

	// Use this for initialization
	void Start () {

#if UNITY_EDITOR
        if (clearPlayerPrefsOnPlayInEditor == true) { PlayerPrefs.DeleteAll(); }
#endif

        // set a default so don't get loc error when data cleared on Android
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("Charm"))) {
            PlayerPrefs.SetString("Charm", "Love");
        }

        SetCharm(PlayerPrefs.GetString("Charm"));
        loaded = true;
    }
	
	// Update is called once per frame
	void Update () {
		GetInput ();
	}
	
	void GetInput() {
        #if UNITY_EDITOR // Mouse Input
        if (Input.GetMouseButtonDown (0))
        {
			GetTapStarted(Input.mousePosition);
		}
		if (Input.GetMouseButtonUp (0))
        {
			GetTapEnded (Input.mousePosition);
		}
        #endif

        #if UNITY_EDITOR // disable cheats in builds
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            CurrencyManager.Instance.GiveBonus(100);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            CurrencyManager.Instance.ClearCurrency();
        }
        #endif

        // Touch Input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
			GetTapStarted(Input.GetTouch(0).position);
		}
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
			GetTapEnded(Input.GetTouch(0).position);
		}
		
		if (Input.GetKeyDown (KeyCode.Escape))
        {
			Application.Quit ();
		}
		
	}
	
	// Start animations on button tap/click
	void GetTapStarted(Vector2 position) {
		ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out hit))
        {
            for (int i = 0; i < charmNames.Length; i++)
            {
               if (hit.transform.name == charmNames[i])
                {
                    SetCharm(charmNames[i]);
                    break;
                }
            }
		}
	}

	// Start animations on button tap/click
	void GetTapEnded(Vector2 position) {

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
		default:
			mainCamera.transform.DOMoveX(love.transform.position.x, swapSpeed);
            charmDescriptionLocText.SetLocalizationKey("LOVE_DESC");
            break;
		}

        charmNameLocText.SetLocalizationKey(charmName.ToUpper());
        PlayerPrefs.SetString("Charm", charmName);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
