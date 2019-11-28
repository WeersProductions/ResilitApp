using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Newtonsoft.Json;
using ResilITApp.Control;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace ResilITApp.Model
{
    public class ScanQRPageViewModel : INotifyPropertyChanged
    {
        private string scannedName;
        public string ScannedName
        {
            get
            {
                return scannedName;
            }
            set
            {
                scannedName = value;
                NotifyPropertyChanged();
            }
        }

        private string scannedId;
        public string ScannedId
        {
            get
            {
                return scannedId;
            }
            set
            {
                scannedId = value;
                NotifyPropertyChanged();
            }
        }

        private bool doScan;
        public bool DoScan
        {
            get
            {
                return doScan;
            }
            set
            {
                doScan = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("DoNotScan");

                if(value)
                {
                    ScannedName = "";
                    ErrorMessage = "";
                    ScanSuccess = false;
                }
            }
        }

        public bool DoNotScan
        {
            get
            {
                return !doScan;
            }
        }

        private string lastScan;

        private bool scanSuccess;
        public bool ScanSuccess
        {
            get
            {
                return scanSuccess;
            }
            set
            {
                scanSuccess = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("ScanFailed");
            }
        }

        public bool ScanFailed
        {
            get
            {
                return !ScanSuccess;
            }
        }

        public bool HasError
        {
            get
            {
                return !string.IsNullOrEmpty(ErrorMessage);
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("HasError");
            }
        }

        private string comment;
        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand ScanQRCommand { get; private set; }
        public ICommand SubmitCommentCommand { get; private set; }
        public ICommand CheckScanResult { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ScanQRPageViewModel()
        {
            ScanQRCommand = new Command<string>(ScanQRButtonClicked);
            SubmitCommentCommand = new Command<string>(UpdateComment);
            CheckScanResult = new Command<ZXing.Result>(DoScanResult);
            DoScan = true;
        }

        private async void UpdateComment(object obj)
        {
            if(!ScanSuccess || string.IsNullOrEmpty(lastScan))
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "Last scan was not successfull. Try to scan the badge again.", "OK");
            }

            AppController.AddBusy(this);

            // Update the comment.
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("comment", Comment)
            });
            HttpMessage response = await Login.Instance.DoPost($"api/badge-scanning/edit/{ScannedId}", formContent);
            ScanSuccess = response.Success;
            if(response.Success)
            {
                string json = await response.Response.Content.ReadAsStringAsync();
                JSONQRComment commentResult = JsonConvert.DeserializeObject<JSONQRComment>(json);
                ScanSuccess = commentResult.success;
                if(!commentResult.success)
                {
                    ErrorMessage = commentResult.message;
                } else
                {
                    ErrorMessage = "";
                    ScanSuccess = false;
                }
            } else
            {
                if(string.IsNullOrEmpty(response.Message))
                {
                    response.Message = "Could not save comment.";
                }
                ErrorMessage = response.Message;
            }
            AppController.RemoveBusy(this);
        }

        private async void DoScanResult(ZXing.Result result)
        {
            DoScan = false;
            string scanned_user = result.Text;
            lastScan = scanned_user;
            AppController.AddBusy(this);
            HttpMessage response = await Login.Instance.DoPost($"api/badge-scanning/{scanned_user}");
            ScanSuccess = response.Success;
            if (response.Success)
            {
                string json = await response.Response.Content.ReadAsStringAsync();
                JSONScanQR scanResult = JsonConvert.DeserializeObject<JSONScanQR>(json);
                ScanSuccess = scanResult.success;
                if (!scanResult.success)
                {
                    ErrorMessage = scanResult.message;
                }
                else
                {
                    ScannedName = scanResult.user_name;
                    scannedId = scanResult.id;
                }
            }
            else
            {
                if(string.IsNullOrEmpty(response.Message))
                {
                    response.Message = "Could not verify.";
                }
                ErrorMessage = response.Message;
            }
            AppController.RemoveBusy(this);
        }

        private void ScanQRButtonClicked(object obj)
        {
            ErrorMessage = "";
            ScannedName = "";
            DoScan = true;
            //ZXingScannerPage scanPage = new ZXingScannerPage();
            //scanPage.OnScanResult += async (result) =>
            //{
                
            //    //await Application.Current.MainPage.Navigation.PopModalAsync();
            //    scanPage.IsVisible = false;
            //};
            //await Application.Current.MainPage.Navigation.PushModalAsync(scanPage);
        }
    }
}
