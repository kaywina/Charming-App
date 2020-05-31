using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendSubscribeVisitEventOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        UnityAnalyticsController.SendVisitSubscribeScreenEvent();
    }
}
