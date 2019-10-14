using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ResilITApp
{
    public partial class LoginPage : ContentView
    {
        public LoginPage()
        {
            InitializeComponent();

            InputEmail.ReturnCommand = new Command(() => InputPassword.Focus());
        }
    }
}
