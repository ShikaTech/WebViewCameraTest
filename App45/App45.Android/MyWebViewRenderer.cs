using Android.Annotation;
using Android.App;
using Android.Content;
using Android.Webkit;
using App45.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(App45.MyWebView), typeof(MyWebViewRenderer))]
namespace App45.Droid
{
    public class MyWebViewRenderer : WebViewRenderer
    {
        Activity mContext;
        public MyWebViewRenderer(Context context) : base(context)
        {
            this.mContext = context as Activity;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            Control.Settings.JavaScriptEnabled = true;
            Control.Settings.DomStorageEnabled = true;
            Control.Settings.AllowFileAccess = true;
            Control.Settings.AllowFileAccessFromFileURLs = true;
            Control.Settings.AllowUniversalAccessFromFileURLs = true;
            Control.ClearCache(true);
            Control.SetWebChromeClient(new MyWebClient(mContext));
        }
        public class MyWebClient : WebChromeClient
        {
            Activity mContext;
            public MyWebClient(Activity context)
            {
                this.mContext = context;
            }
            [TargetApi(Value = 21)]
            public override void OnPermissionRequest(PermissionRequest request)
            {
                mContext.RunOnUiThread(() =>
                {
                    request.Grant(request.GetResources());
                });
            }
        }
    }
}