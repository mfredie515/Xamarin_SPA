using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin_SPA.Components;
using Xamarin_SPA.Droid.Components;

[assembly: ExportRenderer(typeof(ScrollableWebView), typeof(ScrollableWebViewRenderer))]
namespace Xamarin_SPA.Droid.Components
{
    public class ScrollableWebViewRenderer : WebViewRenderer
    {
        public ScrollableWebViewRenderer(Context context) : base(context)
        {

        }

        protected override async void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var customWebView = Element as ScrollableWebView;

                //string base64 = customWebView.Uri.Substring(1);
                //string base64OneTrail = base64.Substring(0, base64.Length - 1);
                //byte[] pdfData = Convert.FromBase64String(base64OneTrail);
                string base64 = customWebView.Uri;
                byte[] pdfData = Convert.FromBase64String(base64);

                string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "temp.pdf");
                await File.WriteAllBytesAsync(path, pdfData);

                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                //Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", string.Format("file:///android_asset/Content/{0}", WebUtility.UrlEncode(customWebView.Uri))));
                Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", path));
            }
        }

        public override bool DispatchTouchEvent(MotionEvent e)
        {
            Parent.RequestDisallowInterceptTouchEvent(true);
            return base.DispatchTouchEvent(e);
        }
    }
}