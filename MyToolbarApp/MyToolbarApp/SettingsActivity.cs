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
using Android.Support.V7.App;

namespace MyToolbarApp
{
    [Activity(Label = "@string/settings", Icon = "@drawable/icon")]
    public class SettingsActivity : AppCompatActivity
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
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);           // This adds back arrow button to left in Toolbar
                SupportActionBar.SetHomeButtonEnabled(true);                // This enables home = back arrow event handling
            }
        }

        /// <summary>Toolbar's back arrow event click handler.</summary>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();                                                   // Close activity if home = back arraw button pressed
            return base.OnOptionsItemSelected(item);
        }
    }
}