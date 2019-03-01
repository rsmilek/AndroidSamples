using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace ProgressBar
{
    /// <summary>
    /// https://stackoverflow.com/questions/45373007/progressdialog-is-deprecated-what-is-the-alternate-one-to-use
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var builder = new Android.Support.V7.App.AlertDialog.Builder(this); // Builder(context)
            builder.SetCancelable(false); // if you want user to wait for some process to finish,
            builder.SetView(Resource.Layout.layout_loading_dialog);
            var dialog = builder.Create();

            var button = FindViewById<Button>(Resource.Id.button);

            button.Click += delegate
            {
                dialog.Show(); // to show this dialog

                Task.Run(() =>
                {
                    Thread.Sleep(3000);
                    RunOnUiThread(() => { dialog.Dismiss(); }); // to hide this dialog
                });
            };
        }
    }
}