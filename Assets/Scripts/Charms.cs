using UnityEngine;
using System.Collections;
using DG.Tweening;


public class Charms : MonoBehaviour {

	public Camera mainCamera;

	public TextMesh charmNameText;
	public TextMesh charmDescriptionText;

	// For Tap Interface
	private Ray ray;
	private RaycastHit hit;

	// For swappable icons on stand
	public GameObject luck;
	public GameObject love;
	public GameObject charisma;
	public GameObject power;
	public GameObject intellect;
	public GameObject grace;
	public GameObject wisdom;
	public GameObject patience;
	public GameObject will;
	public GameObject guile;
	public GameObject joy;
	public GameObject fame;
	public GameObject wealth;

	private float swapSpeed = 0.5f;

	// Use this for initialization
	void Start () {
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
			if (hit.transform.name == "Charm_Luck") {
			}
			if (hit.transform.name == "Charm_Love") {
			}
			if (hit.transform.name == "Charm_Charisma") {
			}
			if (hit.transform.name == "Charm_Power") {
			}
			if (hit.transform.name == "Charm_Intellect") {
			}
			if (hit.transform.name == "Charm_Grace") {
			}
			if (hit.transform.name == "Charm_Wisdom") {
			}
			if (hit.transform.name == "Charm_Patience") {
			}
			if (hit.transform.name == "Charm_Will") {
			}
			if (hit.transform.name == "Charm_Guile") {
			}
			if (hit.transform.name == "Charm_Joy") {
			}
			if (hit.transform.name == "Charm_Fame") {
			}
			if (hit.transform.name == "Charm_Wealth") {
			}
		}
	}

	// Start animations on button tap/click
	void GetTapEnded() {
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform.name == "Charm_Luck") {
				SetCharm ("Luck");
				Debug.Log ("Luck!");
			}
			if (hit.transform.name == "Charm_Love") {
				SetCharm ("Love");
				Debug.Log ("Love!");
			}
			if (hit.transform.name == "Charm_Charisma") {
				SetCharm ("Charisma");
				Debug.Log ("Charisma!");
			}
			if (hit.transform.name == "Charm_Power") {
				SetCharm ("Power");
				Debug.Log ("Power!");
			}
			if (hit.transform.name == "Charm_Intellect") {
				SetCharm ("Intellect");
				Debug.Log ("Intellect!");
			}
			if (hit.transform.name == "Charm_Grace") {
				SetCharm ("Grace");
				Debug.Log ("Grace!");
			}
			if (hit.transform.name == "Charm_Wisdom") {
				SetCharm ("Wisdom");
				Debug.Log ("Wisdom!");
			}
			if (hit.transform.name == "Charm_Patience") {
				SetCharm ("Patience");
				Debug.Log ("Patience!");
			}
			if (hit.transform.name == "Charm_Will") {
				SetCharm ("Will");
				Debug.Log ("Will!");
			}
			if (hit.transform.name == "Charm_Guile") {
				SetCharm ("Guile");
				Debug.Log ("Guile!");
			}
			if (hit.transform.name == "Charm_Joy") {
				SetCharm ("Joy");
				Debug.Log ("Joy!");
			}
			if (hit.transform.name == "Charm_Fame") {
				SetCharm ("Fame");
				Debug.Log ("Fame!");
			}
			if (hit.transform.name == "Charm_Wealth") {
				SetCharm ("Wealth");
				Debug.Log ("Wealth!");
			}
		}
	}

	void SetCharm(string charm) {
		switch (charm) {
		case "Luck":
			mainCamera.transform.DOMoveX(luck.transform.position.x, swapSpeed);
			charmNameText.text = "Luck";
			charmDescriptionText.text = "Your luck improves remarkably";
			PlayerPrefs.SetString ("Charm", "Luck");
			break;
		case "Love":
			mainCamera.transform.DOMoveX(love.transform.position.x, swapSpeed);
			charmNameText.text = "Love";
			charmDescriptionText.text = "Get ready for some sweet love";
			PlayerPrefs.SetString ("Charm", "Love");
			break;
		case "Charisma":
			mainCamera.transform.DOMoveX(charisma.transform.position.x, swapSpeed);
			charmNameText.text = "Charisma";
			charmDescriptionText.text = "People see the best in you";
			PlayerPrefs.SetString ("Charm", "Charisma");
			break;
		case "Power":
			mainCamera.transform.DOMoveX(power.transform.position.x, swapSpeed);
			charmNameText.text = "Power";
			charmDescriptionText.text = "Your might grows stronger";
			PlayerPrefs.SetString ("Charm", "Power");
			break;
		case "Intellect":
			mainCamera.transform.DOMoveX(intellect.transform.position.x, swapSpeed);
			charmNameText.text = "Intellect";
			charmDescriptionText.text = "You know you are correct";
			PlayerPrefs.SetString ("Charm", "Intellect");
			break;
		case "Grace":
			mainCamera.transform.DOMoveX(grace.transform.position.x, swapSpeed);
			charmNameText.text = "Grace";
			charmDescriptionText.text = "Your actions flow like water";
			PlayerPrefs.SetString ("Charm", "Grace");
			break;
		case "Wisdom":
			mainCamera.transform.DOMoveX(wisdom.transform.position.x, swapSpeed);
			charmNameText.text = "Wisdom";
			charmDescriptionText.text = "You make good decisions";
			PlayerPrefs.SetString ("Charm", "Wisdom");
			break;
		case "Patience":
			mainCamera.transform.DOMoveX(patience.transform.position.x, swapSpeed);
			charmNameText.text = "Patience";
			charmDescriptionText.text = "Choose to wait for change";
			PlayerPrefs.SetString ("Charm", "Patience");
			break;
		case "Will":
			mainCamera.transform.DOMoveX(will.transform.position.x, swapSpeed);
			charmNameText.text = "Will";
			charmDescriptionText.text = "You do what needs to be done";
			PlayerPrefs.SetString ("Charm", "Will");
			break;
		case "Guile":
			mainCamera.transform.DOMoveX(guile.transform.position.x, swapSpeed);
			charmNameText.text = "Guile";
			charmDescriptionText.text = "Others accept your conclusions";
			PlayerPrefs.SetString ("Charm", "Guile");
			break;
		case "Joy":
			mainCamera.transform.DOMoveX(joy.transform.position.x, swapSpeed);
			charmNameText.text = "Joy";
			charmDescriptionText.text = "You see the good in all things";
			PlayerPrefs.SetString ("Charm", "Joy");
			break;
		case "Fame":
			mainCamera.transform.DOMoveX(fame.transform.position.x, swapSpeed);
			charmNameText.text = "Fame";
			charmDescriptionText.text = "Your celebrity knows no bounds";
			PlayerPrefs.SetString ("Charm", "Fame");
			break;
		case "Wealth":
			mainCamera.transform.DOMoveX(wealth.transform.position.x, swapSpeed);
			charmNameText.text = "Wealth";
			charmDescriptionText.text = "Fortune rains down upon you";
			PlayerPrefs.SetString ("Charm", "Wealth");
			break;
		default:
			mainCamera.transform.DOMoveX(luck.transform.position.x, swapSpeed);
			charmNameText.text = "Luck";
			charmDescriptionText.text = "Your luck improves remarkably";
			PlayerPrefs.SetString ("Charm", "Luck");
			break;
		}
	}
}
