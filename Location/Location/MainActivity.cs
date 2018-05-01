using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Util;
using Java.Text;

namespace Location
{
	///<summary>Implement ILocationListener interface to get location updates</summary>
	[Activity (Label = "Location", MainLauncher = true)]
	public class MainActivity : Activity, ILocationListener
	{
		LocationManager locMgr = null;
		string tag = "MainActivity";
		Button btnStartStop;
		TextView tvLatitude;
		TextView tvLongitude;
		TextView tvProvider;
        TextView tvDistance;
        TextView tvSpeed;
        /// <summary>Stores GPS coordinates of last location update.</summary>
        LatLng lastLatLng = new LatLng();
        /// <summary>Stores GPS coordinates of actrual location update.</summary>
        LatLng actLatLng = new LatLng();
        /// <summary>Stores begin time measurement.</summary>
        long startTime = 0;
        /// <summary>Stores whole distance taken from location updates.</summary>
        double? distance = null;

        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			Log.Debug(tag, "----- OnCreate called");

			// Set our view from the "Main.axml" layout resource
			SetContentView(Resource.Layout.Main);
            btnStartStop = FindViewById<Button> (Resource.Id.btnStartStop);
            tvLatitude = FindViewById<TextView> (Resource.Id.tvLatitude);
            tvLongitude = FindViewById<TextView> (Resource.Id.tvLongitude);
            tvProvider = FindViewById<TextView> (Resource.Id.tvProvider);
            tvDistance = FindViewById<TextView>(Resource.Id.tvDistance);
            tvSpeed = FindViewById<TextView>(Resource.Id.tvSpeed);

            // Connect event handlers
            btnStartStop.Click += delegate { btnStartStop_OnClick(); };
        }

        protected override void OnStart()
		{
			base.OnStart();
			Log.Debug(tag, "----- OnStart called");
		}

        ///<summary>OnResume gets called every time the activity starts, so we'll put our RequestLocationUpdates code here, so that.</summary>
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
		}

		protected override void OnPause()
		{
			base.OnPause();

            // stop sending location updates when the application goes into the background
            // to learn about updating location in the background, refer to the Backgrounding guide
            // http://docs.xamarin.com/guides/cross-platform/application_fundamentals/backgrounding/

            // RemoveUpdates takes a pending intent - here, we pass the current Activity
            HandleLocationUpdates(false);
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
            if (location == null) return;

            tvLatitude.Text = "Latitude: " + location.Latitude.ToString();
            tvLongitude.Text = "Longitude: " + location.Longitude.ToString();
            tvProvider.Text = "Provider: " + location.Provider.ToString();

            //Decide what to do on location update
            if (!distance.HasValue)
            {
                //First location update - do initializations
                Log.Debug(tag, "----- Location initialized");
                lastLatLng.Assign(location);
                startTime = location.Time;
                distance = 0.0;
            }
            else
            {
                //Next location updates - calculate distance step as actual - last & whole distance 
                Log.Debug(tag, "----- Location changed");
                actLatLng.Assign(location);
                distance += Utils.HaversineDistance(lastLatLng, actLatLng, Utils.DistanceUnit.Kilometers);
                lastLatLng.Assign(actLatLng);
                tvDistance.Text = String.Format("Distance: {0:0.000} Km", distance);

                var testStr = String.Format("{0:0.00} Km/h   ", location.Speed * 3.6);
                testStr += new Java.Text.SimpleDateFormat("HH:mm:ss").Format(location.Time - startTime);
                tvSpeed.Text = testStr;

                ////location.HasAccuracy
                ////location.Speed
                ////location.Time
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

        /// <summary>Activity component event handler - btnStartStop click.</summary>
        public void btnStartStop_OnClick()
        {
            if (locMgr.AllProviders.Contains(LocationManager.GpsProvider)
                && locMgr.IsProviderEnabled(LocationManager.GpsProvider))
            {
                // Handle location manager updates by button state (label value)
                HandleLocationUpdates( btnStartStop.Text == GetString(Resource.String.btn_start) );
            }
            else
            {
                Toast.MakeText(this, "The GPS provider does not exist or is not enabled!", ToastLength.Long).Show();
            }
        }

        /// <summary>Handle updates of location manager.</summary>
        /// <param name="enable">True requests location updates, False removes location updates.</param>
        private void HandleLocationUpdates(bool enable)
        {
            if (enable)
            {
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

                // Requests GPS updates every 1000ms OR 1m
                locMgr.RequestLocationUpdates(LocationManager.GpsProvider, 1000, 1, this);
                btnStartStop.Text = GetString(Resource.String.btn_stop);
                Log.Debug(tag, "----- Requests location updates");
            }
            else
            {
                // RemoveUpdates takes a pending intent - here, we pass the current Activity
                locMgr.RemoveUpdates(this);
                btnStartStop.Text = GetString(Resource.String.btn_start);
                tvLatitude.Text = "";
                tvLongitude.Text = "";
                tvProvider.Text = "";
                tvDistance.Text = "";
                tvSpeed.Text = "";
                startTime = 0;
                distance = null;
                Log.Debug(tag, "----- Removes location updates");
            }
        }
    }
}


