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
#if UNITY_ANDROID
    private const string CHANNEL_ID = "CharmingAppNotifications";
#endif

    public SetPlayerPrefFromToggle togglePrefab;

    private void Start()
    {

        DisableNotifications(); // always start from scratch

        if (PlayerPrefs.GetString(togglePrefab.GetPlayerPrefName()) == "false" )
        {
            Debug.Log("Notifications are disabled");
            return;
        }
        else
        {
            if (String.IsNullOrEmpty(PlayerPrefs.GetString(togglePrefab.GetPlayerPrefName())))
            {
                Debug.Log("Notifications option not yet set; defaulting to enabled");
                PlayerPrefs.SetString(togglePrefab.GetPlayerPrefName(), "true");
            }
        }

        // only show notifications if that setting has been enabled in options

        EnableNotifcations();
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

        Debug.Log("Assign title and text");
        notification.Title = "Title of notification that should repeat daily!";
        notification.Text = "Text of the notification that should show up every day at 11am.";


        Debug.Log("Set fire time and repeat interval");
        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);
        int hourToSend = 11;
        DateTime fireTime = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, hourToSend, 0, 0); // schedule for 11am
        notification.FireTime = fireTime;
        notification.RepeatInterval = new TimeSpan(1, 0, 0, 0); // repeat daily

        Debug.Log("Send Android notification");
        AndroidNotificationCenter.SendNotification(notification, CHANNEL_ID);
        Debug.Log("Android notification has been sent");
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
            Subtitle = "This is a subtitle, show at 11am please thx...",
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread1",
            Trigger = calendarTrigger,
        };

        iOSNotificationCenter.ScheduleNotification(notification);
    }
#endif

    public void DisableNotifications()
    {
        Debug.Log("Cancel notifications");
#if UNITY_ANDROID
        AndroidNotificationCenter.CancelAllNotifications();
#elif UNITY_IOS
        iOSNotificationCenter.RemoveAllScheduledNotifications();
#endif
    }

    public void EnableNotifcations()
    {
#if UNITY_ANDROID
        CreateAndroidChannel();
        ScheduleRepeatDailyNotificationAndroid();
#elif UNITY_IOS
        // we are registering for notifications on app start (see mobile notifications project settings)
        ScheduleRepeatDailyNotificationsIos();
#else
        Debug.Log("Notifications not implemented for this platform");
#endif
    }

    public void ToggleNotifications()
    {
        if (PlayerPrefs.GetString(togglePrefab.GetPlayerPrefName()) == "false")
        {
            DisableNotifications();
            Debug.Log("Notifications have been disabled");
        }
        else
        {
            EnableNotifcations();
            Debug.Log("Notifications have been enabled");
        }
                
    }
}
