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

namespace XamSnap.Droid.UI.Activity
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true)]
    public class LoginActivity : BaseActivity<LoginViewModel>
    {
        EditText username, password;
        Button login;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            username = FindViewById<EditText>(Resource.Id.username);
            password = FindViewById<EditText>(Resource.Id.password);
            login = FindViewById<Button>(Resource.Id.login);
            login.Click += OnLogin;
        }

        protected override void OnResume()
        {
            base.OnResume();
            username.Text =
            password.Text = string.Empty;
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.ConversationsMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            Toast.MakeText(this, "Action selected: " + item.TitleFormatted, ToastLength.Short).Show();

            //if (item.ItemId == Resource.Id.addFriendMenu)
            //{
            //    StartActivity(typeof(FriendsActivity));
            //}
            return base.OnOptionsItemSelected(item);
        }

        async void OnLogin(object sender, EventArgs e)
        {
            viewModel.Username = username.Text;
            viewModel.Password = password.Text;

            try
            {
                await viewModel.Login();
                //TODO: navigate to a new activity
                StartActivity(typeof(ConversationsActivity));
            }
            catch (Exception exc)
            {
                DisplayError(exc);
            }
        }
    }
}