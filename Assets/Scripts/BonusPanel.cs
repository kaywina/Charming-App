using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusPanel : CharmsPanel
{
    public BonusWheel bonusWheel;

    public GameObject tip;
    public GameObject prize;
    public GameObject yesButton;

    public Text prizeText;

    private bool hasSpun;

    new void OnEnable()
    {
        hasSpun = false;
        tip.SetActive(true);
        prize.SetActive(false);
        yesButton.SetActive(false);

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
        prize.SetActive(true);
        CurrencyManager.Instance.GiveBonus(bonus);
        yesButton.SetActive(true);
        hasSpun = true;
    }
}
