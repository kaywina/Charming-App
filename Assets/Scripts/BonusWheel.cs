using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BonusWheel : MonoBehaviour
{
    public BonusPanel bonusPanel;
    public Transform wheelModel;
    public Text prizeText;
    public Text spinTimeText;

    public List<int> prize;
    public List<AnimationCurve> animationCurves;

    private bool spinning;
    private float anglePerItem;
    private float randomTimeMultiplier;
    private int itemNumber;

    void OnEnable()
    {
        spinning = false;
        anglePerItem = 360 / prize.Count;
        wheelModel.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spin();
        }
#endif
    }

    public void Spin()
    {
        if (spinning) { return; }
        itemNumber = Random.Range(0, prize.Count);
        float maxAngle = 720 + (itemNumber * anglePerItem);
        float baseTime = 17f;
        StartCoroutine(SpinTheWheel(baseTime, maxAngle));
    }

    IEnumerator SpinTheWheel(float time, float maxAngle)
    {
        spinning = true;

        float timer = 0.0f;
        float startAngle = wheelModel.eulerAngles.z;
        maxAngle = maxAngle - startAngle;

        int animationCurveNumber = Random.Range(0, animationCurves.Count);

        while (timer < time)
        {
            //to calculate rotation
            float angle = maxAngle * animationCurves[animationCurveNumber].Evaluate(timer / time);
            wheelModel.eulerAngles = new Vector3(0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime;
            yield return 0;
        }

        wheelModel.eulerAngles = new Vector3(0.0f, 0.0f, maxAngle + startAngle);
        spinning = false;

        if (prizeText != null)
        {
            prizeText.text = prize[itemNumber].ToString();
        }

        // prize is premium currency for certain bonus wheel segments
        if (itemNumber > 3 && itemNumber < 8)
        {
            bonusPanel.CompleteSpin(prize[itemNumber], true); // premium currency prize
        }
        else
        {
            bonusPanel.CompleteSpin(prize[itemNumber], false); // regular currency prize
        }        
    }
}
