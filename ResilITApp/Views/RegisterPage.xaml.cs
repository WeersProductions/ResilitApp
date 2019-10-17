using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ResilITApp
{
    public partial class RegisterPage : ContentView
    {
        public RegisterPage()
        {
            InitializeComponent();
            InputTicketCode.ReturnCommand = new Command(() => InputFirstName.Focus());
            InputFirstName.ReturnCommand = new Command(() => InputSurname.Focus());
            InputOtherRemarks.ReturnCommand = new Command(() => InputEmail.Focus());
            InputEmail.ReturnCommand = new Command(() => InputPassword.Focus());
            InputPassword.ReturnCommand = new Command(() => InputConfirmPassword.Focus());
        }
    }
}
