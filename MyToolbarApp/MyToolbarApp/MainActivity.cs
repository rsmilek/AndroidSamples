﻿using System;
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
/// - To handle Toolbar's actions you have to add xml menu resource menu.xml, see comments how to handle actions.
///   Note: Use 'Menu Compat' from available 'New item' of VS.
/// - Additoinally added handling of material menu inspired by https://developer.xamarin.com/samples/monodroid/Supportv7/AppCompat/Toolbar/, see SettingsActivity.cs for details.
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

            // Get our button from the layout resource and attach an event to it
            var clickButton = FindViewById<Button>(Resource.Id.my_button);

            clickButton.Click += (sender, args) =>
              {
                  clickButton.Text = string.Format("{0} clicks!", count++);
              };
        }

        /// <summary>Change main_compat_menu.</summary>
        /// <param name="menu">Menu object to change.</param>
        /// <returns>See Google's docu.</returns>
        /// <remarks>Code copied from menu.xml comment.</remarks>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>Called when the user selects one of the app bar items.</summary>
        /// <param name="item">Object to indicate which item was clicked.</param>
        /// <returns>See Google's docu.</returns>
        /// <remarks>Code copied from menu.xml comment.</remarks>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_settings:
                    StartActivity(new Intent(Application.Context, typeof(SettingsActivity)));
                    break;
                case Resource.Id.action_item1:
                    Toast.MakeText(this, "You pressed 'Item1' action!", ToastLength.Short).Show();
                    break;
                case Resource.Id.action_item2:
                    Toast.MakeText(this, "You pressed 'Item2' action!", ToastLength.Short).Show();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

    }
}

