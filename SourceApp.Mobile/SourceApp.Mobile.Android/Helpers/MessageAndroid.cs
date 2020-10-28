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
using SourceApp.Mobile.Droid.Helpers;
using SourceApp.Mobile.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace SourceApp.Mobile.Droid.Helpers
{
    public class MessageAndroid : IMessage
    {
        public MessageAndroid()
        {

        }
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
    }
}