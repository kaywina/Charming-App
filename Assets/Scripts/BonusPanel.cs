using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusPanel : CharmsPanel
{
    public BonusWheel bonusWheel;
    public GameObject tip;
    public GameObject[] activateAfterSpin;
    public Text prizeText;

    private bool hasSpun;
    private int storedBonus;

    new void OnEnable()
    {
        hasSpun = false;
        tip.SetActive(true);

        for (int i = 0; i < activateAfterSpin.Length; i++)
        {
            activateAfterSpin[i].SetActive(false);
        }

        base.OnEnable();
    }

    new void OnDisable()
    {
        CurrencyManager.Instance.GiveBonus(storedBonus);
        base.OnDisable();
    }

    public void Spin()
    {
        if (hasSpun) { return; }

        //Debug.Log("Spin bonus wheel!");
        tip.SetActive(false);
        bonusWheel.Spin();
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
        storedBonus = storedBonus * 2;
    }
}
