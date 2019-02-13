using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusButton : MonoBehaviour
{

    public GameObject bonusPanel;
    public GameObject optionsPanel;

    public void ShowBonusPanel()
    {
        bonusPanel.SetActive(true);
        OptionsPanel.SetReturnToMain(false);
        optionsPanel.SetActive(false);
    }
}
