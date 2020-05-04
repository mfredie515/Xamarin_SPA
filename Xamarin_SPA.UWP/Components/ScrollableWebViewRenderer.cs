using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Xamarin_SPA.Components;
using Xamarin_SPA.UWP.Components;

[assembly: ExportRenderer(typeof(ScrollableWebView), typeof(ScrollableWebViewRenderer))]
namespace Xamarin_SPA.UWP.Components
{
    public class ScrollableWebViewRenderer : WebViewRenderer
    {
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

                StorageFolder storageFolder = ApplicationData.Current.TemporaryFolder;
                StorageFile pdfFile = await storageFolder.CreateFileAsync("temp.pdf", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteBytesAsync(pdfFile, pdfData);

                StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                StorageFolder assetsFolder = await appInstalledFolder.GetFolderAsync("Assets");
                await pdfFile.MoveAsync(assetsFolder, "temp.pdf", NameCollisionOption.ReplaceExisting);

                Control.Source = new Uri(string.Format("ms-appx-web:///Assets/pdfjs/web/viewer.html?file={0}", "ms-appx-web:///Assets/temp.pdf"));
            }
        }
    }
}
