using Android.App;
using Android.OS;
using Android.Content;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;

namespace com.rsware.smonsys
{
    [Activity(Label = "@string/ApplicationName")]
    public class MainActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(MainActivity).Name;
        Button btnGoDropbox;
        int clickCount;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            btnGoDropbox = FindViewById<Button>(Resource.Id.BtnDropbox);

            btnGoDropbox.Click += (sender, args) =>
                             {
                                 string message = string.Format("{0} {1}.", Resources.GetString(Resource.String.GoDropbox), ++clickCount);
                                 btnGoDropbox.Text = message;
                                 Log.Debug(TAG, message);

                                 StartActivity(new Intent(Application.Context, typeof(DropboxActivity)));
                             };

            Log.Debug(TAG, "MainActivity is loaded.");
        }
    }
}