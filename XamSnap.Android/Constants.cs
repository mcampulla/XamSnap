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

namespace XamSnap.Droid
{
    public static class Constants
    {
        public const string ProjectId = "525066927252";
        public const string ConnectionString = "Endpoint=sb://xamsnap.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=8A2thiucyl6Sji13uouGcgx3FWLopXEjFD7XAIY+roQ=";
        public const string HubName = "xamsnapnotificationhub";
    }
}