using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App45
{
	public partial class MainPage : ContentPage
	{
        MyWebView _webView;
        Button _button;
        StackLayout stackLayout;
        public MainPage()
		{
			InitializeComponent();
           
            Task<bool> b=initPer();
            System.Diagnostics.Debug.Write("permissions============================================================"+ b.Result);
            initView();
            //initPermissions();

        }

        private async Task<bool> initPer()
        {
            var statusCamera = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var statusMicrophone = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Microphone);
            return statusCamera == PermissionStatus.Granted && statusMicrophone == PermissionStatus.Granted;
        }

        private async void initPermissions()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                {
                    await DisplayAlert("Need location", "Gunna need that location", "OK");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                //Best practice to always check that the key exists
                if (results.ContainsKey(Permission.Location))
                    status = results[Permission.Location];
            }

            if (status == PermissionStatus.Granted)
            {
               
            }
            else if (status != PermissionStatus.Unknown)
            {
                await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
            }
        }

        private void initView()
        {
           
            _button = new Button
            {
                Text="add webview",
                WidthRequest = 100,
                HeightRequest = 50
            };
            _button.Clicked += _button_Clicked;

            stackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(5, 20, 5, 10),
                Children = { _button }
            };

            Content = new StackLayout { Children = { stackLayout } };
        }

        private void _button_Clicked(object sender, EventArgs e)
        {
            _webView = new MyWebView
            {
                Source = "https://test.webrtc.org/",
                WidthRequest = 1000,
                HeightRequest = 1000
            };
            stackLayout.Children.Add(_webView);
        }
    }
}
