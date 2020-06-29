using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextFromGameCost : MonoBehaviour
{
    public Text costText;

    void OnEnable()
    {
        costText.text = PlayManager.GetGameCost().ToString();
    }
}
