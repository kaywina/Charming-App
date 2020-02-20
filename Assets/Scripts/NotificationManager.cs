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
        AndroidNotificationCenter.CancelAllScheduledNotifications();
        CreateAndroidChannel();
        ScheduleRepeatDailyNotificationAndroid();
#elif UNITY_IOS
        // we are registering for notifications on app start (see mobile notifications project settings)
        iOSNotificationCenter.RemoveAllScheduledNotifications();
        ScheduleRepeatDailyNotificationsIos();
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
            Description = "Mindfulness notification from Charming App",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    private void ScheduleRepeatDailyNotificationAndroid()
    {
        Debug.Log("Schedule repeat daily Android notification for 11:00am");
        var notification = new AndroidNotification();
        notification.Title = "Title of notification!";
        notification.Text = "Text of the notification.";

        DateTime today = DateTime.Today;
        DateTime fireTime = new DateTime(today.Year, today.Month, today.Day, 11, 0, 0); // schedule for noon

        notification.FireTime = fireTime;
        notification.RepeatInterval = new TimeSpan(1, 0, 0, 0); // repeat daily

        AndroidNotificationCenter.SendNotification(notification, CHANNEL_ID);
    }
#endif

#if UNITY_IOS
    private void ScheduleRepeatDailyNotificationsIos()
    {
        Debug.Log("Schedule repeat daily iOS notification for 11:00am");

        var calendarTrigger = new iOSNotificationCalendarTrigger()
        {
            Hour = 11,
            Repeats = true
        };

        var notification = new iOSNotification()
        {
            // You can optionally specify a custom identifier which can later be 
            // used to cancel the notification, if you don't set one, a unique 
            // string will be generated automatically.
            Identifier = "notification_01",
            Title = "Title",
            Body = "Notification body - scheduled to repeat at hour 11:00am",
            Subtitle = "This is a subtitle, something, something important...",
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread1",
            Trigger = calendarTrigger,
        };

        iOSNotificationCenter.ScheduleNotification(notification);
    }
#endif
}
