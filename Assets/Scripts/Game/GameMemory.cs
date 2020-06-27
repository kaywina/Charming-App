using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMemory : MonoBehaviour
{
    private int level = 1;
    public PlayManager playManager;

    public GameObject[] buttonsLevel1;
    public GameObject[] buttonsLevel2;
    public GameObject[] buttonsLevel3;

    private void OnEnable()
    {
        for (int i = 0; i < buttonsLevel1.Length; i++)
        {
            buttonsLevel1[i].GetComponent<Image>().sprite = playManager.GetSpriteByIndex(0);
        }
    }
}
