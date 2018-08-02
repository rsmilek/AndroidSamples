using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Dropbox.Api;

namespace com.rsware.smonsys
{
    [Activity(Label = "@string/DropboxLogin", Theme = "@style/MyTheme")]
    public class DropboxActivity : AppCompatActivity
    {
        static ProgressBar progressBar;
        WebView webView;
        string oauth2State;
        Uri redirectUri = new Uri("https://localhost/authorize");

        public Uri RedirectUri { get { return redirectUri; } }
        public String Oauth2State { get { return oauth2State; } }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Dropbox);

            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            webView = FindViewById<WebView>(Resource.Id.webView);

            CookieManager.Instance.RemoveAllCookies(null);                       // TODO: Compatibility?   Smaze historii ulozeneho prihlasovani ve webview, 
            CookieManager.Instance.Flush();

            //webView.Settings.UserAgentString = "Android WebView";
            //webView.Settings.LoadWithOverviewMode = true;
            //webView.Settings.UseWideViewPort = true;
            //webView.Settings.BuiltInZoomControls = true;
            webView.Settings.JavaScriptEnabled = true;                      // Some websites will require Javascript to be enabled
            webView.Settings.BuiltInZoomControls = true;                    // allow zooming/panning
            webView.Settings.SetSupportZoom(true);
            webView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;        // scrollbar stuff            
            webView.ScrollbarFadingEnabled = false;                         // so there's no 'white line'

            webView.SetWebViewClient(new DropboxWebLogin(this));

            oauth2State = Guid.NewGuid().ToString("N");
            var authorizeUri = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, "ohgnlxqq0gpcoby", RedirectUri, oauth2State);

            webView.LoadUrl(authorizeUri.AbsoluteUri.ToString());
        }

        public override void OnBackPressed()
        {
            if (webView.CanGoBack())
                webView.GoBack();
            else
                base.OnBackPressed();
        }


        public class DropboxWebLogin : WebViewClient
        {
            DropboxActivity dropboxActivity;
            string accessToken;
            bool loading;

            public DropboxWebLogin(DropboxActivity dropboxActivity)
            {
                this.dropboxActivity = dropboxActivity;
            }

            /// <summary>Give the host application a chance to take over the control when a new url is about to be loaded in the current WebView</summary>
            public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
            {
                view.LoadUrl(request.Url.ToString());
                return true;
            }

            public override void OnPageStarted(WebView view, String url, Bitmap favicon)
            {
                loading = true;
                progressBar.Progress = view.Progress;
                progressBar.Visibility = ViewStates.Visible;

                if (!url.StartsWith(dropboxActivity.RedirectUri.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return;                                                     // we need to ignore all navigation that isn't to the redirect uri.  
                }

                try
                {
                    var result = DropboxOAuth2Helper.ParseTokenFragment(new Uri(url)); // Causes exception on Dropbox App disallow access
                    if (result.State != dropboxActivity.Oauth2State)
                    {
                        return;
                    }

                    accessToken = result.AccessToken; // TODO: Ulozit token 
                }
                catch
                {
                    // TODO: Ulozit ne token :-)
                }
                finally
                {
                    view.StopLoading(); // zastavi loadovani stranky s redirectUri, ktera samozdrejme neexistuje a pak je vyhozena chyba ...
                    dropboxActivity.Finish();
                }
            }

            public override void OnLoadResource(WebView view, string url)
            {
                if (loading)
                    progressBar.Progress = view.Progress;
            }

            public override void OnPageFinished(WebView view, string url)
            {
                progressBar.Progress = 0;
                progressBar.Visibility = ViewStates.Invisible;
                loading = false;
            }
        }

    }


}