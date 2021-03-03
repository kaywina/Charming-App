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

    public static string PLAYERPREF_NAME_HOUR = "NotificationHour"; // don't change in production
    public static string PLAYERPREF_NAME_MINUTE = "NotificationMinute"; // don't change in production
    private string notificationsPlayerPref = "EnableNotifications"; // don't change in production

#if UNITY_ANDROID
    private const string CHANNEL_ID = "CharmingAppNotifications";
    private const int dailyAndroidNotificationID = 1;
    private const int missedAndroidNotificationID = 2;

#elif UNITY_IOS
    private const string iOSDailyNotificationID = "CharmingAppDailyNotification";
    private const string iOSMissedNotificationID = "CharmingAppMissedNotification";
    private const string iOSNotificationCategory = "CharmingAppNotification";
    private const string iOSThreadID = "thread1";
#endif

    private void Start()
    {
        CancelDailyNotifications(); // always start from scratch for daily notifications
        CancelMissedNotifications(); // same with "missed" notifications that are sent every three days of inactivity

        if (PlayerPrefs.GetString(notificationsPlayerPref) == "false" )
        {
            //Debug.Log("Notifications are disabled; cancel all notifications on start");
            return;
        }
        else
        {
            if (String.IsNullOrEmpty(PlayerPrefs.GetString(notificationsPlayerPref)))
            {
                //Debug.Log("Notifications option not yet set; defaulting to enabled");
                PlayerPrefs.SetString(notificationsPlayerPref, "true");
            }
        }

#if UNITY_ANDROID
        CreateAndroidChannel();
#endif

        // only show notifications if that setting has been enabled in options
        ScheduleDailyNotifications();
        ScheduleMissedNotifications();
    }

#if UNITY_ANDROID
    private void CreateAndroidChannel()
    {
        //Debug.Log("Create Android Channel");
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
        //Debug.Log("Schedule repeat daily Android mindfulness notification");
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
            //Debug.Log("Android notification scheduled for " + fireTime.ToShortTimeString() + " today");
        }

        // if time has passed, schedule for tomorrow
        else
        {
            fireTime = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, hourToSend, minuteToSend, 0);
            //Debug.Log("Android notification scheduled for " + fireTime.ToShortTimeString() + " tomorrow");
        }
        
        notification.FireTime = fireTime;
        notification.RepeatInterval = new TimeSpan(1, 0, 0, 0); // repeat daily

        AndroidNotificationCenter.SendNotificationWithExplicitID(notification, CHANNEL_ID, dailyAndroidNotificationID);
        //Debug.Log("Android notification scheduling completed");
        
    }
#endif

#if UNITY_IOS
    private void ScheduleRepeatDailyNotificationsIos()
    {
        //Debug.Log("Schedule repeat daily iOS mindfulness notification");

        // set notification time from data; or use default
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

        var calendarTrigger = new iOSNotificationCalendarTrigger()
        {
            Hour = hourToSend,
            Minute = minuteToSend,
            Repeats = true
        };

        //Debug.Log("iOS notification scheduled for hour " + calendarTrigger.Hour.ToString() + " and minute " + calendarTrigger.Minute.ToString());

        var notification = new iOSNotification()
        {
            // You can optionally specify a custom identifier which can later be 
            // used to cancel the notification, if you don't set one, a unique 
            // string will be generated automatically.
            Identifier = iOSDailyNotificationID,
            Title = Localization.GetTranslationByKey("DAILY_NOTIFICATION_TITLE"),
            Body = Localization.GetTranslationByKey("DAILY_NOTIFICATION_TEXT"),
            Subtitle = Localization.GetTranslationByKey("DAILY_NOTIFICATION_SUBTITLE"),
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = iOSNotificationCategory,
            ThreadIdentifier = iOSThreadID,
            Trigger = calendarTrigger,
        };

        iOSNotificationCenter.ScheduleNotification(notification);
        //Debug.Log("iOS notification sent");
    }

    private void ScheduleRepeatMissedNotificationsIos()
    {
        //Debug.Log("Schedule repeat missed iOS mindfulness notification");

        var calendarTrigger = new iOSNotificationCalendarTrigger()
        {
            Hour = (24 * 1) + 12, // 1 days in advance at noon
            Minute = 0,
            Repeats = true
        };


        var notification = new iOSNotification()
        {
            Identifier = iOSMissedNotificationID,
            Title = Localization.GetTranslationByKey("MISSED_NOTIFICATION_TITLE"),
            Body = Localization.GetTranslationByKey("MISSED_NOTIFICATION_TEXT"),
            Subtitle = Localization.GetTranslationByKey("MISSED_NOTIFICATION_SUBTITLE"),
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = iOSNotificationCategory,
            ThreadIdentifier = iOSThreadID,
            Trigger = calendarTrigger,
        };

        iOSNotificationCenter.ScheduleNotification(notification);
        //Debug.Log("iOS notification sent");
    }
#endif

    public void CancelDailyNotifications()
    {
        //Debug.Log("Cancel daily notifications");
#if UNITY_ANDROID
        AndroidNotificationCenter.CancelNotification(dailyAndroidNotificationID);
#elif UNITY_IOS
        iOSNotificationCenter.RemoveScheduledNotification(iOSDailyNotificationID);
#endif
    }

    public void CancelMissedNotifications()
    {
        //Debug.Log("Cancel missed notifications");
#if UNITY_ANDROID
        AndroidNotificationCenter.CancelNotification(missedAndroidNotificationID);
#elif UNITY_IOS
        iOSNotificationCenter.RemoveScheduledNotification(iOSMissedNotificationID);
#endif
    }

    public void ScheduleDailyNotifications()
    {
#if UNITY_ANDROID
        ScheduleRepeatDailyNotificationAndroid();
#elif UNITY_IOS
        // we are registering for notifications on app start (see mobile notifications project settings)
        ScheduleRepeatDailyNotificationsIos();
#else
        Debug.Log("Notifications not implemented for this platform");
#endif
    }

    public void ScheduleMissedNotifications()
    {
#if UNITY_ANDROID
        ScheduleRepeatMissedNotificationAndroid();
#elif UNITY_IOS
        // we are registering for notifications on app start (see mobile notifications project settings)
        ScheduleRepeatMissedNotificationsIos();
#else
        Debug.Log("Notifications not implemented for this platform");
#endif
    }

    public void ToggleNotifications()
    {
        if (PlayerPrefs.GetString(notificationsPlayerPref) == "false")
        {
            CancelDailyNotifications();
            //Debug.Log("Notifications have been disabled");
        }
        else
        {
            ScheduleDailyNotifications();
            //Debug.Log("Notifications have been enabled");
        }        
    }

    public void RescheduleDailyNotifications()
    {
        CancelDailyNotifications();
        if (PlayerPrefs.GetString(notificationsPlayerPref) == "true")
        {
            ScheduleDailyNotifications();
            //Debug.Log("Reschedule notifications");
        }
        else
        {
            //Debug.Log("Notifications have been disabled; do not reschedule");
        }
    }
}
