using Android.App;
using Android.Widget;
using Android.OS;

namespace Phoneword
{
    [Activity(MainLauncher = true, Label = "Phone Word", Theme = "@android:style/Theme.Light.NoTitleBar", Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // New code will go here

            // Get our UI controls from the loaded layout
            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.edtPhoneNumber);
            TextView translatedPhoneWord = FindViewById<TextView>(Resource.Id.tvPhonewordTranslated);
            Button translateButton = FindViewById<Button>(Resource.Id.btnTranslate);

            // Add code to translate number
            translateButton.Click += (sender, e) =>
            {
                // Translate user’s alphanumeric phone number to numeric
                string translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(translatedNumber))
                {
                    translatedPhoneWord.Text = string.Empty;
                }
                else
                {
                    translatedPhoneWord.Text = translatedNumber;
                }
            };
        }
    }
}

