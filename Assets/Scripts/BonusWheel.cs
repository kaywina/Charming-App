using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BonusWheel : MonoBehaviour
{
    public BonusPanel bonusPanel;
    public Transform wheelModel;
    private bool randomStartingPosition = true;
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

        if (randomStartingPosition)
        {
            RandomizeStartRotation();
        }
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

    private void RandomizeStartRotation()
    {
        int randomAngle = Random.Range(0, 360);
        wheelModel.eulerAngles = new Vector3(0, 0, randomAngle);
        //Debug.Log("Randomize start rotation to " + randomAngle);
    }

    public void Spin()
    {
        if (spinning) { return; }

        float minRandomTimeMultiplier = 1f;
        float maxRandomTimeMultiplier = 3f;
        randomTimeMultiplier = Random.Range(minRandomTimeMultiplier, maxRandomTimeMultiplier);
        itemNumber = Random.Range(0, prize.Count);
        float maxAngle = 360 * maxRandomTimeMultiplier + (itemNumber * anglePerItem);

        float baseTime = 10f;
        float adjustedTime = baseTime * randomTimeMultiplier;


        if (spinTimeText != null)
        {
            spinTimeText.text = adjustedTime.ToString();
        }
        StartCoroutine(SpinTheWheel(adjustedTime, maxAngle));
    }

    IEnumerator SpinTheWheel(float time, float maxAngle)
    {
        spinning = true;

        float timer = 0.0f;
        float startAngle = wheelModel.eulerAngles.z;
        maxAngle = maxAngle - startAngle;

        int animationCurveNumber = Random.Range(0, animationCurves.Count);
        //Debug.Log("Animation Curve No. : " + animationCurveNumber);

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

        //Debug.Log("Prize: " + prize[itemNumber]);//use prize[itemNumnber] as per requirement
        if (prizeText != null)
        {
            prizeText.text = prize[itemNumber].ToString();
        }

        bonusPanel.CompleteSpin(prize[itemNumber]);
        
    }
}
