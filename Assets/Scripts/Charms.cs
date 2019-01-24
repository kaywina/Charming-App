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
	public GameObject fortune;

	private float swapSpeed = 0.5f;

    private bool onFirstLoad = true;

	// Use this for initialization
	void Start () {
        //PlayerPrefs.SetString("Charm", "");

        if (String.IsNullOrEmpty(PlayerPrefs.GetString("Charm"))) {
            PlayerPrefs.SetString("Charm", "Love"); // default is Love
        }
		SetCharm (PlayerPrefs.GetString ("Charm"));
        onFirstLoad = false;
	}
	
	// Update is called once per frame
	void Update () {
		GetInput ();
	}
	
	void GetInput() {
		// Mouse Input
		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown (0))
        {
			GetTapStarted ();
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
		
		if (Input.GetKeyDown ("escape"))
        {
			Application.Quit ();
		}
		
	}
	
	// Start animations on button tap/click
	void GetTapStarted() {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == "Love")
            {
                SetCharm("Love");
                //Debug.Log("Love!");
            }
            if (hit.transform.name == "Grace")
            {
                SetCharm("Grace");
                //Debug.Log("Grace!");
            }
            if (hit.transform.name == "Patience")
            {
                SetCharm("Patience");
                //Debug.Log("Patience!");
            }
            if (hit.transform.name == "Wisdom")
            {
                SetCharm("Wisdom");
                //Debug.Log("Wisdom!");
            }
            if (hit.transform.name == "Joy")
            {
                SetCharm("Joy");
                //Debug.Log("Joy!");
            }
            if (hit.transform.name == "Focus")
            {
                SetCharm("Focus");
                //Debug.Log("Focus!");
            }
            if (hit.transform.name == "Will")
            {
                SetCharm("Will");
                //Debug.Log("Will!");
            }
            if (hit.transform.name == "Guile")
            {
                SetCharm("Guile");
                //Debug.Log("Guile!");
            }
            if (hit.transform.name == "Power")
            {
                SetCharm("Power");
                //Debug.Log("Power!");
            }
            if (hit.transform.name == "Fortune")
            {
                SetCharm("Fortune");
                //Debug.Log("Fortune!");
            }
		}
	}

	// Start animations on button tap/click
	void GetTapEnded() {
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
            if (hit.transform.name == "Love") { }
            if (hit.transform.name == "Grace") { }
            if (hit.transform.name == "Patience") { }
            if (hit.transform.name == "Wisdom") { }
            if (hit.transform.name == "Joy") { }
            if (hit.transform.name == "Focus") { }
            if (hit.transform.name == "Will") { }
            if (hit.transform.name == "Guile") { }
            if (hit.transform.name == "Power") { }
            if (hit.transform.name == "Fortune") { }
        }
	}

	public void SetCharm(string charm) {

        if (!onFirstLoad && charm == PlayerPrefs.GetString("Charm"))
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

        switch (charm) {
		case "Love":
			mainCamera.transform.DOMoveX(love.transform.position.x, swapSpeed);
			charmNameText.text = "Love";
			charmDescriptionText.text = "Good luck friend";
			PlayerPrefs.SetString ("Charm", "Love");
			break;
        case "Grace":
            mainCamera.transform.DOMoveX(grace.transform.position.x, swapSpeed);
            charmNameText.text = "Grace";
            charmDescriptionText.text = "Flow like water";
            PlayerPrefs.SetString("Charm", "Grace");
            break;
        case "Patience":
            mainCamera.transform.DOMoveX(patience.transform.position.x, swapSpeed);
            charmNameText.text = "Patience";
            charmDescriptionText.text = "Wait for change";
            PlayerPrefs.SetString("Charm", "Patience");
            break;
        case "Wisdom":
            mainCamera.transform.DOMoveX(wisdom.transform.position.x, swapSpeed);
            charmNameText.text = "Wisdom";
            charmDescriptionText.text = "Know what's right";
            PlayerPrefs.SetString("Charm", "Wisdom");
            break;
        case "Joy":
            mainCamera.transform.DOMoveX(joy.transform.position.x, swapSpeed);
            charmNameText.text = "Joy";
            charmDescriptionText.text = "Witness the good";
            PlayerPrefs.SetString("Charm", "Joy");
            break;
        case "Focus":
            mainCamera.transform.DOMoveX(focus.transform.position.x, swapSpeed);
            charmNameText.text = "Focus";
            charmDescriptionText.text = "Stay on task";
            PlayerPrefs.SetString("Charm", "Focus");
            break;
        case "Will":
            mainCamera.transform.DOMoveX(will.transform.position.x, swapSpeed);
            charmNameText.text = "Will";
            charmDescriptionText.text = "Endure hard times";
            PlayerPrefs.SetString("Charm", "Will");
            break;
        case "Guile":
            mainCamera.transform.DOMoveX(guile.transform.position.x, swapSpeed);
            charmNameText.text = "Guile";
            charmDescriptionText.text = "Act the part";
            PlayerPrefs.SetString("Charm", "Guile");
            break;
        case "Power":
		    mainCamera.transform.DOMoveX(power.transform.position.x, swapSpeed);
		    charmNameText.text = "Power";
		    charmDescriptionText.text = "Grow your might";
		    PlayerPrefs.SetString ("Charm", "Power");
		    break;
		case "Fortune":
			mainCamera.transform.DOMoveX(fortune.transform.position.x, swapSpeed);
			charmNameText.text = "Fortune";
			charmDescriptionText.text = "Let it rain";
			PlayerPrefs.SetString ("Charm", "Fortune");
			break;
		default:
			mainCamera.transform.DOMoveX(love.transform.position.x, swapSpeed);
			charmNameText.text = "Love";
			charmDescriptionText.text = "Make sweet love";
			PlayerPrefs.SetString ("Charm", "Love");
			break;
		}
	}

    public void Quit()
    {
        //Debug.Log("Quit");
        Application.Quit();
    }
}
