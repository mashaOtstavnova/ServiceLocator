using System;
using System.Collections.Generic;
using WindowsAzure.Messaging;
using Android.App;
using Android.Content;
using Android.Util;
using Gcm.Client;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;

[assembly: Permission(Name = "otstavnova.ServiceLocator.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "otstavnova.ServiceLocator.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]

namespace ServiceLocator.Droid
{
    [BroadcastReceiver(Permission = Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new[] { Constants.INTENT_FROM_GCM_MESSAGE },
        Categories = new[] { "otstavnova.ServiceLocator" })]
    [IntentFilter(new[] { Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
        Categories = new[] { "otstavnova.ServiceLocator" })]
    [IntentFilter(new[] { Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
        Categories = new[] { "otstavnova.ServiceLocator" })]
    public class GcmBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
        public const string SenderID = "519701756904"; // Google API Project Number

        public const string ListenConnectionString =
                "Endpoint=sb://servicelocator.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=0Gmf/q3TF+JQeCXa+eB5jM0ju1wE9Ty9UGSYoM20ukk="
            ;

        public const string NotificationHubName = "servicelocator";

        public const string TAG = "MyBroadcastReceiver-GCM";

        ////IMPORTANT: Change this to your own Sender ID!
        ////The SENDER_ID is your Google API Console App Project Number
        public static string[] SENDER_IDS = { "519701756904" };
    }

    [Service] //Must use the service tag
    public class GcmService : GcmServiceBase
    {
        private string RegistrationID;

        public GcmService() : base(GcmBroadcastReceiver.SENDER_IDS)
        {
        }

        public NotificationHub Hub { get; set; }


        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose(GcmBroadcastReceiver.TAG, "GCM Registered: " + registrationId);
            RegistrationID = registrationId;

            //createNotification("PushHandlerService-GCM Registered...",
            //    "The device has been Registered!");

            Hub = new NotificationHub(GcmBroadcastReceiver.NotificationHubName,
                GcmBroadcastReceiver.ListenConnectionString,
                context);
            try
            {
                Hub.UnregisterAll(registrationId);
            }
            catch (Exception ex)
            {
                Log.Error(GcmBroadcastReceiver.TAG, ex.Message);
            }

            //var tags = new List<string>() { "falcons" }; // create tags if you want
            var tags = new List<string>();

            try
            {
                var hubRegistration = Hub.Register(registrationId, tags.ToArray());
            }
            catch (Exception ex)
            {
                Log.Error(GcmBroadcastReceiver.TAG, ex.Message);
            }
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Hub.Unregister();
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            var currentActivity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            var message = string.Empty;

            // Extract the push notification message from the intent.
            if (intent.Extras.ContainsKey("message"))
            {
                message = intent.Extras.Get("message").ToString();
                var title = "New item added:";

                // Create a notification manager to send the notification.
                var notificationManager =
                    GetSystemService(NotificationService) as NotificationManager;

                // Create a new intent to show the notification in the UI. 
                var contentIntent =
                    PendingIntent.GetActivity(context, 0,
                        new Intent(this, currentActivity.GetType()), 0);

                // Create the notification using the builder.
                var builder = new Notification.Builder(context);
                builder.SetAutoCancel(true);
                builder.SetContentTitle(title);
                builder.SetContentText(message);
                builder.SetSmallIcon(Resource.Drawable.ic_launcher);
                builder.SetContentIntent(contentIntent);
                var notification = builder.Build();

                // Display the notification in the Notifications Area.
                notificationManager.Notify(1, notification);
            }
        }

        protected override bool OnRecoverableError(Context context, string errorId)
        {
            return true;
            //Some recoverable error happened
        }

        protected override void OnError(Context context, string errorId)
        {
            //Some more serious error happened
        }
    }
}