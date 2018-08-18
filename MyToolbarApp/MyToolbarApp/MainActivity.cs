using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;


/// <summary>
/// This demo shows how to use widget Toolbar, so app template is generated from XamarinTemplate = SingleViewApp.
/// See Googles' tutorial https://developer.android.com/training/appbar/ on which is this code snipet inspired with.
/// - Toolbar is widget which replaces native Actionbar over various Android relaces and its definition is done in toolbar.axml.
///   To use include it into your own layout, f.g. main.axml. Note: See aplication style definition styles.xml how to Actionbar switch off.
///   In the activity's onCreate() method, call the activity's setSupportActionBar() method, and pass the activity's toolbar.
/// </summary>

namespace MyToolbarApp
{
    [Activity(Label = "@string/app_name", MainLauncher = true, LaunchMode = Android.Content.PM.LaunchMode.SingleTop, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {

        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.main);

            // Note that the Toolbar defined in the layout has the id "toolbar"
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
                SupportActionBar.SetDisplayHomeAsUpEnabled(false);
                SupportActionBar.SetHomeButtonEnabled(false);
            }

            // Get our button from the layout resource,
            // and attach an event to it
            var clickButton = FindViewById<Button>(Resource.Id.my_button);

            clickButton.Click += (sender, args) =>
              {
                  clickButton.Text = string.Format("{0} clicks!", count++);
              };

        }
    }
}

