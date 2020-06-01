using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendFirstInteractionEvent : MonoBehaviour
{
    public void SendEvent()
    {
        UnityAnalyticsController.SendFirstInteractionEvent();
    }
}
