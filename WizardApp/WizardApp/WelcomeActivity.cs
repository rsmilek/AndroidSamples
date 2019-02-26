using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using WizardApp.Fragments;

using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android;

namespace WizardApp
{
    [Activity(Label = "Wizard", MainLauncher = true, Icon = "@drawable/icon"/*, Theme = "@style/Theme.AppCompat.Light.DarkActionBar"*/)]
    public class WelcomeActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.welcome);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "ViewPager Indicator Dots";

            var pager = new ViewPagerAdapter(SupportFragmentManager);
            var viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            viewPager.Adapter = pager;
            viewPager.PageSelected += ViewPager_PageSelected;
        }

        private void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            var viewPagerCountDots = FindViewById<LinearLayout>(Resource.Id.viewPagerCountDots);
            for (int i = 0; i < viewPagerCountDots.ChildCount; i++)
            {
                ImageView dotImage = (ImageView)viewPagerCountDots.GetChildAt(i);
                if (i == e.Position)
                    dotImage.SetImageResource(Resource.Drawable.DotSelected);
                else
                    dotImage.SetImageResource(Resource.Drawable.DotUnSelected);
            }
        }
    }


    public class ViewPagerAdapter : FragmentStatePagerAdapter
    {
        readonly int numberOfFragments = 3;

        public ViewPagerAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
        {
        }

        public override int Count
        {
            get
            {
                return numberOfFragments;
            }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0:
                    return new FragmentA();
                case 1:
                    return new FragmentB();
                case 2:
                    return new FragmentC();
                default: return new FragmentA();
            }
        }
    }
}
