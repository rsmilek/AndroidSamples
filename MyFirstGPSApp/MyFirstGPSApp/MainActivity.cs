using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;


namespace MyFirstGPSApp
{

    /// <summary>
    /// Before testing the app make sure that you have the following permissions within the AndroidManifest.xml:
    /// <uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />  
    /// <uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />  
    /// <uses-permission android:name="android.permission.INTERNET" />
    /// </summary>
    [Activity(Label = "MyFirstGPSApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        TextView _locationText;
        TextView _addressText;
        TextView _remarksText;

        GPSServiceBinder _binder;
        GPSServiceConnection _gpsServiceConnection;
        Intent _gpsServiceIntent;
        private GPSServiceReciever _receiver;

        public static MainActivity Instance;

        /// <summary>
        /// The OnCreate event is where we initialize the ContentViews and the TextViews. 
        /// It is also where we register the service that we need for our app. 
        /// </summary>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Instance = this;
            SetContentView(Resource.Layout.Main);

            _addressText = FindViewById<TextView>(Resource.Id.txtAddress);
            _locationText = FindViewById<TextView>(Resource.Id.txtLocation);
            _remarksText = FindViewById<TextView>(Resource.Id.txtRemarks);

            RegisterService();
        }

        /// <summary>
        /// The RegisterService() registers and binds the service needed. 
        /// </summary>
        private void RegisterService()
        {
            _gpsServiceConnection = new GPSServiceConnection(_binder);
            _gpsServiceIntent = new Intent(Android.App.Application.Context, typeof(GPSService));
            BindService(_gpsServiceIntent, _gpsServiceConnection, Bind.AutoCreate);
        }

        /// <summary>
        /// The RegisterBroadcastReciever() method registers the broadcast reciever so we can have access to the data from the broadcast. 
        /// This method will be called at OnResume event override. 
        /// </summary>
        private void RegisterBroadcastReceiver()
        {
            IntentFilter filter = new IntentFilter(GPSServiceReciever.LOCATION_UPDATED);
            filter.AddCategory(Intent.CategoryDefault);
            _receiver = new GPSServiceReciever();
            RegisterReceiver(_receiver, filter);
        }

        /// <summary>
        /// The UnRegisterBroadcastReceiver() method unregisters the broadcast reciever. 
        /// This method will be called at OnPause event override of the activity.
        /// </summary>
        private void UnRegisterBroadcastReceiver()
        {
            UnregisterReceiver(_receiver);
        }

        public void UpdateUI(Intent intent)
        {
            _locationText.Text = intent.GetStringExtra("Location");
            _addressText.Text = intent.GetStringExtra("Address");
            _remarksText.Text = intent.GetStringExtra("Remarks");
        }

        protected override void OnResume()
        {
            base.OnResume();
            RegisterBroadcastReceiver();
        }

        protected override void OnPause()
        {
            base.OnPause();
            UnRegisterBroadcastReceiver();
        }


        /// <summary>
        /// The GPSServiceReciever class is used to handle the message from a broadcast by implemeting BroadcastReciever. 
        /// If you recall, under GPSService OnLocationChanged event we sent out an Intent broadcast to pass values. 
        /// These values will then be displayed in the UI for the client to see.
        /// </summary>
        [BroadcastReceiver]
        internal class GPSServiceReciever : BroadcastReceiver
        {
            public static readonly string LOCATION_UPDATED = "LOCATION_UPDATED";

            public override void OnReceive(Context context, Intent intent)
            {
                if (intent.Action.Equals(LOCATION_UPDATED))
                {
                    MainActivity.Instance.UpdateUI(intent);
                }

            }
        }
    }
}

