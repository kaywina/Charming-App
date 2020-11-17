using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSubscriptionButton : MonoBehaviour
{
    public void ManageSubscription ()
    {
        UnityIAPController.OpenManageSubscriptionByPlatform();
    }
}
