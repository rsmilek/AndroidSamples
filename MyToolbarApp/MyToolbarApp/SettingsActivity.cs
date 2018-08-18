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
using static Android.Views.View;
using Android.Support.V7.App;

namespace MyToolbarApp
{
    [Activity(Label = "@string/settings", Icon = "@drawable/icon")]
    public class SettingsActivity : AppCompatActivity, IOnClickListener
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.settings);

            // Note that the Toolbar defined in the layout has the id "toolbar"
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
                // This adds back arraw to Toolbar, 
                // another way is add 'android:parentActivityName=".screens.MainActivity"' into AndroidManifest.xml
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(false);
                // Adds back arrow event handler - needs implementation of IOnClickListener interface
                toolbar.SetNavigationOnClickListener(this);
            }
        }

        /// <summary>Toolbar's back arrow event click handler.</summary>
        public void OnClick(View view)
        {
            // Closes Settings activity
            Finish();
        }
    }
}