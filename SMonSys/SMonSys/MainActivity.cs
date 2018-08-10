using Android.App;
using Android.OS;
using Android.Content;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;

namespace com.rsware.smonsys
{
    [Activity(Label = "@string/ApplicationName", Theme = "@style/MyTheme")]
    public class MainActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(MainActivity).Name;
        Button btnGoDropbox;
        //int clickCount;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            btnGoDropbox = FindViewById<Button>(Resource.Id.btnDropbox);

            btnGoDropbox.Click += (sender, args) =>
                             {
                                 //string message = string.Format("{0} {1}.", Resources.GetString(Resource.String.GoDropbox), ++clickCount);
                                 //var message = Resources.GetString(Resource.String.GoDropbox);
                                 //btnGoDropbox.Text = message;
                                 //Log.Debug(TAG, message);

                                 StartActivity(new Intent(Application.Context, typeof(DropboxActivity)));
                             };

            // Connect event handlers
            //btnGoDropbox.Click += delegate { btnGoDropbox_OnClick(); };

            Log.Debug(TAG, "MainActivity is loaded.");
        }



        //protected void btnGoDropbox_OnClick()
        //{

        //}

    }
}