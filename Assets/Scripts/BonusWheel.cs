using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BonusWheel : MonoBehaviour
{
    public bool randomStartingPosition;
    public Text prizeText;
    public List<int> prize;
    public List<AnimationCurve> animationCurves;

    private bool spinning;
    private float anglePerItem;
    private int randomTime;
    private int itemNumber;

    void Start()
    {
        spinning = false;
        anglePerItem = 360 / prize.Count;

        if (randomStartingPosition)
        {
            transform.eulerAngles = new Vector3(0,0,Random.Range(0, 360));
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

    public void Spin()
    {
        if (spinning) { return; }

        int minRan = 1;
        int maxRan = 4;
        randomTime = Random.Range(minRan, maxRan);
        itemNumber = Random.Range(0, prize.Count);
        float maxAngle = 360 * randomTime + (itemNumber * anglePerItem);

        int baseTime = 7;
        StartCoroutine(SpinTheWheel(baseTime * randomTime, maxAngle));
    }

    IEnumerator SpinTheWheel(float time, float maxAngle)
    {
        spinning = true;

        float timer = 0.0f;
        float startAngle = transform.eulerAngles.z;
        maxAngle = maxAngle - startAngle;

        int animationCurveNumber = Random.Range(0, animationCurves.Count);
        Debug.Log("Animation Curve No. : " + animationCurveNumber);

        while (timer < time)
        {
            //to calculate rotation
            float angle = maxAngle * animationCurves[animationCurveNumber].Evaluate(timer / time);
            transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime;
            yield return 0;
        }

        transform.eulerAngles = new Vector3(0.0f, 0.0f, maxAngle + startAngle);
        spinning = false;

        Debug.Log("Prize: " + prize[itemNumber]);//use prize[itemNumnber] as per requirement
        prizeText.text = prize[itemNumber].ToString();
    }
}
