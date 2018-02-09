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

namespace XamSnap.Droid.UI.Activity
{
    [Activity(Label = "Messages")]
    public class MessagesActivity : BaseActivity<MessageViewModel>
    {
        ListView listView;
        EditText messageText;
        Button sendButton;
        Adapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Title = viewModel.Conversation.Username;
            SetContentView(Resource.Layout.Messages);

            listView = FindViewById<ListView>(Resource.Id.messageList);
            messageText = FindViewById<EditText>(Resource.Id.messageText);
            sendButton = FindViewById<Button>(Resource.Id.sendButton);

            listView.Adapter = adapter = new Adapter(this);
            sendButton.Click += async (sender, e) =>
            {
                viewModel.Text = messageText.Text;
                try
                {
                    await viewModel.SendMessage();
                    messageText.Text = string.Empty;
                    adapter.NotifyDataSetInvalidated();
                }
                catch (Exception exc)
                {
                    DisplayError(exc);
                }
            };
        }

        protected async override void OnResume()
        {
            base.OnResume();
            try
            {
                await viewModel.GetMessages();
                adapter.NotifyDataSetInvalidated();
                listView.SetSelection(adapter.Count);
            }
            catch (Exception exc)
            {
                DisplayError(exc);
            }
        }

        class Adapter : BaseAdapter<SnapMessage>
        {
            readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();
            readonly ISettings settings = ServiceContainer.Resolve<ISettings>();
            readonly LayoutInflater inflater;
            const int MyMessageType = 0, TheirMessageType = 1;

            public Adapter(Context context)
            {
                inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var message = this[position];
                int type = GetItemViewType(position);

                if (convertView == null)
                {
                    if (type == MyMessageType)
                    {
                        convertView = inflater.Inflate(Resource.Layout.MyMessageListItem, null);
                    }
                    else
                    {
                        convertView = inflater.Inflate(Resource.Layout.TheirMessageListItem, null);
                    }
                }

                TextView messageText;
                TextView messageDate;
                if (type == MyMessageType)
                {
                    messageText = convertView.FindViewById<TextView>(Resource.Id.myMessageText);
                    messageDate = convertView.FindViewById<TextView>(Resource.Id.myMessageDate);
                }
                else
                {
                    messageText = convertView.FindViewById<TextView>(Resource.Id.theirMessageText);
                    messageDate = convertView.FindViewById<TextView>(Resource.Id.theirMessageDate);
                }
                messageText.Text = message.Text;
                messageDate.Text = message.MessageDate.ToString();
                return convertView;
            }

            public override int Count
            {
                get
                {
                    return messageViewModel.Messages == null ? 0 : messageViewModel.Messages.Length;
                }
            }

            public override SnapMessage this[int position]
            {
                get { return messageViewModel.Messages[position]; }
            }

            public override int ViewTypeCount
            {
                get { return 2; }
            }

            public override int GetItemViewType(int position)
            {
                var message = this[position];
                return message.UserId == settings.User.Id ? MyMessageType : TheirMessageType;
            }
        }
    }
}