using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusButton : MonoBehaviour
{
    public void OpenBonusWheelScene()
    {
        SceneManager.LoadScene("Bonus");
    }
}
