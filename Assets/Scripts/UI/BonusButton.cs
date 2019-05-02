using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusButton : MonoBehaviour
{

    public GameObject bonusPanel;
    public GameObject optionsPanel;

    public void ShowBonusPanel()
    {
        optionsPanel.SetActive(false);
        bonusPanel.SetActive(true);
    }
}
