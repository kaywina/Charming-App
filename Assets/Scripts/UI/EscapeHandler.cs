using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This class handles the case when the escape key is pressed (native back button on Android)
 * */

public class EscapeHandler : MonoBehaviour
{
    public Button optionsBackButton;
    public Button meditateBackButton;
    public Button bonusSkipButton;
    public Button bonusOKButton;
    public Button storeCloseButton;
    public Button infoOKButton;
    public Button welcomeOKButton;
    public Button unlockNoButton;
    public Button congratsOKButton;
    public Button loveBackButton;
    public Button secretsBackButton;

    private Button[] allButtons;

    // Start is called before the first frame update
    void Start()
    {
        allButtons = new Button[] {
            optionsBackButton,
            meditateBackButton,
            bonusSkipButton,
            bonusOKButton,
            storeCloseButton,
            infoOKButton,
            welcomeOKButton,
            unlockNoButton,
            congratsOKButton,
            loveBackButton,
            secretsBackButton
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            for (int i = 0; i < allButtons.Length; i++)
            {
                if (allButtons[i].gameObject.activeInHierarchy == true)
                {
                    allButtons[i].onClick.Invoke();
                    return;
                }
            }
        }
    }
}
