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
using XamSnap.ViewModels;
using XamSnap.Models;
using XamSnap.Azure;

namespace XamSnap.Droid
{
	[Application(Theme = "@style/Theme.AppCompat.Light")]
	public class BaseApplication : Application
    {
		public BaseApplication(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{

		}

		public override void OnCreate()
		{
			base.OnCreate();

			//ViewModels
			ServiceContainer.Register<LoginViewModel>(() => new LoginViewModel());
			ServiceContainer.Register<FriendViewModel>(() => new FriendViewModel());
			ServiceContainer.Register<MessageViewModel>(() => new MessageViewModel());
			ServiceContainer.Register<RegisterViewModel>(() => new RegisterViewModel());

			//Models
			ServiceContainer.Register<ISettings>(() => new FakeSettings());
			//ServiceContainer.Register<IWebService>(() => new FakeWebService());
            ServiceContainer.Register<IWebService>(() => new AzureWebService());
        }
	}
}

