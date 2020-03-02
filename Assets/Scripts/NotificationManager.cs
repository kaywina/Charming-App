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


    public static string PLAYERPREF_NAME_HOUR = "NotificationHour"; // don't change in production
    public static string PLAYERPREF_NAME_MINUTE = "NotificationMinute"; // don't change in production

    private void Start()
    {

        CancelNotifications(); // always start from scratch

        if (PlayerPrefs.GetString(togglePrefab.GetPlayerPrefName()) == "false" )
        {
            Debug.Log("Notifications are disabled; cancel all notifications on start");
            CancelNotifications();
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
        ScheduleNotifications();
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
        Debug.Log("Schedule repeat daily Android mindfulness notification");
        var notification = new AndroidNotification();

        notification.Title = Localization.GetTranslationByKey("NOTIFICATION_TITLE");
        notification.Text = Localization.GetTranslationByKey("NOTIFICATION_TEXT");

        DateTime now = DateTime.Now;
        DateTime tomorrow = now.AddDays(1);

        int hourToSend = 11; // default time is 11:00am
        if (PlayerPrefs.HasKey(PLAYERPREF_NAME_HOUR))
        {
            hourToSend = PlayerPrefs.GetInt(PLAYERPREF_NAME_HOUR); // otherwise overright default hour with stored time from options
        }

        int minuteToSend = 0; // default time is 11:00am
        if (PlayerPrefs.HasKey(PLAYERPREF_NAME_MINUTE))
        {
            minuteToSend = PlayerPrefs.GetInt(PLAYERPREF_NAME_MINUTE); // otherwise overright default minute with stored time from options
        }

        DateTime fireTime;
        // if notification could still be sent today, schedule for today
        if (hourToSend > now.Hour || (hourToSend == now.Hour && minuteToSend > now.Minute))
        {
            fireTime = new DateTime(now.Year, now.Month, now.Day, hourToSend, minuteToSend, 0);
            Debug.Log("Android notification scheduled for " + fireTime.ToShortTimeString() + " today");
        }

        // if time has passed, schedule for tomorrow
        else
        {
            fireTime = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, hourToSend, minuteToSend, 0);
            Debug.Log("Android notification scheduled for " + fireTime.ToShortTimeString() + " tomorrow");
        }
        
        notification.FireTime = fireTime;
        notification.RepeatInterval = new TimeSpan(1, 0, 0, 0); // repeat daily

        AndroidNotificationCenter.SendNotification(notification, CHANNEL_ID);
        Debug.Log("Android notification scheduling completed");
        
    }
#endif

#if UNITY_IOS
    private void ScheduleRepeatDailyNotificationsIos()
    {
        Debug.Log("Schedule repeat daily iOS mindfulness notification for 11:00am");

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
        Debug.Log("iOS notification sent");
    }
#endif

    public void CancelNotifications()
    {
        Debug.Log("Cancel notifications");
#if UNITY_ANDROID
        AndroidNotificationCenter.CancelAllNotifications();
#elif UNITY_IOS
        iOSNotificationCenter.RemoveAllScheduledNotifications();
#endif
    }

    public void ScheduleNotifications()
    {
#if UNITY_ANDROID
        CreateAndroidChannel();
        ScheduleRepeatDailyNotificationAndroid();
#elif UNITY_IOS
        // we are registering for notifications on app start (see mobile notifications project settings)
        ScheduleRepeatDailyNotificationsIos(onStart);
#else
        Debug.Log("Notifications not implemented for this platform");
#endif
    }

    public void ToggleNotifications()
    {
        if (PlayerPrefs.GetString(togglePrefab.GetPlayerPrefName()) == "false")
        {
            CancelNotifications();
            Debug.Log("Notifications have been disabled");
        }
        else
        {
            ScheduleNotifications();
            Debug.Log("Notifications have been enabled");
        }        
    }

    public void RescheduleNotifications()
    {
        CancelNotifications();
        if (PlayerPrefs.GetString(togglePrefab.GetPlayerPrefName()) == "true")
        {
            ScheduleNotifications();
            Debug.Log("Reschedule notifications");
        }
        else
        {
            Debug.Log("Notifications have been disabled; do not reschedule");
        }
    }
}
