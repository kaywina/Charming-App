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
    private const int missedAndroidNotificationID_A = 2;
    private const int missedAndroidNotificationID_B = 3;
    private const int missedAndroidNotificationID_C = 4;

#elif UNITY_IOS
    private const string iOSDailyNotificationID = "CharmingAppDailyNotification";
    private const string iOSMissedNotificationID_A = "CharmingAppMissedNotificationA";
    private const string iOSMissedNotificationID_B = "CharmingAppMissedNotificationB";
    private const string iOSMissedNotificationID_C = "CharmingAppMissedNotificationC";
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

    private void ScheduleRepeatMissedNotificationsAndroid()
    {
        ScheduleAndroidMissedNotification(2, 12, missedAndroidNotificationID_A, Localization.GetTranslationByKey("MISSED_NOTIFICATION_TITLE_1"), Localization.GetTranslationByKey("MISSED_NOTIFICATION_TEXT_1")); // first notification at noon after 2 days
        ScheduleAndroidMissedNotification(4, 19, missedAndroidNotificationID_B, Localization.GetTranslationByKey("MISSED_NOTIFICATION_TITLE_2"), Localization.GetTranslationByKey("MISSED_NOTIFICATION_TEXT_2")); // second notifcation at 7pm after 4 days
        ScheduleAndroidMissedNotification(6, 9, missedAndroidNotificationID_B, Localization.GetTranslationByKey("MISSED_NOTIFICATION_TITLE_3"), Localization.GetTranslationByKey("MISSED_NOTIFICATION_TEXT_3")); // second notifcation at 9am after 6 days
    }

    private void ScheduleAndroidMissedNotification(int days, int hour, int id, string titleLocKey, string textLocKey)
    {
        //Debug.Log("Schedule repeat daily Android mindfulness notification");
        var notification = new AndroidNotification();

        notification.Title = titleLocKey;
        notification.Text = textLocKey;

        DateTime dayToStart = DateTime.Now.AddDays(days);
        DateTime fireTime = new DateTime(dayToStart.Year, dayToStart.Month, dayToStart.Day, hour, 0, 0);

        notification.FireTime = fireTime;
        notification.RepeatInterval = new TimeSpan(3, 0, 0, 0); // repeat daily

        AndroidNotificationCenter.SendNotificationWithExplicitID(notification, CHANNEL_ID, id);
        Debug.Log("android notification for id = " + id + " scheduled for day = " + fireTime.Day + " and hour = " + hour);
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
        //Debug.Log("iOS daily notification scheduled");
    }

    private void ScheduleRepeatMissedNotificationsIos()
    {
        //Debug.Log("Schedule repeat missed iOS mindfulness notification");

        ScheduleIOSMissedNotification(1, 7, iOSMissedNotificationID_A, Localization.GetTranslationByKey("MISSED_NOTIFICATION_TITLE_1"), Localization.GetTranslationByKey("MISSED_NOTIFICATION_TEXT_1"), Localization.GetTranslationByKey("MISSED_NOTIFICATION_SUBTITLE_1")); // 7am next day
        ScheduleIOSMissedNotification(2, 8, iOSMissedNotificationID_B, Localization.GetTranslationByKey("MISSED_NOTIFICATION_TITLE_2"), Localization.GetTranslationByKey("MISSED_NOTIFICATION_TEXT_2"), Localization.GetTranslationByKey("MISSED_NOTIFICATION_SUBTITLE_2")); // 8am day after that
        ScheduleIOSMissedNotification(3, 9, iOSMissedNotificationID_C, Localization.GetTranslationByKey("MISSED_NOTIFICATION_TITLE_3"), Localization.GetTranslationByKey("MISSED_NOTIFICATION_TEXT_3"), Localization.GetTranslationByKey("MISSED_NOTIFICATION_SUBTITLE_3")); // 9am day after that
    }

    private void ScheduleIOSMissedNotification(int days, int hour, string id, string titleLocKey, string bodyLocKey, string subtitleLocKey) // id must be unique for each notification
    {
        // DateTime is unique for each IOS missed notifcation
        DateTime tomorrow = DateTime.Now.AddDays(days);
        int day = tomorrow.Day;
        int month = tomorrow.Month;
        int year = tomorrow.Year;

        var calendarTrigger = new iOSNotificationCalendarTrigger()
        {
            Year = year,
            Month = month,
            Day = day,
            Hour = hour,
            Minute = 0,
            Repeats = false
        };

        var notification = new iOSNotification()
        {
            Identifier = id,
            Title = titleLocKey,
            Body = bodyLocKey,
            Subtitle = subtitleLocKey,
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = iOSNotificationCategory,
            ThreadIdentifier = iOSThreadID,
            Trigger = calendarTrigger,
        };

        iOSNotificationCenter.ScheduleNotification(notification);
        Debug.Log("ios notification for id = " + id + " scheduled for day = " + day + " and hour = " + hour);
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
        AndroidNotificationCenter.CancelNotification(missedAndroidNotificationID_A);
        AndroidNotificationCenter.CancelNotification(missedAndroidNotificationID_B);
        AndroidNotificationCenter.CancelNotification(missedAndroidNotificationID_C);
#elif UNITY_IOS
        iOSNotificationCenter.RemoveScheduledNotification(iOSMissedNotificationID_A);
        iOSNotificationCenter.RemoveScheduledNotification(iOSMissedNotificationID_B);
        iOSNotificationCenter.RemoveScheduledNotification(iOSMissedNotificationID_C);
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
        ScheduleRepeatMissedNotificationsAndroid();
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
