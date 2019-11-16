using System;
using System.Collections.Generic;
using ResilITApp.Model;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace ResilITApp.Views
{
    public partial class ScanQRPage : ContentView
    {
        public ScanQRPage()
        {
            InitializeComponent();
            //ScanQRPageViewModel binding = new ScanQRPageViewModel();
            //mainView.BindingContext = binding;
            //scannerView.OnScanResult += binding.DoScanResult;
        }

        //async void StartScanning(object sender, EventArgs args)
        //{
        //    ZXingScannerPage scanPage = new ZXingScannerPage();
        //    scanPage.OnScanResult += (result) =>
        //    {
        //        scanPage.IsScanning = false;
        //        Navigation.PopModalAsync();
                
        //    };
        //    await Navigation.PushModalAsync(scanPage);
        //}
    }
}
