using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusPanel : CharmsPanel
{
    public GameObject header;
    public BonusWheel bonusWheel;
    public GameObject tip;
    public GameObject[] activateAfterSpin;
    public Text prizeText;
    public GameObject skipButton;
    public GameObject doubleBonusText;

    private bool hasSpun;
    private int storedBonus;

    new void OnEnable()
    {
        header.SetActive(false);
        doubleBonusText.SetActive(false);
        hasSpun = false;
        tip.SetActive(true);

        for (int i = 0; i < activateAfterSpin.Length; i++)
        {
            activateAfterSpin[i].SetActive(false);
        }
        skipButton.SetActive(true);

        base.OnEnable();
        
    }

    new void OnDisable()
    {
        header.SetActive(true);
        CurrencyManager.Instance.GiveBonus(storedBonus);
        base.OnDisable();
    }

    public void Spin()
    {
        if (hasSpun) { return; }

        //Debug.Log("Spin bonus wheel!");
        tip.SetActive(false);
        bonusWheel.Spin();
        skipButton.SetActive(false);
    }

    public void CompleteSpin(int bonus)
    {
        storedBonus = bonus;
        //Debug.Log("Complete bonus wheel spin");
        prizeText.text = bonus.ToString();
        for (int i = 0; i < activateAfterSpin.Length; i++)
        {
            activateAfterSpin[i].SetActive(true);
        }
        
        hasSpun = true;
    }

    public void DoubleBonus()
    {
        doubleBonusText.SetActive(true);
        storedBonus = storedBonus * 2;
    }
}
