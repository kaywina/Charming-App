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

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetString("Charm", "");
        if (String.IsNullOrEmpty(PlayerPrefs.GetString("Charm"))) {
            PlayerPrefs.SetString("Charm", "Love"); // default is Love
        }
		SetCharm (PlayerPrefs.GetString ("Charm"));
	}
	
	// Update is called once per frame
	void Update () {
		GetInput ();
	}
	
	void GetInput() {
		// Mouse Input
		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown (0)) {
			GetTapStarted ();
		}
		if (Input.GetMouseButtonUp (0)) {
			GetTapEnded ();
		}
		#endif
		// Touch Input
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
			GetTapStarted();
		}
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
			GetTapEnded();
		}
		
		if (Input.GetKeyDown ("escape")) {
			Application.Quit ();
		}
		
	}
	
	// Start animations on button tap/click
	void GetTapStarted() {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit)) {
            if (hit.transform.name == "Charm_Love") { }
            if (hit.transform.name == "Charm_Grace") { }
            if (hit.transform.name == "Charm_Patience") { }
            if (hit.transform.name == "Charm_Wisdom") { }
            if (hit.transform.name == "Charm_Joy") { }
            if (hit.transform.name == "Charm_Focus") { }
            if (hit.transform.name == "Charm_Will") { }
            if (hit.transform.name == "Charm_Guile") { }
            if (hit.transform.name == "Charm_Power") { }
			if (hit.transform.name == "Charm_Fortune") { }
		}
	}

	// Start animations on button tap/click
	void GetTapEnded() {
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform.name == "Charm_Love") {
				SetCharm ("Love");
				Debug.Log ("Love!");
			}
            if (hit.transform.name == "Charm_Grace") {
                SetCharm("Grace");
                Debug.Log("Grace!");
            }
            if (hit.transform.name == "Charm_Patience") {
                SetCharm("Patience");
                Debug.Log("Patience!");
            }
            if (hit.transform.name == "Charm_Wisdom") {
                SetCharm("Wisdom");
                Debug.Log("Wisdom!");
            }
            if (hit.transform.name == "Charm_Joy") {
                SetCharm("Joy");
                Debug.Log("Joy!");
            }
            if (hit.transform.name == "Charm_Focus") {
                SetCharm("Focus");
                Debug.Log("Focus!");
            }
            if (hit.transform.name == "Charm_Will") {
                SetCharm("Will");
                Debug.Log("Will!");
            }
            if (hit.transform.name == "Charm_Guile") {
                SetCharm("Guile");
                Debug.Log("Guile!");
            }
            if (hit.transform.name == "Charm_Power") {
				SetCharm ("Power");
				Debug.Log ("Power!");
			}
			if (hit.transform.name == "Charm_Fortune") {
				SetCharm ("Fortune");
				Debug.Log ("Fortune!");
			}
		}
	}

	void SetCharm(string charm) {
		switch (charm) {
		case "Love":
			mainCamera.transform.DOMoveX(love.transform.position.x, swapSpeed);
			charmNameText.text = "Love";
			charmDescriptionText.text = "Make sweet love";
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
        Debug.Log("Quit");
        Application.Quit();
    }
}
