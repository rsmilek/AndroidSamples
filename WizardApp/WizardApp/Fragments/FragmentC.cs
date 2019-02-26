using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace WizardApp
{
    public class FragmentC : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.fragmentC, container, false);

            //Here goes what you want to do within the fragment.
            var btn = view.FindViewById<Button>(Resource.Id.btn_finish);
            btn.Click += delegate 
            {
                var intent = new Intent(this.Activity, typeof(MainActivity));
                StartActivity(intent);
            };

            return view;
        }
    }
}