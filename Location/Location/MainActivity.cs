using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Util;

namespace Location
{
	///<summary>Implement ILocationListener interface to get location updates</summary>
	[Activity (Label = "Location", MainLauncher = true)]
	public class MainActivity : Activity, ILocationListener
	{
		LocationManager locMgr;
		string tag = "MainActivity";
		Button button;
		TextView tvLatitude;
		TextView tvLongitude;
		TextView tvProvider;
        TextView tvDistance;
        LatLng lastLatLng = new LatLng();
        LatLng actLatLng = new LatLng();
        double? distance = null;


        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			Log.Debug(tag, "----- OnCreate called");

			// Set our view from the "Main.axml" layout resource
			SetContentView(Resource.Layout.Main);
			button = FindViewById<Button> (Resource.Id.myButton);
            tvLatitude = FindViewById<TextView> (Resource.Id.latitude);
            tvLongitude = FindViewById<TextView> (Resource.Id.longitude);
            tvProvider = FindViewById<TextView> (Resource.Id.provider);
            tvDistance = FindViewById<TextView>(Resource.Id.distance);
        }

        protected override void OnStart()
		{
			base.OnStart();
			Log.Debug(tag, "----- OnStart called");
		}

		// OnResume gets called every time the activity starts, so we'll put our RequestLocationUpdates
		// code here, so that 
	    ///<summary></summary>
		protected override void OnResume()
		{
			base.OnResume(); 
			Log.Debug(tag, "----- OnResume called");

			// initialize location manager
			locMgr = GetSystemService(Context.LocationService) as LocationManager;
            if (locMgr.AllProviders.Contains(LocationManager.GpsProvider))
                Log.Debug(tag, "----- GpsProvider available");
            if (locMgr.AllProviders.Contains(LocationManager.NetworkProvider))
                Log.Debug(tag, "----- NetworkProvider available");
            if (locMgr.AllProviders.Contains(LocationManager.PassiveProvider))
                Log.Debug(tag, "----- PassiveProvider available");


            button.Click += delegate {
				button.Text = "Location Service Running";

                // pass in the provider (GPS), 
                // the minimum time between updates (in seconds), 
                // the minimum distance the user needs to move to generate an update (in meters),
                // and an ILocationListener (recall that this class impletents the ILocationListener interface)
                //if (locMgr.AllProviders.Contains(LocationManager.NetworkProvider)
                //    && locMgr.IsProviderEnabled(LocationManager.NetworkProvider))
                //{
                //    locMgr.RequestLocationUpdates(LocationManager.NetworkProvider, 2000, 1, this);
                //}
                //else
                //{
                //    Toast.MakeText(this, "The network provider does not exist or is not enabled!", ToastLength.Long).Show();
                //}


                // Comment the line above, and uncomment the following, to test 
                // the GetBestProvider option. This will determine the best provider
                // at application launch. Note that once the provide has been set
                // it will stay the same until the next time this method is called
                //var locationCriteria = new Criteria();

                //locationCriteria.Accuracy = Accuracy.Coarse;
                //locationCriteria.PowerRequirement = Power.Medium;

                //string locationProvider = locMgr.GetBestProvider(locationCriteria, true);

                //Log.Debug(tag, "Starting location updates with " + locationProvider.ToString());
                //locMgr.RequestLocationUpdates(locationProvider, 2000, 1, this);


                // Requestes GPS provider for updates every min 2000ms & min 1m
                if (locMgr.AllProviders.Contains(LocationManager.GpsProvider)
                    && locMgr.IsProviderEnabled(LocationManager.GpsProvider))
                {
                    locMgr.RequestLocationUpdates(LocationManager.GpsProvider, 2000, 1, this);
                }
                else
                {
                    Toast.MakeText(this, "The GPS provider does not exist or is not enabled!", ToastLength.Long).Show();
                }
            };
		}

		protected override void OnPause()
		{
			base.OnPause();

			// stop sending location updates when the application goes into the background
			// to learn about updating location in the background, refer to the Backgrounding guide
			// http://docs.xamarin.com/guides/cross-platform/application_fundamentals/backgrounding/


			// RemoveUpdates takes a pending intent - here, we pass the current Activity
			locMgr.RemoveUpdates(this);
			Log.Debug(tag, "----- Location updates paused because application is entering the background");
		}

		protected override void OnStop()
		{
			base.OnStop();
			Log.Debug(tag, "----- OnStop called");
		}

	    ///<summary>Called when the location has changed.</summary>
		///<remarks>Implements ILocationListener interface.</remarks>
		public void OnLocationChanged(Android.Locations.Location location)
		{
            tvLatitude.Text = "Latitude: " + location.Latitude.ToString();
            tvLongitude.Text = "Longitude: " + location.Longitude.ToString();
            tvProvider.Text = "Provider: " + location.Provider.ToString();

            if (!distance.HasValue)
            {
                Log.Debug(tag, "----- Location initialized");
                lastLatLng.Latitude = location.Latitude;
                lastLatLng.Longitude = location.Longitude;
                distance = 0.0;
            }
            else
            {
                Log.Debug(tag, "----- Location changed");
                actLatLng.Latitude = location.Latitude;
                actLatLng.Longitude = location.Longitude;
                distance += Utils.HaversineDistance(lastLatLng, actLatLng, Utils.DistanceUnit.Kilometers);
                lastLatLng.Latitude = actLatLng.Latitude;
                lastLatLng.Longitude = actLatLng.Longitude;
                tvDistance.Text = "Distance: " + distance.ToString() + " Km";
            }
        }

        ///<summary>Called when the provider is disabled by the user.</summary>
        ///<remarks>Implements ILocationListener interface.</remarks>
        public void OnProviderDisabled(string provider)
		{
			Log.Debug(tag, "----- " + provider + " disabled by user");
		}

	    ///<summary>Called when the provider is enabled by the user.</summary>
		///<remarks>Implements ILocationListener interface.</remarks>
		public void OnProviderEnabled(string provider)
		{
			Log.Debug(tag, "----- " + provider + " enabled by user");
		}

	    ///<summary>Called when the provider status changes.</summary>
		///<remarks>Implements ILocationListener interface.</remarks>
		public void OnStatusChanged(string provider, Availability status, Bundle extras)
		{
			Log.Debug(tag, "----- " + provider + " availability has changed to " + status.ToString());
		}
	}
}


