using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextFromGameCost : MonoBehaviour
{
    public Text costText;
    public GameObject imageObject;

    void OnEnable()
    {
        int cost = PlayManager.GetGameCost();
        costText.text = PlayManager.GetGameCost().ToString();
        if (cost == 0)
        {
            imageObject.SetActive(false);
            costText.gameObject.SetActive(false);
        }  
    }
}
