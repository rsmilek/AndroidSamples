using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Threading.Tasks;
using System.Threading;

namespace ProgressDialogApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var button = FindViewById<Button>(Resource.Id.button);

            button.Click += delegate
            {
                var progressDialog = new Android.App.ProgressDialog(this);
                progressDialog.Indeterminate = true;
                progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
                progressDialog.SetMessage("Loading... Please wait...");
                progressDialog.SetCancelable(false);
                progressDialog.Show();

                Task.Run(() =>
                {
                    Thread.Sleep(3000);
                    RunOnUiThread(() => { progressDialog.Hide(); });
                });
            };
        }
    }
}