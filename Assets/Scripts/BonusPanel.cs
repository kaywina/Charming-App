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

    public void Spin()
    {
        if (hasSpun) { return; }

        //Debug.Log("Spin bonus wheel!");
        tip.SetActive(false);
        bonusWheel.Spin();
    }

    public void CompleteSpin(int bonus)
    {
        //Debug.Log("Complete bonus wheel spin");
        prizeText.text = bonus.ToString();
        for (int i = 0; i < activateAfterSpin.Length; i++)
        {
            activateAfterSpin[i].SetActive(true);
        }
        CurrencyManager.Instance.GiveBonus(bonus);
        hasSpun = true;
    }
}
