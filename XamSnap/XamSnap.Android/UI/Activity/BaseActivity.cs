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
using Android.Support.V7.App;

namespace XamSnap.Droid.UI.Activity
{
    [Activity]
    public class BaseActivity<TViewModel> : AppCompatActivity where TViewModel : BaseViewModel
    {
        protected readonly TViewModel viewModel;
        protected ProgressDialog progress;

        public BaseActivity()
        {
            viewModel = ServiceContainer.Resolve(typeof(TViewModel)) as TViewModel;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            progress = new ProgressDialog(this);
            progress.SetCancelable(false);
            progress.SetTitle(Resource.String.Loading);
        }

        protected override void OnResume()
        {
            base.OnResume();
            viewModel.IsBusyChanged += OnIsBusyChanged;
        }

        protected override void OnPause()
        {
            base.OnPause();
            viewModel.IsBusyChanged -= OnIsBusyChanged;
        }

        protected void DisplayError(Exception exc)
        {
            string error = exc.Message;
            new Android.Support.V7.App.AlertDialog.Builder(this)
                .SetTitle(Resource.String.ErrorTitle)
                .SetMessage(error)
                .SetPositiveButton(Android.Resource.String.Ok, (IDialogInterfaceOnClickListener)null)
                .Show();
        }

        void OnIsBusyChanged(object sender, EventArgs e)
        {
            if (viewModel.IsBusy)
                progress.Show();
            else
                progress.Hide();
        }
    }
}