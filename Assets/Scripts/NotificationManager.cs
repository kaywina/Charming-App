using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#elif UNITY_IOS
using Unity.Notifications.iOS;
#endif

public class NotificationManager : MonoBehaviour
{
    private const string CHANNEL_ID = "CharmingAppNotifications";

    private void Start()
    {
#if UNITY_ANDROID
        CreateAndroidChannel();
        SendAndroidNotification();
#elif UNITY_IOS
        // we are registering for notifications on app start (see mobile notifications project settings)
        SendIosNotification();
#endif

    }

#if UNITY_ANDROID
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
#endif

#if UNITY_IOS
    private void SendIosNotification()
    {
        int hours = 0;
        int minutes = 0;
        int seconds = 10;

        var timeTrigger = new iOSNotificationTimeIntervalTrigger()
        {
            TimeInterval = new TimeSpan(hours, minutes, seconds),
            Repeats = false
        };

        var notification = new iOSNotification()
        {
            // You can optionally specify a custom identifier which can later be 
            // used to cancel the notification, if you don't set one, a unique 
            // string will be generated automatically.
            Identifier = "_notification_01",
            Title = "Title",
            Body = "Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 10 seconds",
            Subtitle = "This is a subtitle, something, something important...",
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread1",
            Trigger = timeTrigger,
        };

        iOSNotificationCenter.ScheduleNotification(notification);
    }
#endif
}
