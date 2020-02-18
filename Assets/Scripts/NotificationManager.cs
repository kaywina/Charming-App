using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications;
using Unity.Notifications.Android;

public class NotificationManager : MonoBehaviour
{
    private const string CHANNEL_ID = "CharmingAppNotifications";

    private void Start()
    {
        CreateAndroidChannel();
        SendAndroidNotification();
    }

    private void CreateAndroidChannel()
    {
        Debug.Log("Create Android Channel");
        var channel = new AndroidNotificationChannel()
        {
            Id = CHANNEL_ID,
            Name = "Charming App Notifications Channel",
            Importance = Importance.High,
            Description = "Mindfullness notification from Charming App",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    private void SendAndroidNotification()
    {
        Debug.Log("Send Android notification in 10 seconds");
        var notification = new AndroidNotification();
        notification.Title = "Title of notification!";
        notification.Text = "Test of the notification.";
        notification.FireTime = System.DateTime.Now.AddSeconds(10);

        AndroidNotificationCenter.SendNotification(notification, CHANNEL_ID);
    }
}
