using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class Charms : MonoBehaviour {

	public Camera mainCamera;

	public TextMesh charmNameText;
	public TextMesh charmDescriptionText;

	// For Tap Interface
	private Ray ray;
	private RaycastHit hit;

    private string[] charmNames = {"Love", "Grace", "Patience", "Wisdom", "Joy", "Focus", "Will", "Guile", "Power" };

	// For swappable icons on stand
	public GameObject love;
    public GameObject grace;
    public GameObject patience;
    public GameObject wisdom;
    public GameObject joy;
    public GameObject focus;
    public GameObject will;
    public GameObject guile;
    public GameObject power;

	private float swapSpeed = 0.5f;

    private bool onFirstLoad = true;

	// Use this for initialization
	void Start () {
        //PlayerPrefs.SetString("Charm", "");

        if (String.IsNullOrEmpty(PlayerPrefs.GetString("Charm"))) {
            PlayerPrefs.SetString("Charm", charmNames[0]); // default is Love
        }
		SetCharm (PlayerPrefs.GetString ("Charm"));
        onFirstLoad = false;
	}
	
	// Update is called once per frame
	void Update () {
		GetInput ();
	}
	
	void GetInput() {
        #if UNITY_EDITOR // Mouse Input
        if (Input.GetMouseButtonDown (0))
        {
			GetTapStarted();
		}
		if (Input.GetMouseButtonUp (0))
        {
			GetTapEnded ();
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
			GetTapStarted();
		}
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
			GetTapEnded();
		}
		
		if (Input.GetKeyDown (KeyCode.Escape))
        {
			Application.Quit ();
		}
		
	}
	
	// Start animations on button tap/click
	void GetTapStarted() {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
	void GetTapEnded() {

	}

	public void SetCharm(string charmName) {

        if (!onFirstLoad && charmName == PlayerPrefs.GetString("Charm"))
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
			charmDescriptionText.text = "Good luck friend";
			break;
        case "Grace":
            mainCamera.transform.DOMoveX(grace.transform.position.x, swapSpeed);
            charmDescriptionText.text = "Flow like water";
            break;
        case "Patience":
            mainCamera.transform.DOMoveX(patience.transform.position.x, swapSpeed);
            charmDescriptionText.text = "Wait for change";
            break;
        case "Wisdom":
            mainCamera.transform.DOMoveX(wisdom.transform.position.x, swapSpeed);
            charmDescriptionText.text = "Know what's right";
            break;
        case "Joy":
            mainCamera.transform.DOMoveX(joy.transform.position.x, swapSpeed);
            charmDescriptionText.text = "Witness the good";
            break;
        case "Focus":
            mainCamera.transform.DOMoveX(focus.transform.position.x, swapSpeed);
            charmDescriptionText.text = "Stay on task";
            break;
        case "Will":
            mainCamera.transform.DOMoveX(will.transform.position.x, swapSpeed);
            charmDescriptionText.text = "Endure hard times";
            break;
        case "Guile":
            mainCamera.transform.DOMoveX(guile.transform.position.x, swapSpeed);
            charmDescriptionText.text = "Act the part";
            break;
        case "Power":
		    mainCamera.transform.DOMoveX(power.transform.position.x, swapSpeed);
		    charmDescriptionText.text = "Grow your might";
		    break;
		default:
			mainCamera.transform.DOMoveX(love.transform.position.x, swapSpeed);
			charmDescriptionText.text = "Make sweet love";
			break;
		}

        charmNameText.text = charmName;
        PlayerPrefs.SetString("Charm",charmName);
    }

    public void Quit()
    {
        //Debug.Log("Quit");
        Application.Quit();
    }
}
