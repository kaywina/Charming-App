using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubscribePanel : CharmsPanel
{

    private bool fromOptions = false;
    private bool fromVisualOptions = false;
    private bool fromAudioOptions = false;
    private bool fromLove = false;
    private bool fromMeditate = false;
    private bool fromSecrets = false;
    private bool fromTutorial = false;

    public GameObject charmButtons;
    public GameObject meditationWoldSpace;
    public ToggleGameObject meditateOptionsToggle;

    public OptionsSubPanel visualOptionsPanel;
    public OptionsSubPanel audioOptionsPanel;
    public OptionsPanel optionsPanel;
    public CharmsPanel lovePanel;
    public MeditatePanel meditatePanel;
    public SecretsPanel secretsPanel;
    public TutorialPanel tutorialPanel;

    public GameObject promo;
    public GameObject success;

    private bool optionsRTM;
    private bool loveRTM;
    private bool secretsRTM;
    private bool tutorialRTM;

    public void SetFromVisualOptionsFlag(bool flag)
    {
        fromVisualOptions = flag;
    }

    public void SetFromAudioOptionsFlag(bool flag)
    {
        fromAudioOptions = flag;
    }

    public void SetFromOptionsFlag(bool flag)
    {
        fromOptions = flag;
    }

    public void SetFromLoveFlag(bool flag)
    {
        fromLove = flag;
    }

    public void SetFromMeditateFlag(bool flag)
    {
        fromMeditate = flag;
    }

    public void SetFromSecretsFlag(bool flag)
    {
        fromSecrets = flag;
    }

    public void SetFromTutorialFlag(bool flag)
    {
        fromTutorial = flag;
    }

    new void OnEnable()
    {
        base.OnEnable();

        promo.SetActive(true);
        success.SetActive(false);

        returnToMain = false;
        if (fromOptions) {
            optionsRTM = optionsPanel.returnToMain;
            optionsPanel.SetReturnToMain(false);
            optionsPanel.gameObject.SetActive(false);
        }
        else if (fromVisualOptions)
        {
            visualOptionsPanel.SetWentToSubscribe(true);
            visualOptionsPanel.gameObject.SetActive(false);
        }
        else if (fromAudioOptions)
        {
            audioOptionsPanel.SetWentToSubscribe(true);
            audioOptionsPanel.gameObject.SetActive(false);
        }
        else if (fromLove) {
            loveRTM = lovePanel.returnToMain;
            lovePanel.SetReturnToMain(false);
            lovePanel.gameObject.SetActive(false);
        }
        else if (fromMeditate)
        {
            meditationWoldSpace.SetActive(false);
            meditatePanel.gameObject.SetActive(false);
        }
        else if (fromSecrets)
        {
            secretsRTM = secretsPanel.returnToMain;
            secretsPanel.SetReturnToMain(false);
            secretsPanel.gameObject.SetActive(false);
        }
        else if (fromTutorial)
        {
            tutorialRTM = tutorialPanel.returnToMain;
            tutorialPanel.SetReturnToMain(false);
            tutorialPanel.gameObject.SetActive(false);
        }

        else
        {
            returnToMain = true;
        }
    }

    new void OnDisable()
    {
        if (fromOptions) {
            optionsPanel.SetReturnToMain(optionsRTM);
            optionsPanel.gameObject.SetActive(true);
        }
        else if (fromVisualOptions)
        {
            visualOptionsPanel.gameObject.SetActive(true);
        }
        else if (fromAudioOptions)
        {
            audioOptionsPanel.gameObject.SetActive(true);
        }
        else if (fromLove)
        {
            lovePanel.SetReturnToMain(loveRTM);
            lovePanel.gameObject.SetActive(true);
        }
        else if (fromMeditate)
        {
            meditationWoldSpace.SetActive(true);
            meditatePanel.gameObject.SetActive(true);
            meditateOptionsToggle.Toggle();
        }
        else if (fromSecrets)
        {
            secretsPanel.SetReturnToMain(secretsRTM);
            secretsPanel.gameObject.SetActive(true);
        }
        else if (fromTutorial)
        {
            tutorialPanel.SetReturnToMain(tutorialRTM);
            tutorialPanel.gameObject.SetActive(true);
            tutorialPanel.OpenTutorialPageAtIndex(4);

        }
        else
        {
            // for case when returning to main ui after non-subscriber is redirected to subscribe panel from exit of meditate panel
            if (charmButtons != null) { charmButtons.SetActive(true); }
        }

        // reset flags
        fromOptions = false;
        fromLove = false;
        fromVisualOptions = false;
        fromAudioOptions = false;
        fromMeditate = false;
        fromSecrets = false;
        fromTutorial = false;

        base.OnDisable();
    }
}
