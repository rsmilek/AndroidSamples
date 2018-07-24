using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Webkit;
using Android.Graphics;

namespace com.rsware.smonsys
{
    [Activity(Label = "@string/ApplicationName")]
    public class DropboxActivity : Activity
    {
        WebView webView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DropboxWeb);

            webView = FindViewById<WebView>(Resource.Id.WebView);


            webView.SetWebViewClient(new WebViewClient());
            webView.LoadUrl("http://www.dropbox.com");

            // Some websites will require Javascript to be enabled
            webView.Settings.JavaScriptEnabled = true;
            // allow zooming/panning            
            webView.Settings.BuiltInZoomControls = true;
            webView.Settings.SetSupportZoom(true);
            // scrollbar stuff            
            webView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
            // so there's no 'white line'            
            webView.ScrollbarFadingEnabled = false;




//            webView.Settings.JavaScriptEnabled = true;
//            webView.Settings.UserAgentString = "Android WebView";
//            webView.Settings.LoadWithOverviewMode = true;
//            webView.Settings.UseWideViewPort = true;
//            webView.Settings.BuiltInZoomControls = true;

//            webView.SetWebViewClient(new DropboxWebViewClient());
//////            webView.LoadUrl("https://www.xamarin.com/university");
//            ////            webView.LoadData("pokus", "text/html", null);
//            webView.LoadUrl("www.ibm.com‎");
        }
    }


    public class DropboxWebViewClient : WebViewClient
    {
        public override void OnPageStarted(WebView view, String url, Bitmap favicon)
        {
            // TODO Auto-generated method stub
            base.OnPageStarted(view, url, favicon);
        }

        public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
        {
            view.LoadUrl(request.Url.ToString());
            return true;
        }
    }
}