using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextFromVersion : MonoBehaviour
{
    public Text versionNumberText;

    void OnEnable()
    {
        versionNumberText.text = Application.version;
    }
}
