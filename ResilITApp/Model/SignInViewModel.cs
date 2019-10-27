using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ResilITApp.Control;
using ResilITApp.Model;
using Xamarin.Forms;
using Xamarin.Auth;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        public static async Task LoadAuth()
        {
            List<Account> accounts = await SecureStorageAccountStore.FindAccountsForServiceAsync(App.AppName);
            var account = accounts.FirstOrDefault();
            if (account != null)
            {
                await DoLogin(account.Username, account.Properties["Password"], false);
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void SubmitButtonClicked(object obj)
        {
            AppController.AddBusy("SignInViewModel");
            IsPasswordEmpty = string.IsNullOrEmpty(Password);

            if(string.IsNullOrEmpty(Mail) || !mail.Contains("@") || !mail.Contains("."))
            {
                HasError = true;
            }
            else if (!isPasswordEmpty)
            {
                if(await DoLogin(Mail, Password, true))
                {
                    // Save this.
                    Account account = new Account
                    {
                        Username = Mail
                    };
                    account.Properties.Add("Password", Password);
                    await SecureStorageAccountStore.SaveAsync(account, App.AppName);
                }
            }
            AppController.RemoveBusy("SignInViewModel");
        }

        private static async Task<bool> DoLogin(string emailInput, string passwordInput, bool showPopup)
        {
            if(string.IsNullOrEmpty(emailInput) || string.IsNullOrEmpty(passwordInput))
            {
                return false;
            }

            HttpMessage httpMessage = await Login.Instance.DoLoginAsync(new SignInModel { email = emailInput, password = passwordInput });
            AppController.RemoveBusy("SignInViewModel");
            if (httpMessage.Success)
            {
                if(showPopup)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "You are logged in.", "OK");
                }
                MainPage.Instance.ShowSchedule();
                return true;
            }
            else
            {
                if(showPopup)
                {
                    await Application.Current.MainPage.DisplayAlert("Failed", httpMessage.Message, "OK");
                }
                return false;
            }
        }
    }
}
