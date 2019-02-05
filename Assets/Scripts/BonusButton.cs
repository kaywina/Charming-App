using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusButton : MonoBehaviour
{

    public GameObject bonusPanel;

    public void OpenBonusWheelScene()
    {
        bonusPanel.SetActive(true);
    }
}
