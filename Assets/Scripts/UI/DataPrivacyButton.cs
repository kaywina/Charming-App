using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPrivacyButton : MonoBehaviour
{
    public void ClickButton()
    {
        UnityAnalyticsController.TryOpenDataPrivacyURL();
    }
}
