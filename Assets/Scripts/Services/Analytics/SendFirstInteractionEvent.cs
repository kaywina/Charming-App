using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendFirstInteractionEvent : MonoBehaviour
{
    private string playerPrefName = "FirstInteractionDone";

    public void SendEvent()
    {
        bool hasPref = PlayerPrefs.HasKey(playerPrefName);
        if (!hasPref)
        {
            UnityAnalyticsController.SendFirstInteractionEvent();
            PlayerPrefs.SetString(playerPrefName, "true");
            //Debug.Log("Sent first interaction analytics event");
        }
    }
}
