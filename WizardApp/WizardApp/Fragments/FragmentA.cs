using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace WizardApp
{
    public class FragmentA : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.fragmentA, container, false);

            //Here goes what you want to do within the fragment.

            return view;
        }
    }
}