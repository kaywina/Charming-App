using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRemember : MonoBehaviour
{

    public GameObject[] levelButtons;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableButtonsByIndex(int index)
    {
        Debug.Log("Enable buttons for index " + index);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i == index)
            {
                levelButtons[i].SetActive(true);
            }
            else
            {
                levelButtons[i].SetActive(false);
            }
        }
    }
}
