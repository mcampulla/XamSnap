using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gcm.Client;
using XamSnap.Models;
using Android.Support.V7.App;
using XamSnap.Droid.UI.Activity;

namespace XamSnap.Droid
{
    [Service]
    public class PushHandlerService : GcmServiceBase
    {
        public PushHandlerService() : base(Constants.ProjectId)
        {

        }

        protected override void OnError(Context context, string errorId)
        {
            Console.WriteLine("GCM error: " + errorId);
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            string message = intent.Extras.GetString("message");
            if (!string.IsNullOrEmpty(message))
            {
                var notificationManager = (NotificationManager)
                GetSystemService(Context.NotificationService);
                var notification = new NotificationCompat.Builder(this)
                        .SetContentIntent(
                            PendingIntent.GetActivity(this, 0,new Intent(this, typeof(LoginActivity)), 0))
                        .SetSmallIcon(Android.Resource.Drawable.SymActionEmail)
                        .SetAutoCancel(true)
                        .SetContentTitle("XamSnap")
                        .SetContentText(message)
                        .Build();
                notificationManager.Notify(1, notification);
            }
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            var notificationService = ServiceContainer.Resolve<INotificationService>();
            notificationService.SetToken(registrationId);
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Console.WriteLine("GCM unregistered!");
        }
    }
}