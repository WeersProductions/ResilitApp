using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ResilITApp.Control;
using Xamarin.Forms;

namespace ResilITApp
{
    public class SignInViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string mail;

        public string Mail
        {
            get
            {
                return mail;
            }
            set
            {
                mail = value;
                HasError = false;
                NotifyPropertyChanged();
            }
        }

        private bool hasError;
        public bool HasError
        {
            get
            {
                return hasError;
            }
            set
            {
                hasError = value;
                NotifyPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                IsPasswordEmpty = false;
                NotifyPropertyChanged();
            }
        }

        private bool isPasswordEmpty;
        public bool IsPasswordEmpty
        {
            get
            {
                return isPasswordEmpty;
            }
            set
            {
                isPasswordEmpty = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand SubmitCommand { get; private set; }

        public SignInViewModel()
        {
            SubmitCommand = new Command<string>(SubmitButtonClicked);
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void SubmitButtonClicked(object obj)
        {
            AppController.AddBusy(this);
            IsPasswordEmpty = string.IsNullOrEmpty(Password);

            if(string.IsNullOrEmpty(Mail) || !mail.Contains("@") || !mail.Contains("."))
            {
                HasError = true;
            }
            else if (!isPasswordEmpty)
            {
                // TODO: check for connection instead of crash
                bool success = await Login.Instance.DoLoginAsync(new Model.SignInModel { email = Mail, password = Password });
                AppController.RemoveBusy(this);
                if (success)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "You are logged in.", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Failed", "Incorrect username or password.", "OK");
                }
            }
            AppController.RemoveBusy(this);
        }
    }
}
